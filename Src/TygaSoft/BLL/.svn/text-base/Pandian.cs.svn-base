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
    public partial class Pandian
    {
        #region Pandian Member

        public bool IsExistProduct(object pandianId)
        {
            return dal.IsExistProduct(pandianId);
        }

        public int UpdateStatus(object Id, string status)
        {
            return dal.UpdateStatus(Id, status);
        }

        public IList<PandianInfo> GetListByJoin(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetListByJoin(pageIndex, pageSize, sqlWhere, cmdParms);
        }

        public IList<PandianInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetListByJoin(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }

        #endregion
    }
}
