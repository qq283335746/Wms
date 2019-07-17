using System;
using TygaSoft.IMessaging;
using TygaSoft.MessagingFactory;
using TygaSoft.Model;

namespace TygaSoft.BLL
{
    public class RunQueueAsyn
    {
        private static readonly IRunQueue runQueue = QueueAccess.CreateRunQueue();

        public void Insert(RunQueueInfo model)
        {
            runQueue.Send(model);
        }

        public RunQueueInfo ReceiveFromQueue(int timeout)
        {
            return runQueue.Receive(timeout);
        }
    }
}
