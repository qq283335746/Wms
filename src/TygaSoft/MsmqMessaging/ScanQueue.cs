using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Messaging;
using System.Text;
using TygaSoft.IMessaging;
using TygaSoft.Model;

namespace TygaSoft.MsmqMessaging
{
    public class ScanQueue : TygaSoftQueue, IScanQueue
    {
        private static readonly string queuePath = ConfigurationManager.AppSettings["WmsScanQueue"];
        private static int queueTimeout = 20;

        public ScanQueue()
            : base(queuePath, queueTimeout)
        {
            queue.Formatter = new BinaryMessageFormatter();
        }

        public new RunQueueInfo Receive()
        {
            base.transactionType = MessageQueueTransactionType.Automatic;
            return (RunQueueInfo)((Message)base.Receive()).Body;
        }

        public RunQueueInfo Receive(int timeout)
        {
            base.timeout = TimeSpan.FromSeconds(Convert.ToDouble(timeout));
            return Receive();
        }

        public void Send(RunQueueInfo model)
        {
            base.transactionType = MessageQueueTransactionType.Single;
            base.Send(model);
        }
    }
}
