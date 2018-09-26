using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;

namespace TygaSoft.Web.Manages
{
    public partial class DlgPictureUpload : System.Web.UI.Page
    {
        string dlgId;
        string dlgParentId;
        string submitUrl;
        string funName;
        bool isMutilSelect;
        string callBack;
        StringBuilder myDataAppend;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(Request.QueryString["dlgId"]))
                {
                    dlgId = Request.QueryString["dlgId"].Trim();
                }
                if (!string.IsNullOrWhiteSpace(Request.QueryString["dlgParentId"]))
                {
                    dlgParentId = Request.QueryString["dlgParentId"].Trim();
                }
                if (!string.IsNullOrWhiteSpace(Request.QueryString["submitUrl"]))
                {
                    submitUrl = Request.QueryString["submitUrl"].Trim();
                }
                if (!string.IsNullOrWhiteSpace(Request.QueryString["funName"]))
                {
                    funName = Request.QueryString["funName"].Trim();
                }
                if (!string.IsNullOrWhiteSpace(Request.QueryString["isMutil"]))
                {
                    isMutilSelect = Request.QueryString["isMutil"].Trim().ToLower() == "true";
                }
                if (!string.IsNullOrWhiteSpace(Request.QueryString["callBack"]))
                {
                    callBack = Request.QueryString["callBack"].Trim();
                }

                myDataAppend = new StringBuilder(250);

                myDataAppend.Append(File.ReadAllText(Server.MapPath("~/Templates/JsPictureUpload.txt")));
                myDataAppend.Replace("{DlgId}", dlgId);
                myDataAppend.Replace("{DlgParentId}", dlgParentId);
                //myDataAppend.Replace("{DlgParentHref}", "/t/tpicture.html?dlgId=" + dlgParentId + "&funName=" + funName + "&isMutil=" + isMutilSelect + "");
                myDataAppend.Replace("{SubmitUrl}", submitUrl);
                myDataAppend.Replace("{FunName}", funName);
                if (string.IsNullOrWhiteSpace(callBack))
                {
                    myDataAppend.Replace("{CallBack}", string.Format("$(\"#{0}\").dialog('refresh','{1}');", dlgParentId, "/t/tpicture.html?dlgId=" + dlgParentId + "&funName=" + funName + "&isMutil=" + isMutilSelect + ""));
                }
                else
                {
                    myDataAppend.Replace("{CallBack}", string.Format("{0};", callBack));
                }

                ltrMyData.Text = myDataAppend.ToString();
            }
        }
    }
}