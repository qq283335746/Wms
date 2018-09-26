using System;
using System.Configuration;
using System.Runtime.Serialization;
using log4net;

namespace TygaSoft.SysException
{
    [Serializable()]
    public class CustomException : Exception,ISerializable
    {
        static ILog infoLog = log4net. LogManager.GetLogger("InfoLog");
        static ILog errorLog = LogManager.GetLogger("ErrorLog"); 

        public CustomException() { }

        public CustomException(string message)
            : base(message)
        {
            string msg = string.Format("{0}{1}{2}", message, base.Source, base.StackTrace);
            string method = string.Format("{0}", base.TargetSite == null ? "" : base.TargetSite.Name);

            infoLog.Info(string.Format("{0}{1}", msg, string.IsNullOrEmpty(method) ? "" : "    来源：" + method + ""));
        }

        public CustomException(string message, Exception innerException)
            : base(message, innerException)
        {
            string msg = string.Format("{0}{1}{2}", message, base.Source, base.StackTrace);
            string method = string.Format("{0}", base.TargetSite == null ? "" : base.TargetSite.Name);

            errorLog.ErrorFormat("{0}{1}", msg, string.IsNullOrEmpty(method) ? "" : "    来源：" + method + "");
        }

        protected CustomException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
