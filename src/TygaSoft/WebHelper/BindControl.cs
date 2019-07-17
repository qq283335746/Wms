using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using TygaSoft.Model;
using TygaSoft.SysHelper;

namespace TygaSoft.WebHelper
{
    public class BindControl
    {
        public void BindDdl(DropDownList ddl, Type enumType, string selectText,string firstText)
        {
            var list = EnumHelper.GetList(enumType);
            if(!string.IsNullOrWhiteSpace(firstText))
            {
                list.Insert(0, new KeyValueInfo { Key = "-1", Value = firstText });
            }
            
            ddl.DataSource = list;
            ddl.DataTextField = "Value";
            ddl.DataValueField = "Key";
            ddl.DataBind();

            var li = ddl.Items.FindByText(selectText);
            if (li != null) li.Selected = true;
        }

        public void BindDdl(DropDownList ddl, Type enumType, string selectText)
        {
            var list = EnumHelper.GetList(enumType);
            list.Insert(0, new KeyValueInfo { Key = "-1", Value = "请选择" });
            ddl.DataSource = list;
            ddl.DataTextField = "Value";
            ddl.DataValueField = "Key";
            ddl.DataBind();

            var li = ddl.Items.FindByText(selectText);
            if (li != null) li.Selected = true;
        }
    }
}
