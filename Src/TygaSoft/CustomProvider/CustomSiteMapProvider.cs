using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Web;
using System.Security.Permissions;
using System.Web.Security;
using System.Collections;
using System.IO;
using System.Collections.Specialized;
using TygaSoft.Model;
using TygaSoft.SysHelper;

namespace TygaSoft.CustomProvider
{
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
    public class CustomSiteMapProvider : SiteMapProvider
    {
        IList<SiteMenusInfo> list;

        public CustomSiteMapProvider()
        {
            this.list = MenusDataProxy.GetList();
        }

        public override SiteMapNode FindSiteMapNode(string rawUrl)
        {
            if (list == null) return null;
            var currNode = list.FirstOrDefault(m => m.Url.ToLower() == rawUrl.ToLower());
            if(currNode == null)
            {
                if (rawUrl.LastIndexOf("?") > -1) rawUrl = rawUrl.Substring(0, rawUrl.LastIndexOf("?"));
                currNode = list.FirstOrDefault(m => m.Url.ToLower().IndexOf(rawUrl.ToLower()) > -1);
            }

            if (currNode == null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    currNode = list.FirstOrDefault(m => m.Title == EnumData.EnumMenuName.首页.ToString() && !m.Url.Contains(FormsAuthentication.DefaultUrl.Replace("~", "")));
                }
                else currNode = list.FirstOrDefault(m => m.Url.Contains(FormsAuthentication.DefaultUrl.Replace("~", "")));
            }
            if (currNode == null) return null;
            return new SiteMapNode(this, currNode.Id.ToString(), currNode.Url, currNode.Title, currNode.Descr);
        }

        public override SiteMapNodeCollection GetChildNodes(SiteMapNode node)
        {
            if (list == null) return null;
            var q = list.Where(m => m.ParentId.ToString() == node.Key);
            if (q == null || q.Count() == 0) return null;
            SiteMapNodeCollection smnc = new SiteMapNodeCollection();
            foreach (var item in q)
            {
                smnc.Add(new SiteMapNode(this, item.Id.ToString(), item.Url, item.Title, item.Descr));
            }

            return smnc;
        }

        public override SiteMapNode GetParentNode(SiteMapNode node)
        {
            if (list == null) return null;
            var currNode = list.FirstOrDefault(m => m.Id.ToString() == node.Key);
            if (currNode == null) return null;
            var parentNode = list.FirstOrDefault(m => m.Id == currNode.ParentId);
            if (parentNode == null) return null;
            if (parentNode.Title == EnumData.EnumMenuName.禁止匿名访问.ToString() || parentNode.Title == EnumData.EnumMenuName.匿名访问.ToString()) return null;

            var temp = new SiteMapNode(this, parentNode.Id.ToString(), parentNode.Url, parentNode.Title, parentNode.Descr);

            return temp;
        }

        protected override SiteMapNode GetRootNodeCore()
        {
            if (list == null) return null;
            SiteMenusInfo rootNode = null;

            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                rootNode = list.FirstOrDefault(m => m.Title == EnumData.EnumMenuName.首页.ToString() && !m.Url.Contains(FormsAuthentication.DefaultUrl.Replace("~", "")));
            }
            else rootNode = list.FirstOrDefault(m => m.Url.Contains(FormsAuthentication.DefaultUrl.Replace("~", "")));

            if (rootNode == null) throw new Exception(MC.M_NotConfigError);

            var temp = new SiteMapNode(this, rootNode.Id.ToString(), rootNode.Url, rootNode.Title, rootNode.Descr);

            return temp;
        }
    }
}
