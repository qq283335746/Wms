using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TygaSoft.BLL
{
    public class RandomCode
    {
        #region OrderNum Member

        OrderRandom o = new OrderRandom();

        public string GetOrderCode(string prefix)
        {
            var orderCode = string.Empty;

            Monitor.Enter(o);

            orderCode = o.GetOrderCode(prefix);

            Monitor.Exit(o);

            return orderCode;
        }

        public string GetRndCodeByDateTime(string prefix)
        {
            var orderCode = string.Empty;

            Monitor.Enter(o);

            orderCode = o.GetRndCodeByDateTime(prefix);

            Monitor.Exit(o);

            return orderCode;
        }

        #endregion
    }
}
