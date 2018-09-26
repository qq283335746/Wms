using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Profile;
using TygaSoft.BLL;

namespace TygaSoft.CustomProvider
{
    public class CustomProfileCommon:ProfileBase
    {
        public new void Save()
        {
            HttpContext.Current.Profile.Save();
        }

        [SettingsAllowAnonymous(false)]
        [ProfileProvider("SqlProfileProvider")]
        public string UserInfo
        {
            get { return (string)HttpContext.Current.Profile.GetPropertyValue("UserInfo"); }
            set { HttpContext.Current.Profile.SetPropertyValue("UserInfo", value); }
        }

        [SettingsAllowAnonymous(false)]
        [ProfileProvider("SqlProfileProvider")]
        public string UserMenus
        {
            get { return (string)HttpContext.Current.Profile.GetPropertyValue("UserMenus"); }
            set { HttpContext.Current.Profile.SetPropertyValue("UserMenus", value); }
        }

        public string GetUserName()
        {
            if (HttpContext.Current.Profile.IsAnonymous) return HttpContext.Current.Request.AnonymousID;
            else return HttpContext.Current.Profile.UserName;
        }
    }
}
