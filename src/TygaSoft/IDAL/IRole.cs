using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public interface IRole
    {
        #region 成员方法

        RoleInfo GetModel(string roleName);

        List<RoleInfo> GetList();

        int Update(RoleInfo model);

        #endregion
    }
}
