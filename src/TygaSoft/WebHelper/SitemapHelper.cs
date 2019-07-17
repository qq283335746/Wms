using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Configuration;

namespace TygaSoft.WebHelper
{
    public class SitemapHelper
    {
        private static readonly bool enableCaching = bool.Parse(ConfigurationManager.AppSettings["EnableCaching"]);

        public static IList<string> Roles { get; set; }

        public static SiteMapNode DoSiteMapResolve(Object sender, SiteMapResolveEventArgs e)
        {
            SiteMapNode currentNode = SiteMap.CurrentNode.Clone(true);
            SiteMapNode tempNode = currentNode;
            var appPath = e.Context.Request.AppRelativeCurrentExecutionFilePath;
            switch (appPath)
            {
                case "~/Admin/InStore/ListOrderReceipt.aspx":
                    if (e.Context.Request.QueryString["orderType"] == "1") tempNode.Title = "ASN预收货";
                    else tempNode.Title = "收货单";
                    break;
                case "~/Admin/InStore/EditOrderReceiptProduct.aspx":
                    if(e.Context.Request.QueryString["orderType"] == "1") tempNode.Title = "ASN预收货明细";
                    else tempNode.Title = "收货单明细";
                    break;
                default:
                    break;
            }

            return currentNode;
        }

        public static string GetTreeJsonForMenu()
        {
            if (!enableCaching)
            {
                return GetTreeJson();
            }

            string key = "Sitemap_" + WebCommon.GetUserId(HttpContext.Current) + "";
            string data = (string)HttpRuntime.Cache[key];
            if (data == null)
            {
                data = GetTreeJson();
                HttpRuntime.Cache.Add(key, data, null, DateTime.Now.AddHours(8), Cache.NoSlidingExpiration, CacheItemPriority.High, null);
            }

            return data;
        }

        private static string GetTreeJson()
        {
            StringBuilder jsonAppend = new StringBuilder();
            SiteMapNodeCollection nodes = SiteMap.RootNode.ChildNodes;
            jsonAppend.Append("[");
            int index = -1;
            foreach (SiteMapNode node in nodes)
            {
                if (!Roles.Contains("Administrators"))
                {
                    if (!IsInRole(node.Roles)) continue;
                }
                if (node.Description == "hide") continue;

                index++;
                if (index > 0)
                    jsonAppend.Append(",");
                jsonAppend.Append("{\"id\":\"" + node.Url + "\",\"text\":\"" + node.Title + "\",\"state\":\"open\"");
                CreateTreeJson(node, ref jsonAppend);
                jsonAppend.Append("}");
            }

            jsonAppend.Append("]");

            return jsonAppend.ToString();
        }

        private static void CreateTreeJson(SiteMapNode currNode, ref StringBuilder jsonAppend)
        {
            if (currNode.HasChildNodes)
            {
                jsonAppend.Append(",\"children\":");
                jsonAppend.Append("[");
                int temp = -1;
                foreach (SiteMapNode node in currNode.ChildNodes)
                {
                    if (!Roles.Contains("Administrators"))
                    {
                        if (!IsInRole(node.Roles)) continue;
                    }
                    if (node.Description == "hide") continue;

                    temp++;
                    if (temp > 0) jsonAppend.Append(",");
                    jsonAppend.Append("{\"id\":\"" + node.Url + "\",\"text\":\"" + node.Title + "\",\"state\":\"open\"");
                    CreateTreeJson(node, ref jsonAppend);

                    jsonAppend.Append("}");
                }
                jsonAppend.Append("]");
            }
        }

        private static bool IsInRole(IList roles)
        {
            if (roles == null || roles.Count == 0) return true;

            foreach (string item in roles)
            {
                return Roles.Contains(item);
            }

            return false;
        }
    }
}
