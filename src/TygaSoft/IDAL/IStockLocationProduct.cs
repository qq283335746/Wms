using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IStockLocationProduct
    {
        #region IStockLocationProduct Member

        string GetNameByProductId(Guid productId);

        StockLocationProductInfo GetModelByJoin(Guid Id);

        IList<StockLocationProductInfo> GetListByUsable(Guid productId, double minVolume);

        IList<StockLocationProductInfo> GetListForOrderSendProduct();

        IList<StockLocationProductInfo> GetListForOrderPickProduct(Guid productId);

        #endregion
    }
}
