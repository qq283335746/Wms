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
    public partial class MesProduct
    {
        #region MesProduct Member

        public bool IsExistCode(string code, Guid Id)
        {
            return dal.IsExistCode(code, Id);
        }

        #endregion
    }
}
