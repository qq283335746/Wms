using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.BLL
{
    public partial class Category
    {
        #region Category Member

        public CategoryInfo GetModelByCode(string code)
        {
            return dal.GetModelByCode(code);
        }

        public CategoryInfo GetRootModel()
        {
            return dal.GetRootModel();
        }

        public string CreateCode(Guid Id)
        {
            return dal.CreateCode(Id);
        }

        public bool IsExistProduct(object categoryId)
        {
            return dal.IsExistProduct(categoryId);
        }

        public bool IsExistCode(string categoryCode, Guid Id)
        {
            return dal.IsExistCode(categoryCode,Id);
        }

        public string GetTreeJson()
        {
            StringBuilder jsonAppend = new StringBuilder();
            var list = dal.GetList().ToList<CategoryInfo>();
            if (list != null && list.Count > 0)
            {
                var rootNode = list.FirstOrDefault(m => m.ParentId == Guid.Empty);
                CreateTreeJson(list, Guid.Empty, rootNode, ref jsonAppend);
            }
            else
            {
                jsonAppend.Append("[{\"id\":\"" + Guid.Empty + "\",\"text\":\"请选择\",\"state\":\"closed\",\"attributes\":{\"parentId\":\"" + Guid.Empty + "\",\"parentName\":\"请选择\"}}]");
            }

            return jsonAppend.ToString();
        }

        private void CreateTreeJson(List<CategoryInfo> list, object parentId, CategoryInfo rootNode, ref StringBuilder jsonAppend)
        {
            jsonAppend.Append("[");
            var childList = list.FindAll(x => x.ParentId.Equals(parentId));
            if (childList.Count > 0)
            {
                int index = 0;
                foreach (var model in childList)
                {
                    var state = (model.Id == rootNode.Id) ? "open" : "closed";
                    var sText = model.ParentId.Equals(Guid.Empty) ? model.CategoryName : string.Format("{0}（{1}）", model.CategoryCode, model.CategoryName);
                    jsonAppend.AppendFormat(@"{{""id"":""{0}"",""text"":""{1}"",""state"":""{2}"",""attributes"":{{""parentId"":""{3}""}}", model.Id, sText, state, model.ParentId);
                    //jsonAppend.Append("{\"id\":\"" + model.Id + "\",\"text\":\"" + model.CategoryName + "\",\"state\":\""+ state + "\",\"attributes\":{\"parentId\":\"" + model.ParentId + "\"}");
                    if (list.Any(r => r.ParentId.Equals(model.Id)))
                    {
                        jsonAppend.Append(",\"children\":");
                        CreateTreeJson(list, model.Id, rootNode, ref jsonAppend);
                    }
                    jsonAppend.Append("}");
                    if (index < childList.Count - 1) jsonAppend.Append(",");
                    index++;
                }
            }
            jsonAppend.Append("]");
        }

        #endregion
    }
}
