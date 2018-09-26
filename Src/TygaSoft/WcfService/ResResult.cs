using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using TygaSoft.SysHelper;
using TygaSoft.WcfModel;

namespace TygaSoft.WcfService
{
    public class ResResult
    {
        public static ResResultModel Response(bool isOk, string msg, params object[] data)
        {
            return new ResResultModel { ResCode = isOk ? (int)EnumData.ResCode.成功 : (int)EnumData.ResCode.失败, Msg = msg, Data = data == null ? "" : data[0] };
        }

        public static string ResJsonString(bool isOk, string msg, params object[] data)
        {
            return JsonConvert.SerializeObject(new ResResultModel { ResCode = isOk ? (int)EnumData.ResCode.成功 : (int)EnumData.ResCode.失败, Msg = msg, Data = data == null ? "" : data[0] });
        }

        public static string ResJsonString(ResResultModel model)
        {
            return JsonConvert.SerializeObject(model);
        }
    }
}
