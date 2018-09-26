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
using TygaSoft.SysHelper;

namespace TygaSoft.Web.Manages.Members
{
    public partial class AddRole : System.Web.UI.Page
    {
        //protected void Page_PreInit(object sender, EventArgs e)
        //{
        //    if (User.IsInRole("Users_Asset"))
        //    {
        //        MasterPageFile = "~/Masters/Manages.Master";
        //    }
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
            }
            else
            {
                if (!User.IsInRole("Administrators"))
                {
                    MessageBox.Messager(Page, "对不起，您没有权限执行此操作，请联系管理员", MC.AlertTitle_Sys_Info, "warning");
                    return;
                }

                OnSave();
            }
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        private void OnSave()
        {
            string sRoleName = txtRolename.Value.Trim();
            if (string.IsNullOrEmpty(sRoleName))
            {
                MessageBox.MessagerShow(this.Page, Page.Controls[0], "角色名输入不能为空，请检查！");
                return;
            }

            string errorMsg = string.Empty;
            try
            {
                Roles.CreateRole(sRoleName);

                MessageBox.MessagerShow(this.Page, Page.Controls[0], "操作成功！");
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
            }

            if (!string.IsNullOrEmpty(errorMsg))
            {
                MessageBox.Messager(this.Page, Page.Controls[0], errorMsg, "系统异常提醒");
            }
        }
    }
}