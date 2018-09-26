using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TygaSoft.Model;

namespace TygaSoft.BLL
{
    public partial class Package
    {
        #region Package Member

        public PackageInfo GetModelByJoin(object Id)
        {
            return dal.GetModelByJoin(Id);
        }

        public IList<PackageInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetListByJoin(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }

        #endregion
    }
}
