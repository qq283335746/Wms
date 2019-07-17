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
    public sealed class ScanQueue : BaseTask, ITask
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
                TimeSpan tsTimeout = TimeSpan.FromSeconds(Convert.ToDouble(transactionTimeout * batchSize));

                var bll = new ScanQueueAsyn();
                var taskBll = new DoTask();

                while (true)
                {
                    TimeSpan datetimeStarting = new TimeSpan(DateTime.Now.Ticks);
                    double elapsedTime = 0;

                    var list = new List<RunQueueInfo>();

                    for (int j = 0; j < batchSize; j++)
                    {
                        try
                        {
                            if ((elapsedTime + queueTimeout + transactionTimeout) < tsTimeout.TotalSeconds)
                            {
                                list.Add(bll.ReceiveFromQueue(queueTimeout));
                            }
                            else
                            {
                                j = batchSize;
                            }

                            elapsedTime = new TimeSpan(DateTime.Now.Ticks).TotalSeconds - datetimeStarting.TotalSeconds;
                        }
                        catch (TimeoutException)
                        {
                            j = batchSize;
                        }
                    }

                    if (list.Count > 0)
                    {
                        taskBll.DoRunQueue(list);
                    }
                }
            }
            catch (Exception ex)
            {
                new CustomException(string.Format("来自{0}异常：{1}", "ScanQueue", ex.Message), ex);
            }
        }
    }
}
