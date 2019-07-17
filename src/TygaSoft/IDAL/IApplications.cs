using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TygaSoft.IDAL
{
    public partial interface IApplications
    {
        #region IApplication Member

        Guid GetAspnetAppId(string appName);

        #endregion
    }
}
