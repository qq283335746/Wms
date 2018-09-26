using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using TygaSoft.SysHelper;
using TygaSoft.IDAL;
using TygaSoft.Model;

namespace TygaSoft.BLL
{
    public partial class ShelfMissionProduct
    {
        #region ShelfMissionProduct Member

        public void DoShelfMissionProduct(string itemAppend)
        {
            var items = itemAppend.Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries);
            var shelfMissionId = Guid.Parse(items[0]);
            var orderId = Guid.Parse(items[1]);
            var productId = Guid.Parse(items[2]);

            var smBll = new ShelfMission();
            var smInfo = smBll.GetModel(shelfMissionId);
            if (smInfo == null) throw new ArgumentException(MC.GetString(MC.Params_Data_NotExist, shelfMissionId.ToString()));

            var oBll = new OrderReceipt();
            var orderInfo = oBll.GetModel(orderId);
            if (orderInfo == null) throw new ArgumentException(MC.GetString(MC.Params_Data_NotExist, orderId.ToString()));

            var pBll = new Product();
            var productInfo = pBll.GetModel(productId);
            var minVolume = productInfo.OutPackVolume == 0 ? 1 : productInfo.OutPackVolume;

            var slItems = items[3].Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            var slBll = new StockLocation();
            var pslaList = new List<ProductStockLocationAttrInfo>();
            var dicSl = new Dictionary<Guid, float>();
            var totalQty = 0f;
            var currTime = DateTime.Now;

            foreach (var item in slItems)
            {
                var subItems = item.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var slId = Guid.Parse(subItems[0]);
                var qty = float.Parse(subItems[1]);

                var slInfo = slBll.GetModel(slId);
                if (slInfo == null) throw new ArgumentException(MC.GetString(MC.Params_Data_NotExist, "库位ID为" + slId + ""));
                pslaList.Add(new ProductStockLocationAttrInfo(slId, slInfo.Code, slInfo.Named, qty, 0, currTime));
                dicSl.Add(slId, qty);
                totalQty += qty;
            }

            var smpBll = new ShelfMissionProduct();
            var smpInfo = smpBll.GetModel(shelfMissionId, orderId, productId);
            if (smpInfo == null) throw new ArgumentException(MC.Data_NotExist);
            smpInfo.Qty += totalQty;
            smpInfo.StockLocations = JsonConvert.SerializeObject(pslaList);
            smpInfo.LastUpdatedDate = currTime;
            smpBll.Update(smpInfo);

            smBll.SetTotalProduct(shelfMissionId.ToString());

            #region 库存库位货品

            var spBll = new StockProduct();
            spBll.DoProduct(productId, orderInfo.CustomerId, (int)EnumData.EnumStep.上架, dicSl);

            #endregion
        }

        public IList<ShelfMissionProductInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetListByJoin(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }

        public IList<ShelfMissionProductInfo> GetListByJoin(string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetListByJoin(sqlWhere, cmdParms);
        }

        public IList<ShelfMissionProductInfo> GetListByScanned(Guid shelfMissionId)
        {
            return dal.GetListByScanned(shelfMissionId);
        }

        #endregion
    }
}
