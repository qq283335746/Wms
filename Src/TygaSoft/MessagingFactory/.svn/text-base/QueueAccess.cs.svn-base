using System;
using System.Configuration;
using System.Reflection;
using TygaSoft.IMessaging;

namespace TygaSoft.MessagingFactory
{
    public sealed class QueueAccess
    {
        private static readonly string path = ConfigurationManager.AppSettings["MsmqMessaging"];

        private QueueAccess() { }

        public static IScanQueue CreateScanQueue()
        {
            string className = path + ".ScanQueue";
            return (IScanQueue)Assembly.Load(path).CreateInstance(className);
        }

        public static IBarcodeType CreateBarcodeType()
        {
            string className = path + ".BarcodeType";
            return (IBarcodeType)Assembly.Load(path).CreateInstance(className);
        }

        public static IRunQueue CreateRunQueue()
        {
            string className = path + ".RunQueue";
            return (IRunQueue)Assembly.Load(path).CreateInstance(className);
        }
    }
}
