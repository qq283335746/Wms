using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.IDAL;
using TygaSoft.Model;
using TygaSoft.SysHelper;

namespace TygaSoft.BLL
{
    public partial class OrderPickProduct
    {
        #region OrderPickProduct Member

        public void DoOrderPickProduct(string itemAppend)
        {
            var items = itemAppend.Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries);

            var orderPickId = Guid.Parse(items[0]);
            var orderId = Guid.Parse(items[1]);
            var productId = Guid.Parse(items[2]);
            var customerId = Guid.Parse(items[3]);

            var oBll = new OrderPicked();
            var oInfo = oBll.GetModel(orderPickId);
            if (oInfo == null) throw new ArgumentException(MC.GetString(MC.Params_Data_NotExist, "拣货单ID“" + orderPickId + "”"));

            var osBll = new OrderSend();
            var orderInfo = osBll.GetModel(orderId);
            if (orderInfo == null) throw new ArgumentException(MC.GetString(MC.Params_Data_NotExist, orderId.ToString()));
            var ospBll = new OrderSendProduct();
            var ospInfo = ospBll.GetModel(orderId, productId, customerId);
            if (ospInfo == null) throw new ArgumentException(MC.M_RuleInvalidError);

            var pBll = new Product();
            var productInfo = pBll.GetModel(productId);
            var minVolume = productInfo.OutPackVolume == 0 ? 1 : productInfo.OutPackVolume;

            var slItems = items[4].Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            var dicSl = new Dictionary<Guid, float>();
            var totalQty = 0f;
            var currTime = DateTime.Now;

            foreach (var item in slItems)
            {
                var subItems = item.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var qty = float.Parse(subItems[1]);
                dicSl.Add(Guid.Parse(subItems[0]), qty);

                totalQty += qty;
            }

            var slBll = new StockLocation();
            var slpBll = new StockLocationProduct();
            var oppBll = new OrderPickProduct();
            int effect = 0;

            var oppInfo = oppBll.GetModel(orderPickId, orderId, productId, customerId);
            oppInfo.Qty += totalQty;
            oppInfo.StockLocations = slBll.GetStockLocationTextInIds(string.Join(",", dicSl.Select(m => m.Key)));
            oppInfo.LastUpdatedDate = currTime;
            effect = oppBll.Update(oppInfo);

            new StockProduct().DoProduct(productId, customerId, (int)EnumData.EnumStep.拣货, false, dicSl);

            oBll.SetTotalProduct(orderPickId.ToString());

            ospInfo.PickQty += totalQty;
            ospBll.Update(ospInfo);
            osBll.SetStatus(orderId.ToString());
        }

        public IList<OrderPickProductInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetListByJoin(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }

        public IList<OrderPickProductInfo> GetListByJoin(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetListByJoin(pageIndex, pageSize, sqlWhere, cmdParms);
        }

        public IList<OrderPickProductInfo> GetListByJoin(string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetListByJoin(sqlWhere, cmdParms);
        }

        #endregion
    }
}
