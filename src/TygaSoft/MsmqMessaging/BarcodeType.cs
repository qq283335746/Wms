using System;
using System.Configuration;
using System.Messaging;
using TygaSoft.IMessaging;
using TygaSoft.Model;

namespace TygaSoft.MsmqMessaging
{
    public class BarcodeType : TygaSoftQueue, IBarcodeType
    {
        private static readonly string queuePath = ConfigurationManager.AppSettings["WmsBarcodeScanQueue"];
        private static int queueTimeout = 20;

        public BarcodeType()
            : base(queuePath, queueTimeout)
        {
            queue.Formatter = new BinaryMessageFormatter();
        }

        public new BarcodeTypeInfo Receive()
        {
            base.transactionType = MessageQueueTransactionType.Automatic;
            return (BarcodeTypeInfo)((Message)base.Receive()).Body;
        }

        public BarcodeTypeInfo Receive(int timeout)
        {
            base.timeout = TimeSpan.FromSeconds(Convert.ToDouble(timeout));
            return Receive();
        }

        public void Send(BarcodeTypeInfo model)
        {
            base.transactionType = MessageQueueTransactionType.Single;
            base.Send(model);
        }
    }
}
