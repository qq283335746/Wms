using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface ICategory
    {
        #region ICategory Member

        CategoryInfo GetModelByCode(string code);

        CategoryInfo GetRootModel();

        string CreateCode(Guid Id);

        bool IsExistProduct(object categoryId);

        bool IsExistCode(string categoryCode, Guid Id);

        #endregion
    }
}
