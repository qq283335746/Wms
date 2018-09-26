using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;
using System.Collections.Specialized;
using System.Text;
using TygaSoft.Model;
using TygaSoft.BLL;
using TygaSoft.WebHelper;

namespace TygaSoft.Web.Manages.Members
{
    public partial class ListUsers : System.Web.UI.Page
    {
        StringBuilder htmlAppend = new StringBuilder(1000);
        string userName;
        int pageIndex = WebCommon.PageIndex;
        int pageSize = WebCommon.PageSize10;
        int totalRecords = 0;
        string queryStr;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                NameValueCollection nvc = Request.QueryString;
                int index = 0;
                foreach (string item in nvc.AllKeys)
                {
                    GetParms(item, nvc);

                    if (item != "pageIndex" && item != "pageSize")
                    {
                        index++;
                        if (index > 1) queryStr += "&";
                        queryStr += string.Format("{0}={1}", item, Server.HtmlEncode(nvc[item]));
                    }
                }

                //数据绑定
                Bind();

                BindRoleJson();

                ltrMyData.Text = htmlAppend.ToString();
            }
        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        private void Bind()
        {
            GetSearchItem();

            MembershipUserCollection list;

            if (!string.IsNullOrWhiteSpace(userName))
            {
                list = Membership.FindUsersByName(userName, pageIndex - 1, pageSize, out totalRecords);
            }
            else
            {
                list = Membership.GetAllUsers(pageIndex - 1, pageSize, out totalRecords);
            }

            rpData.DataSource = list;
            rpData.DataBind();

            htmlAppend.Append("<div id=\"myDataForPage\" style=\"display:none;\">[{\"PageIndex\":\"" + pageIndex + "\",\"PageSize\":\"" + pageSize + "\",\"TotalRecord\":\"" + totalRecords + "\",\"QueryStr\":\"" + queryStr + "\"}]</div>");
        }

        private void GetSearchItem()
        {
            htmlAppend.Append("<div id=\"myDataForSearch\" style=\"display:none;\">{\"keyword\":\"" + userName + "\"}</div>");
        }

        /// <summary>
        /// 绑定角色
        /// </summary>
        private void BindRoleJson()
        {
            string roleAppend = "";
            string[] roles = Roles.GetAllRoles();
            foreach (string item in roles)
            {
                roleAppend += "{\"RoleName\":\"" + item + "\",\"IsInRole\":\"False\"},";
            }

            roleAppend = roleAppend.Trim(',');

            htmlAppend.Append("<div id=\"myDataForRole\" style=\"display:none;\">["+roleAppend+"]</div>");
        }

        /// <summary>
        /// 获取请求参数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="nvc"></param>
        private void GetParms(string key, NameValueCollection nvc)
        {
            switch (key)
            {
                case "pageIndex":
                    Int32.TryParse(nvc[key], out pageIndex);
                    break;
                case "pageSize":
                    Int32.TryParse(nvc[key], out pageSize);
                    break;
                case "keyword":
                    userName = Server.HtmlEncode(nvc[key]);
                    break;
                default:
                    break;
            }
        }
    }
}