using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Security;

namespace TygaSoft.WebHelper
{
    public class WebCommon
    {
        public static string GetSiteAppName()
        {
            try
            {
                string appName = HostingEnvironment.ApplicationVirtualPath;
                if (String.IsNullOrEmpty(appName))
                {
                    appName = System.Diagnostics.Process.GetCurrentProcess().MainModule.ModuleName;

                    int indexOfDot = appName.IndexOf('.');
                    if (indexOfDot != -1)
                    {
                        appName = appName.Remove(indexOfDot);
                    }
                }

                if (String.IsNullOrEmpty(appName))
                {
                    return "/";
                }
                else
                {
                    return appName;
                }
            }
            catch
            {
                return "/";
            }
        }

        public static Guid GetUserId()
        {
            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (HttpContext.Current.User.Identity is FormsIdentity)
                    {
                        FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;
                        FormsAuthenticationTicket ticket = id.Ticket;
                        string[] datas = ticket.UserData.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        if (datas.Length > 0) return Guid.Parse(datas[0]);
                    }
                }
            }

            return Guid.Empty;
        }

        /// <summary>
        /// 获取当前用户ID
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static Guid GetUserId(HttpContext context)
        {
            MembershipUser user = Membership.GetUser();
            if(user == null) return Guid.Empty;
            return (Guid)user.ProviderUserKey;
        }

        /// <summary>
        /// 获取当前主机地址
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetUrlHost(HttpContext context)
        {
            if (context != null && context.User != null)
            {
                int port = context.Request.Url.Port;
                string host = context.Request.Url.Host;
                if (port != 80) host += ":" + port.ToString();
                return "http://" + host + context.Request.ApplicationPath;
            }

            return string.Empty;
        }

        public const decimal POINTNUM = 1000;

        public const int PageIndex = 1;

        public const int PageSize = 20;

        public const int PageSize10 = 10;
    }
}
