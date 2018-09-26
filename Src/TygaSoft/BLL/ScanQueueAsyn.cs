using System;
using TygaSoft.IMessaging;
using TygaSoft.MessagingFactory;
using TygaSoft.Model;

namespace TygaSoft.BLL
{
    public class ScanQueueAsyn
    {
        private static readonly IScanQueue scanQueue = QueueAccess.CreateScanQueue();

        public void Insert(RunQueueInfo model)
        {
            scanQueue.Send(model);
        }

        public RunQueueInfo ReceiveFromQueue(int timeout)
        {
            return scanQueue.Receive(timeout);
        }
    }
}
