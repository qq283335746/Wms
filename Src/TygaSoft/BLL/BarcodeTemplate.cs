using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;
using TygaSoft.DALFactory;

namespace TygaSoft.BLL
{
    public partial class BarcodeTemplate
    {
        #region BarcodeTemplate Member

        public int SetDefault(Guid Id, bool isDefault, string typeName)
        {
            return dal.SetDefault(Id, isDefault, typeName);
        }

        #endregion
    }
}
