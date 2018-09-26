using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TygaSoft.Model
{
    [Serializable]
    public class StockLocationProductAttrInfo
    {
        public StockLocationProductAttrInfo() { }
        public StockLocationProductAttrInfo(Guid productId, double qty,double freezeQty, DateTime lastUpdatedDate)
        {
            this.ProductId = productId;
            this.Qty = qty;
            this.FreezeQty = freezeQty;
            this.LastUpdatedDate = lastUpdatedDate;
        }

        public Guid ProductId { get; set; }
        public double Qty { get; set; }
        public double FreezeQty { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
