using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using TygaSoft.IDAL;
using TygaSoft.Model;
using TygaSoft.SysHelper;

namespace TygaSoft.BLL
{
    public partial class ShelfMission
    {
        #region ShelfMission Member

        public IList<ShelfMissionInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetListByJoin(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }

        public void SetTotalProduct(string orderCode)
        {
            dal.SetTotalProduct(orderCode);
        }

        #endregion
    }
}
