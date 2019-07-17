using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TygaSoft.Model;

namespace TygaSoft.BLL
{
    public partial class HwCost
    {
        public IList<CostInfo> GetList()
        {
            var list = new List<CostInfo>();

            var currentDate = DateTime.Now;
            var currentEndDate = currentDate.AddDays(-15);
            var currDate = currentDate.ToString("yyyy-MM-dd");
            var endDate = currentDate.AddDays(-15).ToString("yyyy-MM-dd");
            int n = 1;
            while (n<10)
            {
                list.Add(new CostInfo { Id = n, HuopinCode = (3126001 + n).ToString(), HuopinName = "时规带" + n + "", Capacity = (12 + n).ToString(), SRukuStartDate = currDate, SRukuEndDate = endDate, UnitPrice = 124 * n, PayStatus = "已支付", RukuStartDate = currentEndDate, RukuEndDate = currentDate });
                n++;
            }

            return list;
        }
    }
}
