using SelectPdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TygaSoft.Model;
using TygaSoft.WebHelper;

namespace TygaSoft.Web.Handlers
{
    /// <summary>
    /// HandlerFile 的摘要说明
    /// </summary>
    public class HandlerFile : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string reqName = "";
                switch (context.Request.HttpMethod.ToUpper())
                {
                    case "GET":
                        reqName = context.Request.QueryString["ReqName"];
                        break;
                    case "POST":
                        reqName = context.Request.Form["ReqName"];
                        break;
                    default:
                        break;
                }
                if (string.IsNullOrWhiteSpace(reqName)) return;

                switch (reqName.Trim())
                {
                    case "HtmlToPdf":
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                context.Response.Write(ResResult.ResJsonString(false, ex.Message, ""));
            }
        }

        #region 私有方法

        #endregion

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}