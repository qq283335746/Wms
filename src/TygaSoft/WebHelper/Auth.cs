using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json;
using TygaSoft.BLL;
using TygaSoft.Model;
using TygaSoft.SysHelper;
using TygaSoft.CustomProvider;
using TygaSoft.DBUtility;

namespace TygaSoft.WebHelper
{
    public class Auth
    {
        public static readonly string AppCode = "000000";

        public void SetAnonymousLogin()
        {
            if (ConfigHelper.GetValueByKey("RunMode") != "saas")
            {
                HttpContext.Current.Response.Redirect("~/u/t.html");
            }
            else
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated) return;

                CustomProfileCommon Profile = new CustomProfileCommon();
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, "新用户", DateTime.Now, DateTime.Now.Add(FormsAuthentication.Timeout),
                    true, Profile.GetUserName(), FormsAuthentication.FormsCookiePath);
                string encTicket = FormsAuthentication.Encrypt(ticket);
                HttpContext.Current.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
                HttpContext.Current.Response.Redirect("~/u/t.html");
            }
        }

        public void SetAnonymousLoginByPda(string platform, string deviceid, string latlng, string username, string password)
        {
            if (ConfigHelper.GetValueByKey("RunMode") != "saas") throw new ArgumentException(MC.M_NotConfigError);
            if (HttpContext.Current.User.Identity.IsAuthenticated) return;

            FormsAuthenticationTicket ticket = null;

            if (!Membership.ValidateUser(username, password)) throw new ArgumentException(MC.Login_InvalidAccount);
            var userData = Membership.GetUser(username).ProviderUserKey.ToString();
            ticket = new FormsAuthenticationTicket(1, username, DateTime.Now, DateTime.Now.Add(FormsAuthentication.Timeout), true, userData, FormsAuthentication.FormsCookiePath);

            HttpContext.Current.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket)));
        }

        public void SetMigrateAnonymous()
        {
            var userId = WebCommon.GetUserId();
            IList<SiteMenusInfo> smis = new List<SiteMenusInfo>();
            UserProfileInfo upi = new UserProfileInfo();
            var menuBll = new SiteMenus();

            if (Roles.GetRolesForUser().Length == 0)
            {
                var guestRole = new SiteRoles().GetAspnetModel(Membership.ApplicationName, "guest");
                string[] accessIds = { guestRole.Id.ToString() };
                smis = menuBll.GetMenusAccess(Membership.ApplicationName, accessIds, false);
                upi = new UserProfileInfo { SiteCode = AppCode, SiteTitle = "智能物流订单跟踪平台" };
            }
            else
            {
                var accessIds = new List<string>();
                accessIds.Add(userId.ToString());
                var roleIds = new SiteRoles().GetAspnetRoleIds(Membership.ApplicationName, Roles.GetRolesForUser());
                foreach (var item in roleIds)
                {
                    accessIds.Add(item.ToString());
                }
                var isAdmin = HttpContext.Current.User.IsInRole("Administrators");
                Task[] tasks = new Task[2];
                tasks[0] = Task.Factory.StartNew(() =>
                {
                    var fuInfo = new FeatureUser().GetModel(userId, "UserProfile");
                    if (fuInfo != null)
                    {
                        upi.SiteCode = fuInfo.SiteCode;
                        upi.SiteTitle = fuInfo.SiteTitle;
                        upi.SiteLogo = string.IsNullOrWhiteSpace(fuInfo.SiteLogo) ? "" : WebCommon.GetSiteAppName() + fuInfo.SiteLogo;
                        upi.CultureName = fuInfo.CultureName;
                    }
                });

                tasks[1] = Task.Factory.StartNew(() =>
                {
                    smis = menuBll.GetMenusAccess(Membership.ApplicationName, accessIds.ToArray(), isAdmin);
                });
                Task.WaitAll(tasks);
            }

            var Profile = new CustomProfileCommon();
            Profile.UserMenus = JsonConvert.SerializeObject(smis);
            Profile.UserInfo = JsonConvert.SerializeObject(upi);

            Profile.Save();
        }

        public void CreateSearchItem(ref StringBuilder sqlWhere, ref ParamsHelper parms)
        {
            if (HttpContext.Current.User.IsInRole("Administrators")) return;

            if (sqlWhere == null) sqlWhere = new StringBuilder(500);
            if (parms == null) parms = new ParamsHelper();

            CustomProfileCommon Profile = new CustomProfileCommon();
            var upi = JsonConvert.DeserializeObject<UserProfileInfo>(Profile.UserInfo);

            sqlWhere.Append("and AppCode = @AppCode ");
            var parm = new SqlParameter("@AppCode", SqlDbType.Char, 6);
            parm.Value = upi.SiteCode;
            parms.Add(parm);

            if (upi.SiteCode == "000000")
            {
                sqlWhere.Append("and UserId = @UserId ");
                parm = new SqlParameter("@UserId", SqlDbType.UniqueIdentifier);
                parm.Value = WebCommon.GetUserId();
                parms.Add(parm);
            }
        }
    }
}
