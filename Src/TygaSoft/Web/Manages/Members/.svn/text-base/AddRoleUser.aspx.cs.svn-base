using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Web.Security;
using System.Transactions;
using System.Collections.Specialized;
using System.Text;
using TygaSoft.Model;
using TygaSoft.BLL;
using TygaSoft.WebHelper;

namespace TygaSoft.Web.Manages.Members
{
    public partial class AddRoleUser : System.Web.UI.Page
    {
        StringBuilder myDataAppend;
        string roleName;
        string userName;
        int pageIndex = WebCommon.PageIndex;
        int pageSize = WebCommon.PageSize10;
        int totalRecords = 0;
        string queryStr;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Request.QueryString["rName"]))
            {
                roleName = HttpUtility.HtmlDecode(Request.QueryString["rName"]).Trim();
            }
            if (!string.IsNullOrWhiteSpace(Request.QueryString["uName"]))
            {
                userName = HttpUtility.HtmlDecode(Request.QueryString["uName"]).Trim();
            }

            if (!Page.IsPostBack)
            {
                myDataAppend = new StringBuilder();

                lbRole.InnerText = roleName;

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
            }
        }

        private void Bind()
        {
            List<RoleInfo> list = new List<RoleInfo>();

            if (!string.IsNullOrEmpty(userName))
            {
                MembershipUserCollection users = Membership.FindUsersByName(userName, pageIndex-1, pageSize, out totalRecords);
                foreach (MembershipUser user in users)
                {
                    RoleInfo rModel = new RoleInfo();
                    rModel.UserName = user.UserName;
                    string[] currRoles = Roles.GetRolesForUser(user.UserName);
                    rModel.IsInRole = currRoles.Contains(roleName);

                    list.Add(rModel);
                }
            }
            else
            {
                string[] usersInRole = Roles.GetUsersInRole(roleName);

                foreach (var item in usersInRole)
                {
                    list.Add(new RoleInfo { UserName = item, IsInRole = true });
                }

                list = list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList<RoleInfo>();

                totalRecords = usersInRole.Length;
            }

            rpData.DataSource = list;
            rpData.DataBind();

            myDataAppend.Append("<div id=\"myDataForPage\" style=\"display:none;\">{\"PageIndex\":\"" + pageIndex + "\",\"PageSize\":\"" + pageSize + "\",\"TotalRecord\":\"" + totalRecords + "\",\"QueryStr\":\"" + queryStr + "\"}</div>");
            myDataAppend.Append("<div id=\"myDataForModel\" style=\"display:none;\">{\"Keyword\":\"" + userName + "\"}</div>");
            ltrMyData.Text = myDataAppend.ToString();
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
                default:
                    break;
            }
        }
    }
}