using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IStockLocation
    {
        #region IStockLocation Member

        IList<StockLocationInfo> GetListInZoneIds(string zoneIds);

        string GetStockLocationTextInIds(string Ids);

        StockLocationInfo GetModelForTemp();

        StockLocationInfo GetModelByJoin(object Id);

        List<StockLocationInfo> GetListByBest();

        IList<StockLocationInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        #endregion
    }
}
