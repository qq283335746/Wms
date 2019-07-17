using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IMesOrder
    {
        #region IMesOrder Member

        MesOrderInfo GetModel(string oBarcode, string pBarcode, string pdBarcode, string ptBarcode);

        #endregion
    }
}
