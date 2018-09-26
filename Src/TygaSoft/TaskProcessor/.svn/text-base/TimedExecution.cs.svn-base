using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using TygaSoft.SysException;
using TygaSoft.BLL;

namespace TygaSoft.TaskProcessor
{
    public class TimedExecution : BaseTask, ITask
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
            while (true)
            {
                try
                {
                    var currTime = DateTime.Now;
                    var sShortTime = currTime.ToString("yyyy-MM-dd");
                    if (currTime.Hour == 4)
                    {
                        //BarcodeScan bll = new BarcodeScan();
                        //bll.DeleteAgoData(3);
                        var ts = DateTime.Parse(string.Format("{0} {1}", currTime.AddDays(1).ToString("yyyy-MM-dd"), "04:00:00")) - currTime;
                        Thread.Sleep(ts);
                    }
                    else
                    {
                        if (currTime.Hour < 4)
                        {
                            var ts = DateTime.Parse(string.Format("{0} {1}", currTime.ToString("yyyy-MM-dd"), "04:00:00")) - currTime;
                            Thread.Sleep(ts);
                        }
                        else
                        {
                            var ts = DateTime.Parse(string.Format("{0} {1}", currTime.AddDays(1).ToString("yyyy-MM-dd"), "04:00:00")) - currTime;
                            Thread.Sleep(ts);
                        }
                    }
                }
                catch (Exception ex)
                {
                    new CustomException(ex.Message, ex);
                }
            }
        }
    }
}
