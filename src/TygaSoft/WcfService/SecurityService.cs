using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Security;
using System.Web;
using System.Transactions;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using TygaSoft.CustomProvider;
using TygaSoft.SysHelper;
using TygaSoft.Model;
using TygaSoft.WcfModel;
using TygaSoft.BLL;

using TygaSoft.WebHelper;

namespace TygaSoft.WcfService
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class SecurityService:ISecurity
    {
        #region 系统管理

        #region 用户角色管理

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SaveRole(RoleModel model)
        {
            try
            {
                if (!HttpContext.Current.User.IsInRole("Administrators")) throw new ArgumentException(MC.Role_InvalidError);

                model.RoleName = model.RoleName.Trim();
                if (string.IsNullOrEmpty(model.RoleName))
                {
                    return ResResult.Response(false, MC.Submit_Params_InvalidError, "");
                }

                if (Roles.RoleExists(model.RoleName))
                {
                    return ResResult.Response(false, MC.Submit_Exist, "");
                }

                Guid gId = Guid.Empty;
                if (model.RoleId != null)
                {
                    Guid.TryParse(model.RoleId.ToString(), out gId);
                }

                var bll = new Role();
                var modelInfo = new RoleInfo(gId, model.RoleName, model.UserName, model.IsInRole);

                if (!gId.Equals(Guid.Empty))
                {
                    if (modelInfo.RoleName == "Administrators"|| modelInfo.RoleName == "System" || modelInfo.RoleName == "Users" || modelInfo.RoleName == "Guest") return ResResult.Response(false, MC.M_SysDataChangedError, "");

                    //MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.编辑, true);

                    bll.Update(modelInfo);
                }
                else
                {
                    //MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.新增, true);

                    Roles.CreateRole(model.RoleName);
                }

                return ResResult.Response(true, "", "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SaveUser(UserModel model)
        {
            try
            {
                if (!HttpContext.Current.User.IsInRole("Administrators")) throw new ArgumentException(MC.Role_InvalidError);

                if (string.IsNullOrWhiteSpace(model.UserName) || string.IsNullOrWhiteSpace(model.Password))
                {
                    return ResResult.Response(false, MC.Submit_Params_InvalidError, "");
                }
                if (model.Password != model.CfmPsw)
                {
                    return ResResult.Response(false, MC.Request_InvalidCompareToPassword, "");
                }
                model.UserName = model.UserName.Trim();
                model.Password = model.Password.Trim();
                if (!Regex.IsMatch(model.Password, Membership.PasswordStrengthRegularExpression))
                {
                    return ResResult.Response(false, MC.Login_InvalidPassword, "");
                }
                if (string.IsNullOrWhiteSpace(model.Email))
                {
                    model.Email = model.UserName + "@tygaweb.com";
                }

                model.RoleName = model.RoleName.Trim().Trim(',');
                string[] roles = null;
                if (!string.IsNullOrEmpty(model.RoleName))
                {
                    roles = model.RoleName.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                }

                MembershipCreateStatus status;
                MembershipUser user;

                user = Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, model.IsApproved, out status);
                if (roles != null && roles.Length > 0)
                {
                    Roles.AddUserToRoles(model.UserName, roles);
                }

                //using (TransactionScope scope = new TransactionScope())
                //{
                //    user = Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, model.IsApproved, out status);
                //    if (roles != null && roles.Length > 0)
                //    {
                //        Roles.AddUserToRoles(model.UserName, roles);
                //    }

                //    scope.Complete();
                //}

                if (user == null)
                {
                    return ResResult.Response(false, EnumMembershipCreateStatus.GetStatusMessage(status), "");
                }

                return ResResult.Response(true, "调用成功", "");
            }
            catch (MembershipCreateUserException ex)
            {
                return ResResult.Response(false, EnumMembershipCreateStatus.GetStatusMessage(ex.StatusCode), "");
            }
            catch (HttpException ex)
            {
                return ResResult.Response(false, "" + MC.AlertTitle_Ex_Error + "：" + ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SaveUserInRole(string userName, string roleName, bool isInRole)
        {
            try
            {
                if (!HttpContext.Current.User.IsInRole("Administrators")) throw new ArgumentException(MC.Role_InvalidError);

                if (string.IsNullOrWhiteSpace(userName))
                {
                    return ResResult.Response(false, MC.GetString(MC.Request_InvalidArgument, "用户名"), "");
                }
                if (string.IsNullOrWhiteSpace(roleName))
                {
                    return ResResult.Response(false, MC.GetString(MC.Request_InvalidArgument, "角色"), "");
                }

                if (isInRole)
                {
                    if (!Roles.IsUserInRole(userName, roleName))
                    {
                        Roles.AddUserToRole(userName, roleName);
                    }
                }
                else
                {
                    if (Roles.IsUserInRole(userName, roleName))
                    {
                        Roles.RemoveUserFromRole(userName, roleName);
                    }
                }

                return ResResult.Response(true, "调用成功", "");
            }
            catch (System.Configuration.Provider.ProviderException pex)
            {
                return ResResult.Response(false, pex.Message, "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel DelRole(string itemAppend)
        {
            try
            {
                if (!HttpContext.Current.User.IsInRole("Administrators")) throw new ArgumentException(MC.Role_InvalidError);

                itemAppend = itemAppend.Trim();
                if (string.IsNullOrEmpty(itemAppend))
                {
                    return ResResult.Response(false, MC.Submit_InvalidRow, "");
                }

                string[] roles = itemAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if(roles.Contains("Administrators") || roles.Contains("System") || roles.Contains("Users") || roles.Contains("Guest")) return ResResult.Response(false, MC.M_SysDataChangedError, "");

                foreach (string item in roles)
                {
                    Roles.DeleteRole(item);
                }

                return ResResult.Response(true, "调用成功", "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SaveIsLockedOut(string userName)
        {
            try
            {
                if (!HttpContext.Current.User.IsInRole("Administrators")) throw new ArgumentException(MC.Role_InvalidError);

                MembershipUser user = Membership.GetUser(userName);
                if (user == null)
                {
                    return ResResult.Response(false, "当前用户不存在，请检查", "");
                }
                if (user.IsLockedOut)
                {
                    if (user.UnlockUser())
                    {
                        return ResResult.Response(false, "", "0");
                    }
                    else
                    {
                        return ResResult.Response(false, "操作失败，请联系管理员", "");
                    }
                }

                return ResResult.Response(false, "只有“已锁定”的用户才能执行此操作", "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SaveIsApproved(string userName)
        {
            try
            {
                if (!HttpContext.Current.User.IsInRole("Administrators")) throw new ArgumentException(MC.Role_InvalidError);

                MembershipUser user = Membership.GetUser(userName);
                if (user == null)
                {
                    return ResResult.Response(false, "当前用户不存在，请检查", "");
                }
                if (user.IsApproved)
                {
                    user.IsApproved = false;
                }
                else
                {
                    user.IsApproved = true;
                }

                Membership.UpdateUser(user);

                return ResResult.Response(user.IsApproved, user.IsApproved ? "调用成功" : "", user.IsApproved ? "1" : "0");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetRolesForUser(string userName)
        {
            try
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.浏览, true);

                string[] roles = Roles.GetRolesForUser(userName);
                if (roles.Length == 0) return ResResult.Response(false, "", "");

                return ResResult.Response(true, "调用成功", string.Join(",", roles));
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetUsersInRole(string roleName)
        {
            try
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.浏览, true);

                string[] users = Roles.GetUsersInRole(roleName);
                if (users.Length == 0) return ResResult.Response(false, "", "");
                var list = new List<ComboboxInfo>();
                foreach (var item in users)
                {
                    list.Add(new ComboboxInfo(item, item));
                }

                return ResResult.Response(true, "", "{\"total\":" + list.Count + ",\"rows\":" + JsonConvert.SerializeObject(list) + "}");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel DelUser(string userName)
        {
            try
            {
                if (!HttpContext.Current.User.IsInRole("Administrators")) throw new ArgumentException(MC.Role_InvalidError);

                if (!Membership.DeleteUser(userName)) return ResResult.Response(false, MC.M_Save_Error, "");

                return ResResult.Response(true, "", "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, "" + MC.AlertTitle_Ex_Error + "：" + ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel ResetPassword(string username)
        {
            try
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.编辑, true);

                if (!Membership.EnablePasswordReset)
                {
                    return ResResult.Response(false, "系统不允许重置密码操作，请联系管理员", "");
                }
                var user = Membership.GetUser(username);
                if (user == null)
                {
                    return ResResult.Response(false, "用户【" + username + "】不存在或已被删除，请检查", "");
                }
                string rndPsw = new Random().Next(100000, 999999).ToString();
                if (!user.ChangePassword(user.ResetPassword(), rndPsw))
                {
                    return ResResult.Response(false, "重置密码失败，请稍后再重试", "");
                }

                return ResResult.Response(true, "调用成功", rndPsw);
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel CheckUserName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                return ResResult.Response(false, "参数不能为空字符串", "-1");
            }

            try
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.浏览, true);

                MembershipUser user = Membership.GetUser(userName);
                if (user != null)
                {
                    return ResResult.Response(true, "调用成功", 1);
                }

                return ResResult.Response(true, "调用成功", 0);
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetUserList(ListModel model)
        {
            try
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.浏览, true);

                var totalRecord = 0;
                var users = Membership.GetAllUsers((model.PageIndex-1),model.PageSize,out totalRecord);
                var list = new List<ComboboxInfo>();
                foreach (MembershipUser user in users)
                {
                    list.Add(new ComboboxInfo(user.ProviderUserKey.ToString(), user.UserName));
                }
                return ResResult.Response(true, "", "{\"total\":" + totalRecord + ",\"rows\":" + JsonConvert.SerializeObject(list) + "}");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }

        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel ChangePassword(string username, string oldPassword, string password)
        {
            try
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.编辑, true);

                if (string.IsNullOrWhiteSpace(username)) username = HttpContext.Current.User.Identity.Name;
                if (!Regex.IsMatch(password, Membership.PasswordStrengthRegularExpression)) return ResResult.Response(false, MC.Login_InvalidPassword, "");
                if (!Membership.ValidateUser(username, oldPassword)) return ResResult.Response(false, MC.Login_InvalidOldPsw, "");
                if (!Membership.GetUser(username).ChangePassword(oldPassword, password)) return ResResult.Response(false, MC.M_Save_Error, "");

                return ResResult.Response(true, "", "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        #endregion

        #region 菜单管理

        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public ResResultModel GetMenusChildrenByParentName(string parentName)
        {
            try
            {
                var userMenuList = MenusDataProxy.GetUserMenus();
                if (userMenuList == null || userMenuList.Count() == 0) return ResResult.Response(false, MC.Data_InvalidExist, "");
                if (string.IsNullOrWhiteSpace(parentName) || HttpContext.Current.User.IsInRole("Administrators"))
                {
                    parentName = "首页";
                }
                var parentInfo = userMenuList.FirstOrDefault(m => (m.Title.Contains(parentName)));
                if (parentInfo == null) return ResResult.Response(false, MC.Data_InvalidExist, "");
                var childData = userMenuList.Where(m => (m.ParentId == parentInfo.Id) && m.Descr.IndexOf("hide") == -1);
                if (childData == null) return ResResult.Response(false, MC.Data_InvalidExist, "");

                return ResResult.Response(true, "", JsonConvert.SerializeObject(childData));
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public ResResultModel GetMenusTreeChildrenByParentId(Guid parentId)
        {
            try
            {
                var userMenuList = MenusDataProxy.GetUserMenus();
                if (userMenuList == null || userMenuList.Count() == 0) return ResResult.Response(false, MC.Data_InvalidExist, "");
                var parentInfo = userMenuList.FirstOrDefault(m => m.Id.Equals(parentId));
                if (parentInfo == null) return ResResult.Response(false, MC.Data_InvalidExist, "");
                var sb = new StringBuilder();
                var bll = new SiteMenus();
                bll.CreateTreeJson(userMenuList, parentInfo.Id, ref sb);

                return ResResult.Response(true, "", sb.ToString());
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public ResResultModel GetMenusTree()
        {
            try
            {
                var bll = new SiteMenus();
                return ResResult.Response(true, "", bll.GetTreeJson(Membership.ApplicationName));
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SaveMenus(MenusModel model)
        {
            try
            {
                if (!HttpContext.Current.User.IsInRole("Administrators")) throw new ArgumentException(MC.Role_InvalidError);

                if (model == null) return ResResult.Response(false, MC.Request_Params_InvalidError);
                if (string.IsNullOrWhiteSpace(model.Title)) return ResResult.Response(false, MC.Request_Params_InvalidError);
                var Id = Guid.Empty;
                var parentId = Guid.Empty;
                if (model.Id != null && !string.IsNullOrWhiteSpace(model.Id.ToString())) Guid.TryParse(model.Id.ToString(), out Id);
                if (model.ParentId != null && !string.IsNullOrWhiteSpace(model.ParentId.ToString())) Guid.TryParse(model.ParentId.ToString(), out parentId);

                var appBll = new Applications();
                var appId = appBll.GetAspnetAppId(Membership.ApplicationName);

                var bll = new SiteMenus();
                int effect = 0;

                var modelInfo = new SiteMenusInfo(Guid.Parse(appId.ToString()), Id, parentId, model.IdStep, model.Title, model.Url, model.Descr, model.Sort, DateTime.Now);

                if (Id.Equals(Guid.Empty))
                {
                    //MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.新增, true);

                    modelInfo.Id = Guid.NewGuid();
                    modelInfo.IdStep = (modelInfo.Id + "," + modelInfo.IdStep).Trim(',');
                    effect = bll.InsertByOutput(modelInfo);
                }
                else
                {
                    //MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.编辑, true);
                    var oldInfo = bll.GetModel(Id);
                    effect = bll.Update(modelInfo);
                }
                if (effect < 1) return ResResult.Response(false, "操作失败，数据库操作异常");

                return ResResult.Response(true, "操作成功", modelInfo.Id);
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, "操作异常：" + ex.Message + "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel DeleteMenus(Guid Id)
        {
            try
            {
                //MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.删除, true);
                if (!HttpContext.Current.User.IsInRole("Administrators")) throw new ArgumentException(MC.Role_InvalidError);

                if (Id.Equals(Guid.Empty)) return ResResult.Response(false, "参数值无效");

                var bll = new SiteMenus();

                bll.Delete(Id);
                return ResResult.Response(true, "", "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetMenusTreeGrid(MenusPermissionModel model)
        {
            try
            {
                var accessId = Guid.Empty;
                var accessType = "";
                var isAdministrators = false;
                if (!string.IsNullOrWhiteSpace(model.AllowRole))
                {
                    accessType = "Roles";
                    SiteRoles rBll = new SiteRoles();
                    var roleInfo = rBll.GetAspnetModel(Membership.ApplicationName, model.AllowRole);
                    accessId = roleInfo.Id;
                    isAdministrators = roleInfo.LowerName == "administrators";
                }
                if (!string.IsNullOrWhiteSpace(model.DenyUser))
                {
                    accessType = "Users";
                    accessId = Guid.Parse(Membership.GetUser(model.DenyUser).ProviderUserKey.ToString());
                }
                var bll = new SiteMenus();
                return ResResult.Response(true, "", bll.GetTreeGridJson(Membership.ApplicationName, accessId, accessType, isAdministrators));
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        #endregion

        #endregion

        #region 私有

        #endregion
    }
}
