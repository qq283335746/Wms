using System;
using System.Collections.Generic;

namespace TygaSoft.Model
{
    [Serializable]
    public class PdaShelfMissionProductInfo
    {
        public PdaShelfMissionProductInfo() { }

        public PdaShelfMissionProductInfo(Guid productId,string productCode,string productName,double qty,int status, List<PdaStockLocationProductInfo> stockLocationList) {
            this.ProductId = productId;
            this.ProductCode = productCode;
            this.ProductName = productName;
            this.Qty = qty;
            this.Status = status;
            this.StockLocationList = stockLocationList;
        }

        public Guid ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public double Qty { get; set; }
        public int Status { get; set; }
        public List<PdaStockLocationProductInfo> StockLocationList { get; set; }
    }
}
