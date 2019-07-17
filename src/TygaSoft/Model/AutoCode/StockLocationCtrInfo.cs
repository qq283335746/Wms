using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class StockLocationCtrInfo
    {
        public StockLocationCtrInfo() { }

        public StockLocationCtrInfo(Guid stockLocationId, string routeSeq, bool isMixPlace, bool isBatchNum, bool isLoseId, string status, string warehouse, double levelNum, string ctrType, string aBC, double insertTaskSeq, string pickArea, string pickMethod, double inventoryGroupId)
        {
            this.StockLocationId = stockLocationId;
            this.RouteSeq = routeSeq;
            this.IsMixPlace = isMixPlace;
            this.IsBatchNum = isBatchNum;
            this.IsLoseId = isLoseId;
            this.Status = status;
            this.Warehouse = warehouse;
            this.LevelNum = levelNum;
            this.CtrType = ctrType;
            this.ABC = aBC;
            this.InsertTaskSeq = insertTaskSeq;
            this.PickArea = pickArea;
            this.PickMethod = pickMethod;
            this.InventoryGroupId = inventoryGroupId;
        }

        public Guid StockLocationId { get; set; }
        public string RouteSeq { get; set; }
        public bool IsMixPlace { get; set; }
        public bool IsBatchNum { get; set; }
        public bool IsLoseId { get; set; }
        public string Status { get; set; }
        public string Warehouse { get; set; }
        public double LevelNum { get; set; }
        public string CtrType { get; set; }
        public string ABC { get; set; }
        public double InsertTaskSeq { get; set; }
        public string PickArea { get; set; }
        public string PickMethod { get; set; }
        public double InventoryGroupId { get; set; }
    }
}
