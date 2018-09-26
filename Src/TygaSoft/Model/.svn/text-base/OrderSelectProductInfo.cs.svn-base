using System;

namespace TygaSoft.Model
{
    [Serializable]
    public class OrderSelectProductInfo
    {
        public OrderSelectProductInfo() { }

        public OrderSelectProductInfo(Guid productId, string productCode, string productName, double qty, double unQty, double realQty)
        {
            this.ProductId = productId;
            this.ProductCode = productCode;
            this.ProductName = productName;
            this.Qty = qty;
            this.UnQty = unQty;
            this.RealQty = realQty;
        }

        public Guid ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public double Qty { get; set; }
        public double UnQty { get; set; }
        public double RealQty { get; set; }
        public string StockLocations { get; set; }
    }
}
