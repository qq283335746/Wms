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
    public partial class MesOrder
    {
        private static readonly IMesOrder dal = DataAccess.CreateMesOrder();

        #region MesOrder Member

        public int Insert(MesOrderInfo model)
        {
            return dal.Insert(model);
        }

        public int InsertByOutput(MesOrderInfo model)
        {
            return dal.InsertByOutput(model);
        }

        public int Update(MesOrderInfo model)
        {
            return dal.Update(model);
        }

        public int Delete(Guid id)
        {
            return dal.Delete(id);
        }

        public bool DeleteBatch(IList<object> list)
        {
            return dal.DeleteBatch(list);
        }

        public MesOrderInfo GetModel(Guid id)
        {
            return dal.GetModel(id);
        }

        public IList<MesOrderInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }

        public IList<MesOrderInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(pageIndex, pageSize, sqlWhere, cmdParms);
        }

        public IList<MesOrderInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(sqlWhere, cmdParms);
        }

        public IList<MesOrderInfo> GetList()
        {
            return dal.GetList();
        }

        #endregion
    }
}
