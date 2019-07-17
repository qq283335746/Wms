using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TygaSoft.Model;
using TygaSoft.SysHelper;

namespace TygaSoft.BLL
{
    public partial class OrderReceiptRecord
    {
        #region OrderReceiptRecord Member

        public void DoOrderReceipt(OrderReceiptRecordInfo model)
        {
            DateTime currTime = DateTime.Now;

            OrderReceipt orbBll = new OrderReceipt();
            var orbModel = orbBll.GetModel(model.OrderId);

            OrderReceiptProduct orpBll = new OrderReceiptProduct();
            var orpModel = orpBll.GetModel(model.OrderId, model.ProductId);
            orpModel.ReceiptQty += model.Qty;
            orpBll.UpdateQty(model.OrderId, model.ProductId, orpModel.ReceiptQty);

            StockProduct spBll = new StockProduct();
            StockProductInfo spModel = null;
            spModel = spBll.GetModel(Guid.Empty, model.ProductId);
            if (spModel == null)
            {
                var stepCode = (int)EnumData.EnumStep.收货;
                var stepName = EnumData.EnumStep.收货.ToString();
                spModel = new StockProductInfo(Guid.Empty, model.ProductId, model.Qty,orpModel.ReceiptQty, 0, stepCode.ToString(), stepName,EnumData.EnumIsDisable.启用.ToString(),"","", currTime);

                spBll.Insert(spModel);
            }
            else
            {
                spModel.UnQty += model.Qty;
                spModel.LastUpdatedDate = currTime;
                spBll.Update(spModel);
            }
        }

        public IList<OrderReceiptRecordInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetListByJoin(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }

        #endregion
    }
}
