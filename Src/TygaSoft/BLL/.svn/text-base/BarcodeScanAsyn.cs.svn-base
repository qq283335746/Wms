using System;
using TygaSoft.IMessaging;
using TygaSoft.MessagingFactory;
using TygaSoft.Model;

namespace TygaSoft.BLL
{
    public class BarcodeScanAsyn
    {
        private static readonly IBarcodeType barcodeScanQueue = QueueAccess.CreateBarcodeType();

        public void Insert(BarcodeTypeInfo model)
        {
            barcodeScanQueue.Send(model);
        }

        public BarcodeTypeInfo ReceiveFromQueue(int timeout)
        {
            return barcodeScanQueue.Receive(timeout);
        }
    }
}
