using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class StockLocationProductInfo
    {
        public StockLocationProductInfo() { }

        public StockLocationProductInfo(Guid stockLocationId, string productAttr, double maxVolume)
        {
            this.StockLocationId = stockLocationId;
            this.ProductAttr = productAttr;
            this.MaxVolume = maxVolume;
        }

        public Guid StockLocationId { get; set; }
        public string ProductAttr { get; set; }
        public double MaxVolume { get; set; }
    }
}
