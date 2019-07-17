using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.IDAL;
using TygaSoft.Model;
using TygaSoft.DBUtility;

namespace TygaSoft.SqlServerDAL
{
    public partial class OrderSendProduct
    {
        #region IOrderSendProduct Member

        public float[] GetTotalByOrders(string orderIds)
        {
            var datas = new float[3];
            var items = orderIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var sqlIn = new StringBuilder(300);
            foreach(var item in items)
            {
                sqlIn.AppendFormat("'{0}',", item);
            }
            var cmdText = string.Format(@"select sum(osp.Qty) TotalQty from OrderSendProduct osp where osp.OrderId in({0})", sqlIn.ToString().Trim(','));
            var obj = SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, cmdText);
            if(obj != null)
            {
                datas[0] = float.Parse(obj.ToString());
                datas[1] = datas[0];
                datas[2] = datas[0];
            }
            else
            {
                datas[0] = 0;
                datas[1] = 0;
                datas[2] = 0;
            }

            return datas;
        }

        public IList<OrderSendProductInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            var sb = new StringBuilder(500);
            sb.Append(@"select count(*) from OrderSendProduct osp 
                      left join Product p on p.Id = osp.ProductId 
                      left join Customer c on c.Id = osp.CustomerId
                      ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<OrderSendProductInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by osp.LastUpdatedDate desc) as RowNumber,
			          osp.OrderId,osp.ProductId,osp.CustomerId,osp.Qty,osp.PickQty,osp.Status,osp.LastUpdatedDate
                      ,p.ProductCode,p.ProductName,c.Coded CustomerCode,c.Named CustomerName
					  from OrderSendProduct osp 
                      left join Product p on p.Id = osp.ProductId
                      left join Customer c on c.Id = osp.CustomerId
                      ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            var list = new List<OrderSendProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var model = new OrderSendProductInfo();
                        model.OrderId = reader.GetGuid(1);
                        model.ProductId = reader.GetGuid(2);
                        model.CustomerId = reader.GetGuid(3);
                        model.Qty = reader.GetDouble(4);
                        model.PickQty = reader.GetDouble(5);
                        model.Status = reader.GetString(6);
                        model.LastUpdatedDate = reader.GetDateTime(7);

                        model.ProductCode = reader.IsDBNull(8) ? "" : reader.GetString(8);
                        model.ProductName = reader.IsDBNull(9) ? "" : reader.GetString(9);
                        model.CustomerCode = reader.IsDBNull(10) ? "" : reader.GetString(10);
                        model.CustomerName = reader.IsDBNull(11) ? "" : reader.GetString(11);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<OrderSendProductInfo> GetListByJoin(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select osp.OrderId,osp.ProductId,osp.CustomerId,osp.Qty,osp.PickQty,osp.Status,osp.LastUpdatedDate
                        ,p.ProductCode,p.ProductName,c.Coded CustomerCode,c.Named CustomerName
                        from OrderSendProduct osp 
                        left join Product p on p.Id = osp.ProductId
                        left join Customer c on c.Id = osp.CustomerId
                        ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by osp.LastUpdatedDate ");

            var list = new List<OrderSendProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderSendProductInfo model = new OrderSendProductInfo();
                        model.OrderId = reader.GetGuid(0);
                        model.ProductId = reader.GetGuid(1);
                        model.CustomerId = reader.GetGuid(2);
                        model.Qty = reader.GetDouble(3);
                        model.PickQty = reader.GetDouble(4);
                        model.Status = reader.GetString(5);
                        model.LastUpdatedDate = reader.GetDateTime(6);

                        model.ProductCode = reader.IsDBNull(7) ? "" : reader.GetString(7);
                        model.ProductName = reader.IsDBNull(8) ? "" : reader.GetString(8);
                        model.CustomerCode = reader.IsDBNull(9) ? "" : reader.GetString(9);
                        model.CustomerName = reader.IsDBNull(10) ? "" : reader.GetString(10);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public bool IsExist(Guid orderId,Guid productId)
        {
            var cmdText = @"select 1 from [OrderSendProduct] where OrderId = @OrderId and ProductId = @ProductId ";
            SqlParameter[] parms = {
                                     new SqlParameter("@OrderId",SqlDbType.UniqueIdentifier),
                                     new SqlParameter("@ProductId",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = orderId;
            parms[1].Value = productId;

            object obj = SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, cmdText, parms);
            if (obj != null) return true;

            return false;
        }

        #endregion
    }
}
