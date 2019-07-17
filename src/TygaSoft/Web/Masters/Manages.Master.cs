using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using TygaSoft.CustomProvider;
using TygaSoft.Model;
using TygaSoft.SysHelper;

namespace TygaSoft.Web.Masters
{
    public partial class Manages : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Bind();
            }
        }

        private void Bind()
        {
            Control ctl = this.LoadControl("~/WebUserControls/UCMenu.ascx");
            ctl.ID = "UCMenu";
            phUc.Controls.Add(ctl);

            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var Profile = new CustomProfileCommon();
                var userProfileInfo = JsonConvert.DeserializeObject<UserProfileInfo>(Profile.UserInfo);
                if(userProfileInfo != null)
                {
                    lbSiteTitle.InnerText = userProfileInfo.SiteTitle;
                    if (!string.IsNullOrWhiteSpace(userProfileInfo.SiteLogo))
                    {
                        imgLogo.Src = userProfileInfo.SiteLogo;
                        imgLogo.Visible = true;
                    }
                    if(!string.IsNullOrWhiteSpace(userProfileInfo.CultureName) && userProfileInfo.CultureName.ToLower() == "en-us")
                    {
                        LoginStatus lsUser = lvUser.FindControl("lsUser") as LoginStatus;
                        lsUser.LogoutText = "[Sign Out]";
                        LoginName lnUser = lvUser.FindControl("lnUser") as LoginName;
                        lnUser.FormatString = "Welcome，{0}";
                    }
                    lbAppId.InnerText = userProfileInfo.SiteCode;
                }
            }
        }
    }
}