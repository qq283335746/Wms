using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using TygaSoft.Model;
using TygaSoft.SysHelper;

namespace TygaSoft.WebHelper
{
    public class ResResult
    {
        public static ResResultInfo Response(bool isOk, string msg, params object[] data)
        {
            return new ResResultInfo { ResCode = isOk ? (int)EnumData.ResCode.成功 : (int)EnumData.ResCode.失败, Msg = msg, Data = data == null ? "" : data[0] };
        }

        public static string ResJsonString(bool isOk, string msg, params object[] data)
        {
            return JsonConvert.SerializeObject(new ResResultInfo { ResCode = isOk ? (int)EnumData.ResCode.成功 : (int)EnumData.ResCode.失败, Msg = msg, Data = data == null ? "" : data[0] });
        }

        public static string ResJsonString(ResResultInfo model)
        {
            return JsonConvert.SerializeObject(model);
        }
    }
}
