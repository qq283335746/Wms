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
    public partial class RFID
    {
        private static readonly IRFID dal = DataAccess.CreateRFID();

        #region RFID Member

        public int Insert(RFIDInfo model)
        {
            return dal.Insert(model);
        }

        public int Update(RFIDInfo model)
        {
            return dal.Update(model);
        }

        public int Delete(string tID, string ePC)
        {
            return dal.Delete(tID, ePC);
        }

        public bool DeleteBatch(IList<object> list)
        {
            return dal.DeleteBatch(list);
        }

        public RFIDInfo GetModel(string tID, string ePC)
        {
            return dal.GetModel(tID, ePC);
        }

        public IList<RFIDInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }

        public IList<RFIDInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(pageIndex, pageSize, sqlWhere, cmdParms);
        }

        public IList<RFIDInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(sqlWhere, cmdParms);
        }

        public IList<RFIDInfo> GetList()
        {
            return dal.GetList();
        }

        #endregion
    }
}
