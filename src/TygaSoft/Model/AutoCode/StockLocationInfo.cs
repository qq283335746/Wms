using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class StockLocationInfo
    {
        public StockLocationInfo() { }

        public StockLocationInfo(Guid id, Guid userId, Guid zoneId, string code, string named, double width, double wide, double high, double volume, double cubage, double row, double layer, double col, double passway, double xc, double yc, double zc, double orientation, string stockLocationType, double stackLimit, double groundTrayQty, string stockLocationDeal, double carryWeight, string useStatus, string remark, DateTime lastUpdatedDate)
        {
            this.Id = id;
            this.UserId = userId;
            this.ZoneId = zoneId;
            this.Code = code;
            this.Named = named;
            this.Width = width;
            this.Wide = wide;
            this.High = high;
            this.Volume = volume;
            this.Cubage = cubage;
            this.Row = row;
            this.Layer = layer;
            this.Col = col;
            this.Passway = passway;
            this.Xc = xc;
            this.Yc = yc;
            this.Zc = zc;
            this.Orientation = orientation;
            this.StockLocationType = stockLocationType;
            this.StackLimit = stackLimit;
            this.GroundTrayQty = groundTrayQty;
            this.StockLocationDeal = stockLocationDeal;
            this.CarryWeight = carryWeight;
            this.UseStatus = useStatus;
            this.Remark = remark;
            this.LastUpdatedDate = lastUpdatedDate;
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ZoneId { get; set; }
        public string Code { get; set; }
        public string Named { get; set; }
        public double Width { get; set; }
        public double Wide { get; set; }
        public double High { get; set; }
        public double Volume { get; set; }
        public double Cubage { get; set; }
        public double Row { get; set; }
        public double Layer { get; set; }
        public double Col { get; set; }
        public double Passway { get; set; }
        public double Xc { get; set; }
        public double Yc { get; set; }
        public double Zc { get; set; }
        public double Orientation { get; set; }
        public string StockLocationType { get; set; }
        public double StackLimit { get; set; }
        public double GroundTrayQty { get; set; }
        public string StockLocationDeal { get; set; }
        public double CarryWeight { get; set; }
        public string UseStatus { get; set; }
        public string Remark { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
