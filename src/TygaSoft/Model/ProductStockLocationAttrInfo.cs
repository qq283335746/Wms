using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TygaSoft.Model
{
    [Serializable]
    public class ProductStockLocationAttrInfo
    {
        public ProductStockLocationAttrInfo() { }
        public ProductStockLocationAttrInfo(Guid stockLocationId,string stockLocationCode,string stockLocationName, double qty,double freezeQty, DateTime lastUpdatedDate)
        {
            this.StockLocationId = stockLocationId;
            this.StockLocationCode = stockLocationCode;
            this.StockLocationName = stockLocationName;
            this.Qty = qty;
            this.FreezeQty = freezeQty;
            this.LastUpdatedDate = lastUpdatedDate;
        }

        public Guid StockLocationId { get; set; }
        public string StockLocationCode { get; set; }
        public string StockLocationName { get; set; }
        public double Qty { get; set; }
        public double FreezeQty { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
