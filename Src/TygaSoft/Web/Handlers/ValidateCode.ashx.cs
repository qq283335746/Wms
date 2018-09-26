using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using TygaSoft.SysHelper;

namespace TygaSoft.Web.Handlers
{
    /// <summary>
    /// ValidateCode 的摘要说明
    /// </summary>
    public class ValidateCode : IHttpHandler
    {
        Random rnd;

        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";

            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);//特别注意，如不加，单击验证图片＇看不清换一张＇，无效果．           
            this.CreateCheckCodeImage(GenerateCheckCode(context), context);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        #region 验证码开始

        private string GenerateCheckCode(HttpContext context)
        {
            string checkCode = String.Empty;
            rnd = new Random();

            char[] character = { '2', '3', '4', '5', '6', '8', '9', 'a', 'b', 'd', 'e', 'f', 'h', 'k', 'm', 'n', 'r', 'x', 'y', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'R', 'S', 'T', 'W', 'X', 'Y' };

            int charLen = character.Length;
            for (int i = 0; i < 4; i++)
            {
                checkCode += character[rnd.Next(charLen)];
            }

            string cookieName = GetCookieName(context);
            if (cookieName != "")
            {
                AESEncrypt aes = new AESEncrypt();
                HttpCookie cookie = new HttpCookie(cookieName, aes.EncryptString(checkCode));
                cookie.HttpOnly = true;
                //cookie.Expires = DateTime.Now.AddMinutes(5d); 注意：bug，设置Expires将导致获取不到cookie
                context.Response.Cookies.Add(cookie);
            }

            return checkCode;
        }

        private void CreateCheckCodeImage(string checkCode, HttpContext context)
        {
            int codeW = 80;
            int codeH = 38;
            int fontSize = 18;
            Color[] color = { Color.Black, Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Brown, Color.DarkBlue, Color.CadetBlue, Color.DarkCyan, Color.DodgerBlue, Color.LightSeaGreen, Color.Teal, Color.DarkSlateGray, Color.MediumBlue, Color.RoyalBlue };
            string[] font = { "Times New Roman", "Verdana", "Arial", "Gungsuh", "Impact" };
            Bitmap bmp = null;
            Graphics g = null;

            try
            {
                bmp = new Bitmap(codeW, codeH);
                g = Graphics.FromImage(bmp);
                g.Clear(Color.White);

                for (int i = 0; i < 1; i++)
                {
                    int x1 = rnd.Next(codeW);
                    int y1 = rnd.Next(codeH);
                    int x2 = rnd.Next(codeW);
                    int y2 = rnd.Next(codeH);
                    Color clr = color[rnd.Next(color.Length)];
                    g.DrawLine(new Pen(clr), x1, y1, x2, y2);
                }

                int codeLen = checkCode.Length;
                for (int i = 0; i < codeLen; i++)
                {
                    string fnt = font[rnd.Next(font.Length)];
                    Font ft = new Font(fnt, fontSize);
                    Color clr = color[rnd.Next(color.Length)];
                    g.DrawString(checkCode[i].ToString(), ft, new SolidBrush(clr), (float)i * 18 + 2, (float)0);
                }

                for (int i = 0; i < 100; i++)
                {
                    int x = rnd.Next(bmp.Width);
                    int y = rnd.Next(bmp.Height);
                    Color clr = color[rnd.Next(color.Length)];
                    bmp.SetPixel(x, y, clr);
                }
                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, ImageFormat.Png);

                context.Response.ClearContent();
                context.Response.ContentType = "image/Png";
                context.Response.BinaryWrite(ms.ToArray());
            }
            finally
            {
                g.Dispose();
                bmp.Dispose();
            }

        }

        /// <summary>
        /// 获取验证码存储的Cookie名称
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string GetCookieName(HttpContext context)
        {
            string cookieName = context.Request.QueryString["vcType"];
            if (!string.IsNullOrWhiteSpace(cookieName))
            {
                switch (cookieName)
                {
                    case "login":
                        return "Wms_LoginVc";
                    case "register":
                        return "Wms_RegisterVc";
                    case "pswReset":
                        return "Wms_PswResetVc";
                    case "addUser":
                        return "Wms_AddUserVc";
                    default:
                        return "";
                }
            }

            return "ChechCode";
        }

        #endregion 验证码结束
    }
}