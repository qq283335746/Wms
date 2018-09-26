using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TygaSoft.Model
{
    public class MinStockLocationInfo
    {
        public MinStockLocationInfo() { }
        public MinStockLocationInfo(Guid stockLocationId,string stockLocationCode,string stockLocationName, double qty)
        {
            this.StockLocationId = stockLocationId;
            this.StockLocationCode = stockLocationCode;
            this.StockLocationName = stockLocationName;
            this.Qty = qty;
        }

        public Guid StockLocationId { get; set; }
        public string StockLocationCode { get; set; }
        public string StockLocationName { get; set; }
        public double Qty { get; set; }
    }
}
