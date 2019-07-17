using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;
using TygaSoft.DBUtility;

namespace TygaSoft.SqlServerDAL
{
    public partial class BarcodeTemplate
    {
        #region IBarcodeTemplate Member

        public int SetDefault(Guid Id, bool isDefault,string typeName)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"update BarcodeTemplate set IsDefault = @IsDefault,LastUpdatedDate = @LastUpdatedDate 
			            where Id = @Id
					    ");
            if(isDefault)
            {
                sb.Append(@";update BarcodeTemplate set IsDefault = @IsDefault2,LastUpdatedDate = @LastUpdatedDate 
			                 where Id <> @Id and TypeName = @TypeName
					      ");
            }
            

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
                                     new SqlParameter("@IsDefault",SqlDbType.Bit),
                                     new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime),
                                   };
            parms[0].Value = Id;
            parms[1].Value = isDefault;
            parms[2].Value = DateTime.Now;
            if (isDefault)
            {
                Array.Resize(ref parms, 5);
                parms[3] = new SqlParameter("@IsDefault2", SqlDbType.Bit);
                parms[3].Value = !isDefault;
                parms[4] = new SqlParameter("@TypeName", SqlDbType.NVarChar, 20);
                parms[4].Value = typeName;
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        #endregion
    }
}
