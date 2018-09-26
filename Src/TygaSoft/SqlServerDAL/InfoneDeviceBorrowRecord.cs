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
    public partial class InfoneDeviceBorrowRecord
    {
        #region IInfoneDeviceBorrowRecord Member

        public DataSet GetDsExport(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select RecordDate '日期', Customer '客户',CustomerContact '客户联系人',SerialNumber '序列号',DeviceModel '型号（配置）',DevicePart '配件明细',PartStatus '配件状况',ProjectAbout '项目信息',SaleMan '业务员',SendOrderCode '寄出单号',(case IsBack when 1 then '是' else '否' end) '是否归还',BackDate '归还日期',Register '登记人',Remark '备注'
                        from DeviceBorrowRecord ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by RecordDate desc ");

            return SqlHelper.ExecuteDataset(SqlHelper.InfoneDbConnString, CommandType.Text, sb.ToString(), cmdParms);
        }

        public IList<InfoneDeviceBorrowRecordInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from DeviceBorrowRecord ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.InfoneDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<InfoneDeviceBorrowRecordInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			          Id,UserId,Customer,CustomerContact,SerialNumber,DeviceModel,DevicePart,PartStatus,ProjectAbout,SaleMan,SendOrderCode,IsBack,BackDate,Register,Remark,FunType,RecordDate,LastUpdatedDate
					  from DeviceBorrowRecord ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            var list = new List<InfoneDeviceBorrowRecordInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.InfoneDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        InfoneDeviceBorrowRecordInfo model = new InfoneDeviceBorrowRecordInfo();
                        model.Id = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1);
                        //model.UserId = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2);
                        model.Customer = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                        model.CustomerContact = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                        model.SerialNumber = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                        model.DeviceModel = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                        model.DevicePart = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                        model.PartStatus = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                        model.ProjectAbout = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                        model.SaleMan = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
                        model.SendOrderCode = reader.IsDBNull(11) ? string.Empty : reader.GetString(11);
                        model.IsBack = reader.IsDBNull(12) ? false : reader.GetBoolean(12);
                        model.BackDate = reader.IsDBNull(13) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(13);
                        model.Register = reader.IsDBNull(14) ? string.Empty : reader.GetString(14);
                        model.Remark = reader.IsDBNull(15) ? string.Empty : reader.GetString(15);
                        model.FunType = reader.IsDBNull(16) ? string.Empty : reader.GetString(16);
                        model.RecordDate = reader.IsDBNull(17) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(17);
                        model.LastUpdatedDate = reader.IsDBNull(18) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(18);

                        model.SIsBack = model.IsBack ? "是" : "否";
                        model.SBackDate = model.BackDate.ToString("yyyy-MM-dd") == "1754-01-01" ? "" : model.BackDate.ToString("yyyy-MM-dd");
                        model.SRecordDate = model.RecordDate.ToString("yyyy-MM-dd") == "1754-01-01" ? "" : model.RecordDate.ToString("yyyy-MM-dd");

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
