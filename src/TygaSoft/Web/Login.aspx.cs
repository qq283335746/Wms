using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using TygaSoft.Model;
using TygaSoft.BLL;
using TygaSoft.WebHelper;
using TygaSoft.SysHelper;
using TygaSoft.CustomProvider;

namespace TygaSoft.Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Bind();
            }
            else
            {
                OnLogin();
            }
        }

        private void Bind()
        {
            var userName = string.Empty;
            try
            {
                if (Request.Cookies["Wms_UserInfo"] != null && !string.IsNullOrEmpty(Request.Cookies["Wms_UserInfo"].Value))
                {
                    var sLoginInfo = Request.Cookies["Wms_UserInfo"].Value;
                    AESEncrypt aes = new AESEncrypt();
                    var loginInfo = JsonConvert.DeserializeObject<LoginInfo>(aes.DecryptString(sLoginInfo));
                    userName = loginInfo.UserName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Messager(this.Page, Page.Controls[0], ex.Message, MC.AlertTitle_Sys_Info);
            }
            ltrMyData.Text = "<div id=\"myData\" style=\"display:none;\">{\"UserName\":\"" + userName + "\"}</div>";
        }

        private void OnLogin()
        {
            var fromLoginUrl = string.Empty;
            try
            {
                string userName = Request.Form["txtUserName"];
                string psw = Request.Form["txtPsw"];
                string sVc = Request.Form["txtVc"];

                if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(psw))
                {
                    throw new ArgumentException(MC.Login_InvalidAccount);
                }

                if (string.IsNullOrWhiteSpace(sVc))
                {
                    throw new ArgumentException(MC.Login_InvalidVC);
                }

                bool isRemember = Request.Form["cbRememberMe"] == "1" ? true : false;

                userName = userName.Trim();
                psw = psw.Trim();
                sVc = sVc.Trim();

                var cookie = Request.Cookies["Wms_LoginVc"];
                if (cookie == null || string.IsNullOrWhiteSpace(cookie.Value))
                {
                    throw new ArgumentException(MC.Login_InvalidVCCookie);
                }
                string validCode = cookie.Value;

                AESEncrypt aes = new AESEncrypt();

                if (sVc.ToLower() != aes.DecryptString(validCode).ToLower())
                {
                    throw new ArgumentException(MC.Login_InvalidVC);
                }
                if (!Regex.IsMatch(psw, Membership.PasswordStrengthRegularExpression))
                {
                    throw new ArgumentException(MC.Login_InvalidPassword);
                }

                #region 使用wcf身份认证服务

                //AuthenticationServiceClient authService = new AuthenticationServiceClient();
                //if (!authService.Login(userName, psw, "", true))
                //{
                //    throw new ArgumentException(MC.Login_InvalidUserNameAndPsw);
                //}

                #endregion

                #region 不使用wcf身份认证服务

                string userData = string.Empty;

                MembershipUser userInfo = Membership.GetUser(userName);
                if (!Membership.ValidateUser(userName, psw))
                {
                    if (userInfo == null)
                    {
                        throw new ArgumentException(EnumMembershipCreateStatus.GetStatusMessage(MembershipCreateStatus.InvalidUserName));
                    }
                    if (userInfo.IsLockedOut)
                    {
                        throw new ArgumentException(MC.Login_AccountLock);
                    }
                    if (!userInfo.IsApproved)
                    {
                        throw new ArgumentException(MC.Login_AccountAllow);
                    }
                    else
                    {
                        throw new ArgumentException(MC.Login_InvalidPsw);
                    }
                }

                userData = userInfo.ProviderUserKey.ToString();

                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userName, DateTime.Now, DateTime.Now.Add(FormsAuthentication.Timeout),
                        true, userData, FormsAuthentication.FormsCookiePath);
                string encTicket = FormsAuthentication.Encrypt(ticket);
                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

                //登录成功，则

                //bool isPersistent = true;
                //bool isRemember = true;
                //bool isAuto = false;
                //double d = 100;
                //if (cbRememberMe.Checked) isAuto = true;
                //自动登录 设置时间为7天
                //if (isAuto) d = 10080;

                #endregion

                if (isRemember)
                {
                    var loginInfo = new LoginInfo(userName, DateTime.Now);
                    var sUserInfo = aes.EncryptString(JsonConvert.SerializeObject(loginInfo));
                    Response.Cookies.Add(new HttpCookie("Wms_UserInfo", sUserInfo));
                }
                else
                {
                    Response.Cookies.Add(new HttpCookie("Wms_UserInfo", ""));
                }

                //fromLoginUrl = FormsAuthentication.GetRedirectUrl(userName, true);
                fromLoginUrl = "~/u/t.html";
            }
            catch (Exception ex)
            {
                MessageBox.Messager(this.Page, Page.Controls[0], ex.Message, MC.AlertTitle_Sys_Info);
                return;
            }

            if (!string.IsNullOrEmpty(fromLoginUrl)) Response.Redirect(fromLoginUrl);

            //FormsAuthentication.RedirectFromLoginPage(userName, true);//使用此行会清空ticket中的userData ？！！！
        }
    }
}