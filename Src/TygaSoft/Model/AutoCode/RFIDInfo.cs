using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class RFIDInfo
    {
        public RFIDInfo() { }

        public RFIDInfo(string tID, string ePC, DateTime lastUpdatedDate)
        {
            this.TID = tID;
            this.EPC = ePC;
            this.LastUpdatedDate = lastUpdatedDate;
        }

        public string TID { get; set; }
        public string EPC { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
