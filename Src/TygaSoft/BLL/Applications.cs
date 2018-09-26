using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TygaSoft.DALFactory;
using TygaSoft.IDAL;

namespace TygaSoft.BLL
{
    public partial class Applications
    {
        private static readonly IApplications dal = DataAccess.CreateApplications();

        #region Applications Member

        public Guid GetAspnetAppId(string appName)
        {
            return dal.GetAspnetAppId(appName);
        }

        #endregion
    }
}
