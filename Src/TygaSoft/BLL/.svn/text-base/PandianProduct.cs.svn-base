using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.IDAL;
using TygaSoft.Model;

namespace TygaSoft.BLL
{
    public partial class PandianProduct
    {
        #region PandianProduct Member

        public int[] GetTotal(object pandianId)
        {
            return dal.GetTotal(pandianId);
        }

        public IList<PandianProductInfo> GetListByJoin(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetListByJoin(pageIndex, pageSize, sqlWhere, cmdParms);
        }

        public IList<PandianProductInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetListByJoin(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }

        #endregion
    }
}
