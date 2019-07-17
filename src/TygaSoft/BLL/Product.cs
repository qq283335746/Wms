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
    public partial class Product
    {
        #region Product Member

        public IList<ProductInfo> GetListInIds(string IdAppend)
        {
            var items = IdAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var sb = new StringBuilder(500);
            var index = 0;
            foreach (var item in items)
            {
                if (index > 0) sb.Append(",");
                sb.AppendFormat("'{0}'", item);

                index++;
            }
            var sqlWhere = string.Format("and Id in ({0}) ", sb.ToString());

            return dal.GetList(sqlWhere, null);
        }

        public string CreateCode(Guid categoryId)
        {
            return dal.CreateCode(categoryId);
        }

        public bool IsExistCode(string productCode, Guid Id)
        {
            return dal.IsExistCode(productCode, Id);
        }

        public IList<ProductInfo> GetListByCategory(int pageIndex, int pageSize, out int totalRecords, object categoryId, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetListByCategory(pageIndex, pageSize, out totalRecords, categoryId, sqlWhere, cmdParms);
        }

        #endregion
    }
}
