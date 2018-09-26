using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace TygaSoft.WebHelper
{
    public class MessageBox
    {
        /// <summary>
        /// JEASYUI 弹出消息框
        /// </summary>
        /// <param name="page"></param>
        /// <param name="control"></param>
        /// <param name="msg"></param>
        /// <param name="title"></param>
        public static void MessagerShow(Page page, Control control, string msg)
        {
            msg = msg.Replace(@"'", @"“");
            ClientScriptManager csm = page.ClientScript;
            csm.RegisterClientScriptBlock(control.GetType(), control.ClientID, string.Format(@"$.messager.show({{title: '{1}', msg: '{0}',showType: 'slide',style: {{right: '', top: document.body.scrollTop + document.documentElement.scrollTop, bottom: ''}}}});", msg, "温馨提醒"), true);
        }

        /// <summary>
        /// JEASYUI 弹出消息框
        /// </summary>
        /// <param name="page"></param>
        /// <param name="control"></param>
        /// <param name="msg"></param>
        /// <param name="title"></param>
        public static void MessagerShow(Page page, Control control, string msg, string title)
        {
            msg = msg.Replace(@"'", @"“");
            ClientScriptManager csm = page.ClientScript;
            csm.RegisterClientScriptBlock(control.GetType(), control.ClientID, string.Format(@"$.messager.show({{title: '{1}', msg: '{0}',showType: 'slide',style: {{right: '', top: document.body.scrollTop + document.documentElement.scrollTop, bottom: ''}}}});", msg, title), true);
        }

        /// <summary>
        /// JEASYUI 弹出消息框
        /// </summary>
        /// <param name="page"></param>
        /// <param name="control"></param>
        /// <param name="msg"></param>
        public static void Messager(Page page, Control control, string msg)
        {
            msg = msg.Replace(@"'", @"“");
            ClientScriptManager csm = page.ClientScript;
            csm.RegisterClientScriptBlock(control.GetType(), control.ClientID, string.Format(@"$.messager.alert('温馨提醒','{0}');", msg), true);
        }

        /// <summary>
        /// JEASYUI 弹出消息框
        /// </summary>
        /// <param name="page"></param>
        /// <param name="control"></param>
        /// <param name="msg"></param>
        /// <param name="title"></param>
        public static void Messager(Page page, Control control, string msg, string title)
        {
            msg = msg.Replace(@"'", @"“");
            ClientScriptManager csm = page.ClientScript;
            csm.RegisterClientScriptBlock(control.GetType(), control.ClientID, string.Format(@"$.messager.alert('{0}','{1}','info');", title, msg), true);
        }

        /// <summary>
        /// JEASYUI 弹出消息框
        /// </summary>
        /// <param name="page"></param>
        /// <param name="control"></param>
        /// <param name="msg"></param>
        /// <param name="title"></param>
        /// <param name="icon"></param>
        public static void Messager(Page page, string msg, string title, string icon)
        {
            msg = msg.Replace(@"'", @"“");
            ClientScriptManager csm = page.ClientScript;
            csm.RegisterClientScriptBlock(page.GetType(), page.ClientID, string.Format(@"$.messager.alert('{1}','{0}','{2}');", msg, title, icon), true);
        }

        /// <summary>
        /// JEASYUI 弹出消息框
        /// </summary>
        /// <param name="page"></param>
        /// <param name="control"></param>
        /// <param name="msg"></param>
        /// <param name="title"></param>
        /// <param name="icon"></param>
        public static void Messager(Page page, Control control, string msg,string title,string icon)
        {
            msg = msg.Replace(@"'", @"“");
            ClientScriptManager csm = page.ClientScript;
            csm.RegisterClientScriptBlock(control.GetType(), control.ClientID, string.Format(@"$.messager.alert('{1}','{0}','{2}');", msg,title,icon), true);
        }

        /// <summary>
        /// JEASYUI 弹出消息框，并跳转
        /// </summary>
        /// <param name="page"></param>
        /// <param name="control"></param>
        /// <param name="msg"></param>
        /// <param name="url"></param>
        public static void Show(Page page, Control control, string msg, string url)
        {
            msg = msg.Replace(@"'", @"“");
            ClientScriptManager csm = page.ClientScript;
            csm.RegisterClientScriptBlock(control.GetType(), control.ClientID, string.Format(@"$.messager.show({{title: '{1}', msg: '{0}',showType: 'slide',style: {{right: '', top: document.body.scrollTop + document.documentElement.scrollTop, bottom: ''}}}});setTimeout(function(){{window.location = '{2}';}},1000)", msg, "温馨提醒", url), true);
        }
    }
}
