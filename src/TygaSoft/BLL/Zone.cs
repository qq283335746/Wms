using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.IDAL;
using TygaSoft.Model;
using TygaSoft.DALFactory;

namespace TygaSoft.BLL
{
    public partial class Zone
    {
        #region Zone Member

        public IList<ZoneInfo> GetListInStockLocation(string slIds)
        {
            return dal.GetListInStockLocation(slIds);
        }

        #endregion
    }
}
