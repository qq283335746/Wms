using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.BLL
{
    public partial class SiteMenus
    {
        #region SiteMenus Member

        public IList<SiteMenusInfo> GetListByParentName(string parentName)
        {
            return dal.GetListByParentName(parentName);
        }

        public IList<SiteMenusInfo> GetMenusAccess(string appName, string[] accessIds, bool isAdministrators)
        {
            return dal.GetMenusAccess(appName, accessIds, isAdministrators);
        }

        public string GetTreeJson(string appName)
        {
            StringBuilder jsonAppend = new StringBuilder();
            var list = GetMenus(appName).ToList<SiteMenusInfo>();
            if (list != null && list.Count > 0)
            {
                CreateTreeJson(list, Guid.Empty, ref jsonAppend);
            }
            else
            {
                jsonAppend.Append("[{\"id\":\"" + Guid.Empty + "\",\"text\":\"请选择\",\"state\":\"open\",\"attributes\":{\"parentId\":\"" + Guid.Empty + "\",\"parentName\":\"请选择\"}}]");
            }

            return jsonAppend.ToString();
        }

        public void CreateTreeJson(IEnumerable<SiteMenusInfo> q, object parentId, ref StringBuilder jsonAppend)
        {
            jsonAppend.Append("[");
            var childList = q.Where(x => x.ParentId.Equals(parentId));
            if (childList != null && childList.Count() > 0)
            {
                int index = 0;
                foreach (var model in childList)
                {
                    var hasChild = q.Any(r => r.ParentId.Equals(model.Id));
                    var state = hasChild ? "closed" : "open";
                    jsonAppend.Append("{\"id\":\"" + model.Id + "\",\"text\":\"" + model.Title + "\",\"state\":\"" + state + "\",\"attributes\":{\"ParentId\":\"" + model.ParentId + "\",\"Url\":\"" + model.Url + "\"}");
                    if (hasChild)
                    {
                        jsonAppend.Append(",\"children\":");
                        CreateTreeJson(q, model.Id, ref jsonAppend);
                    }
                    jsonAppend.Append("}");
                    if (index < childList.Count() - 1) jsonAppend.Append(",");
                    index++;
                }
            }
            jsonAppend.Append("]");
        }

        public string GetTreeGridJson(string appName, Guid accessId, string accessType, bool isAdministrators)
        {
            var appId = new Applications().GetAspnetAppId(appName);
            StringBuilder jsonAppend = new StringBuilder();
            var list = dal.GetMenusAccess(appId, accessId, isAdministrators);
            if (list != null && list.Count > 0)
            {
                var isRole = accessType == "Roles";
                CreateTreeGridJson(list, Guid.Empty, ref jsonAppend, isRole);
            }
            else
            {
                jsonAppend.Append("[{\"Id\":\"" + Guid.Empty + "\",\"Title\":\"请选择\",\"IsAllowRole\":\"0\",\"IsDenyUser\":\"0\",\"IsView\":\"0\",\"IsAdd\":\"0\",\"IsEdit\":\"0\",\"IsDel\":\"0\"}]");
            }

            return jsonAppend.ToString();
        }

        private void CreateTreeGridJson(List<SiteMenusInfo> list, object parentId, ref StringBuilder jsonAppend, bool isRole)
        {
            jsonAppend.Append("[");
            var childList = list.FindAll(x => x.ParentId.Equals(parentId));
            if (childList.Count > 0)
            {
                int index = 0;
                foreach (var model in childList)
                {
                    var isView = model.IsView ? 1 : 0;
                    var isAdd = model.IsAdd ? 1 : 0;
                    var isEdit = model.IsEdit ? 1 : 0;
                    var isDelete = model.IsDelete ? 1 : 0;
                    jsonAppend.Append("{\"Id\":\"" + model.Id + "\",\"Title\":\"" + model.Title + "\",\"Url\":\"" + model.Url + "\",\"IsView\":\"" + isView + "\",\"IsAdd\":\"" + isAdd + "\",\"IsEdit\":\"" + isEdit + "\",\"IsDel\":\"" + isDelete + "\"");
                    if (list.Any(r => r.ParentId.Equals(model.Id)))
                    {
                        jsonAppend.Append(",\"children\":");
                        CreateTreeGridJson(list, model.Id, ref jsonAppend, isRole);
                    }
                    jsonAppend.Append("}");
                    if (index < childList.Count - 1) jsonAppend.Append(",");
                    index++;
                }
            }
            jsonAppend.Append("]");
        }

        public IList<SiteMenusInfo> GetMenus(string appName)
        {
            string sqlWhere = string.Empty;
            if (!string.IsNullOrEmpty(appName))
            {
                var appId = new Applications().GetAspnetAppId(appName);
                sqlWhere = "and ApplicationId = '" + appId + "' ";
            }

            return dal.GetList(sqlWhere, null);
        }

        #endregion
    }
}
