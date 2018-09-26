using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using TygaSoft.SysException;
using TygaSoft.SysHelper;
using TygaSoft.Model;

namespace TygaSoft.BLL
{
    public class DoTask
    {
        #region 命令行运行

        public void DoRunQueue(List<RunQueueInfo> list)
        {
            foreach (var model in list)
            {
                if (model.RunType == EnumData.EnumRunQueue.统计单总数.ToString())
                {
                    SetOrderTotalProduct(model);
                }
                if (model.RunType == EnumData.EnumRunQueue.RFID.ToString())
                {
                    new CustomException(string.Format("list.Count--：{0}", list.Count));
                    SetRFID(model);
                }
            }
        }

        private void SetOrderTotalProduct(RunQueueInfo model)
        {
            try
            {
                switch (model.RunName)
                {
                    case "ShelfMission":
                        var smBll = new ShelfMission();
                        smBll.SetTotalProduct(model.RunValue);
                        break;
                    case "OrderPicked":
                        var opBll = new OrderPicked();
                        opBll.SetTotalProduct(model.RunValue);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                new CustomException(ex.Message, ex);
            }
        }

        #endregion

        public void DoBarcode(List<BarcodeTypeInfo> list)
        {
            foreach (var model in list)
            {
                if (model.From == EnumData.EnumStep.上架.ToString())
                {
                    DoShelfMissionProduct(model);
                }
                
            }
        }

        private void DoShelfMissionProduct(BarcodeTypeInfo model)
        {
            try
            {
                ShelfMissionProductQueueInfo item = JsonConvert.DeserializeObject<ShelfMissionProductQueueInfo>(model.TypeBody);

                var spBll = new StockProduct();
                var spInfo = spBll.GetModel(Guid.Empty, item.ProductId);
                if (spInfo == null) throw new ArgumentException(string.Format("货品（{0}）未入库！", item.ProductId));
                var stepList = spInfo.StepCode.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                var currStepCode = ((int)EnumData.EnumStep.上架).ToString();
                var currStepName = EnumData.EnumStep.上架.ToString();
                if (!stepList.Contains(currStepCode)) stepList.Add(currStepCode);

                spInfo.UnQty -= item.Qty;
                if (spInfo.UnQty < 0) throw new ArgumentException(string.Format("货品（{0}）上架数量（{1}）超出范围！", item.ProductId,item.Qty));
                spInfo.Qty += item.Qty;
                spInfo.StepCode = string.Join(",", stepList);
                spInfo.LastStepName = currStepName;
                spInfo.LastUpdatedDate = DateTime.Now;
                spInfo.StockLocations = JsonConvert.SerializeObject(item.StockLocationList);
                spBll.Update(spInfo);
            }
            catch (Exception ex)
            {
                new CustomException(ex.Message, ex);
            }
        }

        private void SetRFID(RunQueueInfo model)
        {
            RFIDInfo info = JsonConvert.DeserializeObject<RFIDInfo>(model.RunValue);
            info.LastUpdatedDate = DateTime.Now;
            new CustomException(string.Format("info--：{0}", JsonConvert.SerializeObject(info)));
            var bll = new RFID();
            var oldInfo = bll.GetModel(info.TID, info.EPC);
            if (oldInfo == null) bll.Insert(info);
            else bll.Update(info);
        }
    }
}
