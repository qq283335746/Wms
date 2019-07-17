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
    public partial class MesOrder : IMesOrder
    {
        #region IMesOrder Member

        public MesOrderInfo GetModel(string oBarcode, string pBarcode, string pdBarcode, string ptBarcode)
        {
            MesOrderInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 Id,UserId,OBarcode,PBarcode,PdBarcode,PtBarcode,Qty,StartDate,EndDate,Sort,Remark,LastUpdatedDate 
			            from MesOrder
						where OBarcode = @OBarcode and PBarcode = @PBarcode and PdBarcode = @PdBarcode and PtBarcode = @PtBarcode ");
            SqlParameter[] parms = {
                                     new SqlParameter("@OBarcode",SqlDbType.VarChar,36),
                                     new SqlParameter("@PBarcode",SqlDbType.VarChar,36),
                                     new SqlParameter("@PdBarcode",SqlDbType.VarChar,36),
                                     new SqlParameter("@PtBarcode",SqlDbType.VarChar,36)
                                   };
            parms[0].Value = oBarcode;
            parms[1].Value = pBarcode;
            parms[2].Value = pdBarcode;
            parms[3].Value = ptBarcode;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        model = new MesOrderInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.OBarcode = reader.GetString(2);
                        model.PBarcode = reader.GetString(3);
                        model.PdBarcode = reader.GetString(4);
                        model.PtBarcode = reader.GetString(5);
                        model.Qty = reader.GetDouble(6);
                        model.StartDate = reader.GetDateTime(7);
                        model.EndDate = reader.GetDateTime(8);
                        model.Sort = reader.GetInt32(9);
                        model.Remark = reader.GetString(10);
                        model.LastUpdatedDate = reader.GetDateTime(11);
                    }
                }
            }

            return model;
        }

        #endregion
    }
}
