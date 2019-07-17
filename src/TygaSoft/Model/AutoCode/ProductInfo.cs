using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class ProductInfo
    {
        public ProductInfo() { }

        public ProductInfo(Guid id, Guid userId, Guid categoryId, Guid supplierId, string productCode, string productName, string fullName, string specs, decimal price, string materialQuality, double weight, int maxStore, int minStore, double outPackVolume, double outPackWeight, double inPackVolume, double inPackWeight, int outPackQty, int inPackQty, int shelfLife, int sort, string remark, bool isDisable, DateTime lastUpdatedDate)
        {
            this.Id = id;
            this.UserId = userId;
            this.CategoryId = categoryId;
            this.SupplierId = supplierId;
            this.ProductCode = productCode;
            this.ProductName = productName;
            this.FullName = fullName;
            this.Specs = specs;
            this.Price = price;
            this.MaterialQuality = materialQuality;
            this.Weight = weight;
            this.MaxStore = maxStore;
            this.MinStore = minStore;
            this.OutPackVolume = outPackVolume;
            this.OutPackWeight = outPackWeight;
            this.InPackVolume = inPackVolume;
            this.InPackWeight = inPackWeight;
            this.OutPackQty = outPackQty;
            this.InPackQty = inPackQty;
            this.ShelfLife = shelfLife;
            this.Sort = sort;
            this.Remark = remark;
            this.IsDisable = isDisable;
            this.LastUpdatedDate = lastUpdatedDate;
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid SupplierId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string FullName { get; set; }
        public string Specs { get; set; }
        public decimal Price { get; set; }
        public string MaterialQuality { get; set; }
        public double Weight { get; set; }
        public int MaxStore { get; set; }
        public int MinStore { get; set; }
        public double OutPackVolume { get; set; }
        public double OutPackWeight { get; set; }
        public double InPackVolume { get; set; }
        public double InPackWeight { get; set; }
        public int OutPackQty { get; set; }
        public int InPackQty { get; set; }
        public int ShelfLife { get; set; }
        public int Sort { get; set; }
        public string Remark { get; set; }
        public bool IsDisable { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
