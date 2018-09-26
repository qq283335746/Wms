using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Configuration;
using Newtonsoft.Json;
using TygaSoft.SysException;
using TygaSoft.Model;
using TygaSoft.BLL;

namespace TygaSoft.TaskProcessor
{
    public class StockWarningTimer : BaseTask, ITask
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
                var spBll = new StockProduct();
                var swBll = new StockWarning();
                var pBll = new Product();

                while (true)
                {
                    TimeSpan tsTimeout = TimeSpan.FromSeconds(Convert.ToDouble(ConfigurationManager.AppSettings["StockWarningTimer"]));
                    var pageIndex = 1;
                    var pageSize = 10000;
                    var currTime = DateTime.Now;

                    while (true)
                    {
                        var spList = spBll.GetList(pageIndex,pageSize,"",null);
                        if (spList == null || spList.Count == 0)
                        {
                            pageIndex = 1;
                            break;
                        }

                        var swList = swBll.GetList();
                        if (swList == null || swList.Count == 0) break;

                        foreach (var item in spList)
                        {
                            List<string> oldWarnMsgList = null;
                            if (!string.IsNullOrWhiteSpace(item.WarnMsg))
                            {
                                oldWarnMsgList = item.WarnMsg.Split(new char[] { '，' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                            }
                            List<string> warnMsgList = new List<string>();

                            #region 库存报警条件

                            var pInfo = pBll.GetModel(item.ProductId);
                            if (pInfo.MaxStore > 0 && (item.Qty >= pInfo.MaxStore))
                            {
                                if (!warnMsgList.Contains("最大库存量预警")) warnMsgList.Add("最大库存量预警");
                            }
                            if (pInfo.MinStore > 0 && (item.Qty <= pInfo.MinStore))
                            {
                                if (!warnMsgList.Contains("最小库存量预警")) warnMsgList.Add("最小库存量预警");
                            }
                            if (!string.IsNullOrWhiteSpace(item.StockLocations))
                            {
                                var pslaList = JsonConvert.DeserializeObject<List<ProductStockLocationAttrInfo>>(item.StockLocations);
                                if (pslaList != null && pslaList.Count > 0)
                                {
                                    foreach (var pslaInfo in pslaList)
                                    {
                                        var swInfo = swList.FirstOrDefault(m => m.StockLocationId.Equals(pslaInfo.StockLocationId));
                                        if (swInfo == null) continue;

                                        if ((swInfo.MaxQty > 0) && (pslaInfo.Qty >= swInfo.MaxQty))
                                        {
                                            if (!warnMsgList.Contains("最大库存量预警")) warnMsgList.Add("最大库存量预警");
                                        }
                                        if ((swInfo.MinQty > 0) && (pslaInfo.Qty <= swInfo.MinQty))
                                        {
                                            if (!warnMsgList.Contains("最小库存量预警")) warnMsgList.Add("最小库存量预警");
                                        }
                                        if (swInfo.OverdueDay > 0)
                                        {
                                            if ((currTime - pslaInfo.LastUpdatedDate).TotalDays >= swInfo.OverdueDay)
                                            {
                                                if (!warnMsgList.Contains("超期预警")) warnMsgList.Add("超期预警");
                                            }
                                        }
                                    }
                                }
                            }

                            #endregion

                            if (warnMsgList.Count > 0) item.WarnMsg = string.Join("，", warnMsgList);
                            else item.WarnMsg = "";

                            #region 数据库操作

                            if (oldWarnMsgList == null && warnMsgList.Count > 0)
                            {
                                spBll.UpdateWarnMsg(item.ProductId, item.CustomerId, item.WarnMsg);
                            }
                            else if (oldWarnMsgList != null && oldWarnMsgList.Count > 0)
                            {
                                foreach (var s in oldWarnMsgList)
                                {
                                    if (warnMsgList.Contains(s)) warnMsgList.Remove(s);
                                }
                                if (warnMsgList.Count > 0)
                                {
                                    spBll.UpdateWarnMsg(item.ProductId, item.CustomerId, item.WarnMsg);
                                }
                                else
                                {
                                    foreach (var s in warnMsgList)
                                    {
                                        if (oldWarnMsgList.Contains(s)) oldWarnMsgList.Remove(s);
                                    }
                                    if (oldWarnMsgList.Count > 0)
                                    {
                                        spBll.UpdateWarnMsg(item.ProductId, item.CustomerId, item.WarnMsg);
                                    }
                                }
                            }

                            #endregion
                        }

                        pageIndex++;
                    }

                    Thread.Sleep(tsTimeout);
                }
            }
            catch (Exception ex)
            {
                new CustomException(string.Format("来自{0}异常：{1}", "StockWarningTimer", ex.Message), ex);
            }
        }
    }
}
