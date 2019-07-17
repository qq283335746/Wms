using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IBarcodeTemplate
    {
        #region IBarcodeTemplate Member

        int SetDefault(Guid Id, bool isDefault, string typeName);

        #endregion
    }
}
