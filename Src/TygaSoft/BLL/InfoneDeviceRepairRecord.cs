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
    public partial class InfoneDeviceRepairRecord
    {
        #region DeviceRepairRecord Member


        public DataTable GetExportToExcelData(string sqlWhere, params SqlParameter[] cmdParms)
        {
            DataTable dtData = new DataTable("dtDeviceRepairRecord");
            var colAppend = "日期,客户,序列号,型号,故障原因,解决方案,客户问题,配件,处理情况,是否修好,交接人,是否归还,归还日期,登记人,备注";
            var eColAppend = @"RecordDate,Customer,SerialNumber,DeviceModel,FaultCause,SolveMethod,CustomerProblem,DevicePart,
                               TreatmentSituation,WhetherFix,HandoverPerson,IsBack,BackDate,
                               RegisteredPerson,Remark";
            var cols = colAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var ecols = eColAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var item in cols)
            {
                dtData.Columns.Add(new DataColumn(item, System.Type.GetType("System.String")));
            }

            var ds = dal.GetDsByExport(sqlWhere, cmdParms);
            var dt = ds.Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DataRow drData = dtData.NewRow();
                    for (var i = 0; i < cols.Length; i++)
                    {
                        drData["" + cols[i].Trim() + ""] = dr["" + ecols[i].Trim() + ""];
                    }

                    drData["日期"] = ((DateTime)dr["RecordDate"]).ToString("yyyy-MM-dd");
                    drData["是否归还"] = dr["IsBack"].ToString() == "True" ? "是" : "否";
                    drData["归还日期"] = ((DateTime)dr["BackDate"]).ToString("yyyy-MM-dd").Replace("1754-01-01", "");

                    dtData.Rows.Add(drData);
                }
            }

            return dtData;
        }

        public IList<InfoneDeviceRepairRecordInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetListByJoin(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }


        #endregion
    }
}
