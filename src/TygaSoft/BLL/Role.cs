using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TygaSoft.IDAL;
using TygaSoft.Model;
using TygaSoft.DALFactory;

namespace TygaSoft.BLL
{
    public class Role
    {
        private static readonly IRole dal = DataAccess.CreateRole();

        #region Role Member

        public RoleInfo GetModel(string roleName)
        {
            return dal.GetModel(roleName);
        }

        public List<RoleInfo> GetList()
        {
            return dal.GetList();
        }

        public int Update(RoleInfo model)
        {
            return dal.Update(model);
        }

        #endregion
    }
}
