using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using TygaSoft.BLL;
using TygaSoft.Model;
using TygaSoft.SysException;

namespace TygaSoft.TaskProcessor
{
    public sealed class BarcodeScan : BaseTask, ITask
    {
        public void TaskStart()
        {
            var workThread = new Thread(new ThreadStart(WorkProcess));
            workThread.IsBackground = true;
            workThread.SetApartmentState(ApartmentState.STA);
            workThread.Start();
        }

        public void WorkProcess()
        {
            try
            {
                // the transaction timeout should be long enough to handle all of orders in the batch
                TimeSpan tsTimeout = TimeSpan.FromSeconds(Convert.ToDouble(transactionTimeout * batchSize));

                var bsBll = new BarcodeScanAsyn();
                var taskBll = new DoTask();

                while (true)
                {
                    // queue timeout variables
                    TimeSpan datetimeStarting = new TimeSpan(DateTime.Now.Ticks);
                    double elapsedTime = 0;

                    var listBarcode = new List<BarcodeTypeInfo>();

                    for (int j = 0; j < batchSize; j++)
                    {
                        try
                        {
                            //only receive more queued orders if there is enough time
                            if ((elapsedTime + queueTimeout + transactionTimeout) < tsTimeout.TotalSeconds)
                            {
                                listBarcode.Add(bsBll.ReceiveFromQueue(queueTimeout));
                            }
                            else
                            {
                                j = batchSize;   // exit loop
                            }

                            //update elapsed time
                            elapsedTime = new TimeSpan(DateTime.Now.Ticks).TotalSeconds - datetimeStarting.TotalSeconds;
                        }
                        catch (TimeoutException)
                        {
                            //exit loop because no more messages are waiting
                            j = batchSize;
                        }
                    }

                    if (listBarcode.Count > 0)
                    {
                        taskBll.DoBarcode(listBarcode);
                    }

                }
            }
            catch (Exception ex)
            {
                new CustomException(string.Format("来自{0}异常：{1}", "BarcodeScan", ex.Message), ex);
            }
        }
    }
}
