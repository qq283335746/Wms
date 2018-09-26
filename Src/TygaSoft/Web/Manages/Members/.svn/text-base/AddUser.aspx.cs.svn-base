using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using TygaSoft.Model;
using TygaSoft.BLL;
using TygaSoft.WebHelper;

namespace TygaSoft.Web.Manages.Members
{
    public partial class AddUser : System.Web.UI.Page
    {
        string htmlAppend;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindRoleJson();

                ltrMyData.Text = htmlAppend;
            }
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

            htmlAppend += "<div id=\"myDataForRole\" style=\"display:none;\">[" + roleAppend + "]</div>";
        }
    }
}