using System;
using System.Collections.Generic;

namespace TygaSoft.Model
{
    [Serializable]
    public class ShelfMissionProductQueueInfo
    {
        public ShelfMissionProductQueueInfo() { }

        public ShelfMissionProductQueueInfo(object id, Guid shelfMissionId, Guid productId, string productCode, string productName, double qty, List<PdaStockLocationProductInfo> stockLocationList)
        {
            this.Id = id;
            this.ShelfMissionId = shelfMissionId;
            this.ProductId = productId;
            this.ProductCode = productCode;
            this.ProductName = productName;
            this.Qty = qty;
            this.StockLocationList = stockLocationList;
        }

        public object Id { get; set; }
        public Guid ShelfMissionId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public double Qty { get; set; }
        public List<PdaStockLocationProductInfo> StockLocationList { get; set; }
    }
}
