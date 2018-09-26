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
    public partial class MesOrder
    {
        #region MesOrder Member

        public MesOrderInfo GetModel(string oBarcode, string pBarcode, string pdBarcode, string ptBarcode)
        {
            return dal.GetModel(oBarcode, pBarcode, pdBarcode, ptBarcode);
        }

        #endregion
    }
}
