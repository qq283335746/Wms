using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Security;

namespace TygaSoft.CustomProvider
{
    public class EnumMembershipCreateStatus
    {
        public static string GetStatusMessage(MembershipCreateStatus status)
        {
            switch (status)
            {
                case MembershipCreateStatus.Success:
                    return "创建用户成功。";
                case MembershipCreateStatus.InvalidUserName:
                    return "在数据库中未找到用户名。";
                case MembershipCreateStatus.InvalidPassword:
                    return "密码的格式设置不正确。";
                case MembershipCreateStatus.InvalidQuestion:
                    return "密码提示问题的格式设置不正确。";
                case MembershipCreateStatus.InvalidAnswer:
                    return "密码提示问题答案的格式设置不正确。";
                case MembershipCreateStatus.InvalidEmail:
                    return "电子邮件地址的格式设置不正确。";
                case MembershipCreateStatus.DuplicateUserName:
                    return "用户名已存在于应用程序的数据库中。";
                case MembershipCreateStatus.DuplicateEmail:
                    return "电子邮件地址已存在于应用程序的数据库中。";
                case MembershipCreateStatus.UserRejected:
                    return "因为提供程序定义的某个原因而未创建用户。";
                case MembershipCreateStatus.InvalidProviderUserKey:
                    return "提供程序用户键值的类型或格式无效。";
                case MembershipCreateStatus.DuplicateProviderUserKey:
                    return "提供程序用户键值已存在于应用程序的数据库中。";
                case MembershipCreateStatus.ProviderError:
                    return "提供程序返回一个未由其他 MembershipCreateStatus 枚举值描述的错误。";
                default:
                    return "未知错误！";
            }
        }
    }
}
