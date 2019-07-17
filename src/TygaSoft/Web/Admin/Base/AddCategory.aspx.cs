using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TygaSoft.BLL;
using TygaSoft.Model;

namespace TygaSoft.Web.Admin.Sys
{
    public partial class AddCategory : System.Web.UI.Page
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
            Guid Id = Guid.Empty;
            if (!string.IsNullOrWhiteSpace(Request.QueryString["Id"]))
            {
                Guid.TryParse(Request.QueryString["Id"], out Id);
            }
            string action = Request.QueryString["action"];
            switch (action)
            {
                case "add":
                    InitAdd(Id);
                    break;
                case "edit":
                    InitEdit(Id);
                    break;
                default:
                    break;
            }
        }

        private void InitAdd(Guid parentId)
        {
            hParentId.Value = parentId.ToString();
            var bll = new Category();
            txtCode.Value = bll.CreateCode(parentId);
        }

        private void InitEdit(Guid Id)
        {
            var bll = new Category();
            var model = bll.GetModel(Id);
            if (model != null)
            {
                hId.Value = model.Id.ToString();
                hParentId.Value = model.ParentId.ToString();
                txtCode.Value = model.CategoryCode;
                txtName.Value = model.CategoryName;
                txtRemark.Value = model.Remark;
                txtSort.Value = model.Sort.ToString();
            }
        }
    }
}