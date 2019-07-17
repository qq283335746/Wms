using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class MesOrderInfo
    {
        public MesOrderInfo() { }

        public MesOrderInfo(Guid id, Guid userId, string oBarcode, string pBarcode, string pdBarcode, string ptBarcode, double qty, DateTime startDate, DateTime endDate, int sort, string remark, DateTime lastUpdatedDate)
        {
            this.Id = id;
            this.UserId = userId;
            this.OBarcode = oBarcode;
            this.PBarcode = pBarcode;
            this.PdBarcode = pdBarcode;
            this.PtBarcode = ptBarcode;
            this.Qty = qty;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.Sort = sort;
            this.Remark = remark;
            this.LastUpdatedDate = lastUpdatedDate;
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string OBarcode { get; set; }
        public string PBarcode { get; set; }
        public string PdBarcode { get; set; }
        public string PtBarcode { get; set; }
        public double Qty { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Sort { get; set; }
        public string Remark { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
