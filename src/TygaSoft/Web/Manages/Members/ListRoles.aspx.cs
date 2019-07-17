using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Specialized;
using TygaSoft.Model;
using TygaSoft.BLL;
using TygaSoft.WebHelper;

namespace TygaSoft.Web.Manages.Members
{
    public partial class ListRoles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Bind();
            }
            
        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        private void Bind()
        {
            Role bll = new Role();
            List<RoleInfo> roleList = bll.GetList();
            List<RoleInfo> list = new List<RoleInfo>();
            string[] items = Roles.GetAllRoles();
            foreach (string item in items)
            {
                RoleInfo model = new RoleInfo();
                model.RoleId = roleList.Find(m => m.RoleName == item).RoleId;
                model.RoleName = item;

                list.Add(model);
            }

            rpData.DataSource = list;
            rpData.DataBind();
        }
    }
}