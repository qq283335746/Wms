using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TygaSoft.SysHelper
{
    public static class MC
    {
        public static string GetString(string strString)
        {
            return strString;
        }
        public static string GetString(string strString, string param1)
        {
            return string.Format(strString, param1);
        }
        public static string GetString(string strString, string param1, string param2)
        {
            return string.Format(strString, param1, param2);
        }
        public static string GetString(string strString, string param1, string param2, string param3)
        {
            return string.Format(strString, param1, param2, param3);
        }

        public const string M_OrderExistProductError = "该单号存在货物，请先删除该单号的货物后再执行此操作！";
        public const string M_Save_Ok = "操作成功！";
        public const string M_Save_Error = "操作失败，请稍后再重试！";
        public const string M_Order_NotExist = "请先保存单据信息！";
        public const string M_RuleInvalidError = "不符合规则，无法操作！";
        public const string M_QtyInvalidError = "数量超出了范围！";
        public const string M_NotExistDetailError = "未找到任何相关明细数据，请正确操作！";
        public const string M_NotConfigError = "未找到相关配置，请检查！";
        public const string M_SysDataChangedError = "当前操作包含系统预设的数据，不能更改系统预设的数据！";
        public const string M_StockProductInvalidError = "库存数据存在异常，请检查！";

        public const string Params_OrderProductInvalidError = "单号为“{0}”中存在未处理的数据行，请检查！";
        public const string Params_Order_ProductExist = "货物“{0}”已存在其它收货单内！";
        public const string Params_Order_StepError = "货物“{0}”已经{1}！";
        public const string Params_ExistProductError = "货物“{0}”已存在！";
        public const string Params_OrderProductPassError = "货物“{0}”数量“{1}”超出了范围，请核对已收货量、已上架量！";
        public const string Params_ProductPassError = "货物“{0}”数量“{1}”超出了范围！";
        public const string Params_OrderProductDeleteError = "货物“{0}”已经{1}，无法删除！";
        public const string Params_CodeExistError = "编号为“{0}”已经存在，请务必保持唯一值！";
        public const string Params_Data_NotExist = "“{0}”对应数据不存在或已被删除！";
        public const string Params_SwitchNameNotExist = "找不到名称为“{0}”！";
        public const string Params_Step_BeforeShelfMission = "货物编号为“{0}”需要先做收货后再上架，请检查！";
        public const string Params_ExistDetailError = "{0}存在明细信息，请先删除明细信息后再删除该数据！";
        public const string Params_SaveRoleAccessError = "不能对角色为“{0}”进行此操作！";
        public const string Params_SaveUserAccessError = "用户“{0}”属于“Administrators”的角色，说明该用户已具有“Administrators”的所有权限！";
        public const string Params_FeatureUserExistError = "用户“{0}”已分配，不能重复对一个用户多次分配！";

        public const string Data_NotExist = "数据不存在或已被删除！";
        public const string Data_InvalidExist = "暂无数据，可能原因：当前登录用户无权限访问或数据不存在！";

        public const string Submit_Exist = "已存在相同数据记录，请勿重复提交！";
        public const string Submit_Params_InvalidError = "有“*”标识的为必填项，请检查！";
        public const string Submit_Params_InvalidExist = "当前{0}不存在，请核对后再提交！";
        public const string Submit_Params_InvalidEnough = "当前{0}不足，请核对后再提交！";
        public const string Submit_Params_GetInvalidRegex = "获取{0}的值格式不正确，请检查！";
        public const string Submit_Params_InvalidRegex = "{0}输入值格式不正确，请检查！";
        public const string Submit_InvalidRow = "请至少勾选一行进行操作！";
        public const string Submit_Ex_Params_InvalidExist = "系统异常，原因：当前{0}不存在，请联系管理员！";
        public const string Submit_Ex_Error = "系统异常，原因：{0}";

        public const string AlertTitle_Info = "温馨提醒";
        public const string AlertTitle_Error = "错误提示";
        public const string AlertTitle_Sys_Info = "系统提示";
        public const string AlertTitle_Ex_Error = "异常提示";

        public const string Request_InvalidError = "非法操作，已禁止执行！";
        public const string Request_InvalidStaffBasic = "请先完善人员基本信息！";
        public const string Request_InvalidArgument = "获取{0}的值为空字符串或格式不正确，请检查！";
        public const string Request_NotExist = "{0}不存在或已被删除！";
        public const string Request_InvalidCompareToPassword = "前后输入密码不一致！";
        public const string Request_Params_InvalidError = "请求参数值不正确！";
        public const string Request_InvalidQty = "数量“{0}”超出了范围！";

        public const string Response_Ok = "调用成功！";

        public const string Role_InvalidError = "对不起，您木有操作权限，请联系管理员！";

        public const string Import_InvalidError = "带有“*”的列为必填项，请正确操作";
        public const string Import_NotDataError = "无任何可导入的数据，请下载导入模板并填写信息后再导入！";

        public const string Login_InvalidAccount = "用户名或密码不正确！";
        public const string Login_InvalidVC = "验证码不正确！";
        public const string Login_InvalidVCCookie = "验证码不存在或已过期！";
        public const string Login_InvalidPsw = "密码不正确！";
        public const string Login_InvalidOldPsw = "当前密码不正确！";
        public const string Login_AccountLock = "您的账号已被锁定，请联系管理员先解锁后才能登录！";
        public const string Login_AccountAllow = "您的帐户尚未获得批准，请联系管理员！";
        public const string Login_InvalidPassword = "密码应由数字、字母、特殊字符（不包括“&”）组成，且是6-30位的字符串！";
    }
}
