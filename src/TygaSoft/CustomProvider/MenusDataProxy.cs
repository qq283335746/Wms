using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using Newtonsoft.Json;
using TygaSoft.CacheDependencyFactory;
using TygaSoft.CustomProvider;
using TygaSoft.BLL;
using TygaSoft.Model;
using TygaSoft.SysHelper;

namespace TygaSoft.CustomProvider
{
    public class MenusDataProxy
    {
        private static readonly bool enableCaching = bool.Parse(ConfigurationManager.AppSettings["EnableCaching"]);
        private static readonly int menusTimeout = int.Parse(ConfigurationManager.AppSettings["MenusCacheDuration"]);

        public static void ValidateAccess(int enumValidateAccess, bool isService)
        {
            if (HttpContext.Current.User.IsInRole("Administrators")) return;

            var url = "";
            if (isService)
            {
                var uri = HttpContext.Current.Request.UrlReferrer;
                if (uri == null) throw new ArgumentException(MC.Role_InvalidError);
                url = uri.AbsolutePath;
            }
            else url = HttpContext.Current.Request.RawUrl;
            if (url.LastIndexOf("?") > -1) url = url.Substring(0, url.LastIndexOf("?"));
            var userMenuList = GetUserMenus();
            var currNode = userMenuList.FirstOrDefault(m => !string.IsNullOrEmpty(m.Url) && m.Url == url);
            if (currNode == null) return;
            //if (currNode == null) throw new ArgumentException(MC.Role_InvalidError);
            switch (enumValidateAccess)
            {
                case (int)EnumData.EnumValidateAccess.IsView:
                    if (!currNode.IsView) throw new ArgumentException(MC.Role_InvalidError);
                    break;
                case (int)EnumData.EnumValidateAccess.IsAdd:
                    if (!currNode.IsAdd) throw new ArgumentException(MC.Role_InvalidError);
                    break;
                case (int)EnumData.EnumValidateAccess.IsEdit:
                    if (!currNode.IsEdit) throw new ArgumentException(MC.Role_InvalidError);
                    break;
                case (int)EnumData.EnumValidateAccess.IsDelete:
                    if (!currNode.IsDelete) throw new ArgumentException(MC.Role_InvalidError);
                    break;
                default:
                    throw new ArgumentException(MC.Role_InvalidError);
            }
        }

        public static IEnumerable<SiteMenusInfo> GetUserMenus()
        {
            if (HttpContext.Current.User.IsInRole("Administrators"))
            {
                var menusList = GetList();
                var rootNode = menusList.FirstOrDefault(m => m.Title == "禁止匿名访问");
                if(rootNode != null)
                {
                    return menusList.Where(m => m.IdStep.IndexOf(rootNode.Id.ToString()) > -1 && m.Descr.IndexOf("hide") == -1);
                }
                return null;
            }
            else
            {
                var userMenuList = new List<SiteMenusInfo>();
                var Profile = new CustomProfileCommon();
                var sUserMenu = Profile.UserMenus;
                if (!string.IsNullOrEmpty(sUserMenu)) userMenuList = JsonConvert.DeserializeObject<List<SiteMenusInfo>>(sUserMenu).FindAll(m => m.IsView && m.Descr.IndexOf("hide") == -1);

                return userMenuList;
            }
        }

        public static IList<SiteMenusInfo> GetList()
        {
            var appName = Membership.ApplicationName;
            var bll = new SiteMenus();

            if (!enableCaching)
            {
                return bll.GetMenus(appName);
            }

            string key = "Menus_All_" + appName + "";
            IList<SiteMenusInfo> data = (List<SiteMenusInfo>)HttpRuntime.Cache[key];

            if (data == null || data.Count == 0)
            {
                data = bll.GetMenus(appName);
                if(data != null && data.Count > 0)
                {
                    HttpRuntime.Cache.Add(key, data, null, DateTime.Now.AddHours(menusTimeout), Cache.NoSlidingExpiration, CacheItemPriority.High, null);
                }
            }

            return data;
        }
    }
}
