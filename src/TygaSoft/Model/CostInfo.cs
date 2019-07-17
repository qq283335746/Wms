using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TygaSoft.Model
{
    public class CostInfo
    {
        public object Id { get; set; }
        public string HuopinCode { get; set; }
        public string HuopinName { get; set; }
        public string Capacity { get; set; }
        public DateTime RukuStartDate { get; set; }
        public string SRukuStartDate { get; set; }
        public DateTime RukuEndDate { get; set; }
        public string SRukuEndDate { get; set; }
        public decimal UnitPrice { get; set; }
        public string SUnitPrice { get; set; }
        public string PayStatus { get; set; }
    }
}
