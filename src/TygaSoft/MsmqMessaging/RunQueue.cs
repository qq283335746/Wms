using System;
using System.Configuration;
using System.Messaging;
using TygaSoft.IMessaging;
using TygaSoft.Model;

namespace TygaSoft.MsmqMessaging
{
    public class RunQueue : TygaSoftQueue, IRunQueue
    {
        private static readonly string queuePath = ConfigurationManager.AppSettings["WmsRunQueue"];
        private static int queueTimeout = 20;

        public RunQueue()
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
