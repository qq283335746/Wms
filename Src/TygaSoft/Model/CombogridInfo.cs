using System;

namespace TygaSoft.Model
{
    [Serializable]
    public class CombogridInfo
    {
        public CombogridInfo() { }
        public CombogridInfo(string id, string name, string date,object customerId,string customerCode,string customerName)
        {
            this.Id = id;
            this.Name = name;
            this.SDate = date;
            this.CustomerId = customerId;
            this.CustomerCode = customerCode;
            this.CustomerName = customerName;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string SDate { get; set; }
        public object CustomerId { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
    }
}
