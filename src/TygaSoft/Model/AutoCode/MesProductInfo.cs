using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class MesProductInfo
    {
        public MesProductInfo() { }

        public MesProductInfo(Guid id, Guid userId, Guid categoryId, string coded, string named, double useQty, string barcode, int sort, string remark, DateTime lastUpdatedDate)
        {
            this.Id = id;
            this.UserId = userId;
            this.CategoryId = categoryId;
            this.Coded = coded;
            this.Named = named;
            this.UseQty = useQty;
            this.Barcode = barcode;
            this.Sort = sort;
            this.Remark = remark;
            this.LastUpdatedDate = lastUpdatedDate;
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CategoryId { get; set; }
        public string Coded { get; set; }
        public string Named { get; set; }
        public double UseQty { get; set; }
        public string Barcode { get; set; }
        public int Sort { get; set; }
        public string Remark { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
