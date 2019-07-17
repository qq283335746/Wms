using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TygaSoft.Model
{
    public class SiteMenusAccessItemInfo
    {
        public SiteMenusAccessItemInfo() { }
        public SiteMenusAccessItemInfo(Guid menuId, bool isView, bool isAdd, bool isEdit, bool isDelete)
        {
            this.MenuId = menuId;
            this.IsView = isView;
            this.IsAdd = isAdd;
            this.IsEdit = isEdit;
            this.IsDelete = isDelete;
        }

        public Guid MenuId { get; set; }
        public bool IsView { get; set; }
        public bool IsAdd { get; set; }
        public bool IsEdit { get; set; }
        public bool IsDelete { get; set; }
    }
}
