using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using TygaSoft.SysHelper;
using TygaSoft.WebHelper;
using TygaSoft.Model;
using TygaSoft.BLL;

namespace TygaSoft.Web.Handlers
{
    /// <summary>
    /// HandlerMap 的摘要说明
    /// </summary>
    public class HandlerMap : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json; charset=utf-8";
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
                reqName = reqName.Trim();

                switch (reqName)
                {
                    case "GetOrderMapList":
                        GetOrderMapList(context);
                        break;
                    case "GetLocation":
                        GetLocation(context);
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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private void GetOrderMapList(HttpContext context)
        {
            var bll = new OrderMap();
            context.Response.Write(ResResult.ResJsonString(true, "", bll.GetList()));
        }

        private void GetLocation(HttpContext context)
        {
            context.Response.Write(ResResult.ResJsonString(true, "", MapHelper.GetLocationByIP(HttpClientHelper.GetClientIp(context))));
        }
    }
}