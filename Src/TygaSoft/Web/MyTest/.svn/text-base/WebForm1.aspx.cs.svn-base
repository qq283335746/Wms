using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace TygaSoft.Web.MyTest
{
    public partial class WebForm1 : System.Web.UI.Page
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
            List<StudentInfo> list = new List<StudentInfo>();
            for (var i = 0; i < 10; i++)
            {
                list.Add(new StudentInfo { Name = "Name" + i });
            }
            rpData.DataSource = list;
            rpData.DataBind();
        }

        protected void rpData_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                bool isNoCheck = true;
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string databaseNameStr = string.Empty;

                RepeaterItemCollection ric = rpData.Items;
                foreach (RepeaterItem item in ric)
                {
                    //找到CheckBox
                    HtmlInputCheckBox cb = item.FindControl("cbItem") as HtmlInputCheckBox;
                    if (cb != null && cb.Checked)
                    {
                        if (isNoCheck) isNoCheck = false;
                        sb.AppendFormat("{{\"Name\":\"{0}\"}},", cb.Value);
                    }
                }

                if (isNoCheck)
                {
                    
                    return;
                }
            }
        }
    }

    public class StudentInfo {
        public string Name { get; set; }
    }
}