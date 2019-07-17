using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using TygaSoft.IDAL;
using TygaSoft.Model;
using TygaSoft.DALFactory;

namespace TygaSoft.BLL
{
    public partial class OrderRandom
    {
        #region OrderRandom Member

        private static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();

        public string GetOrderCode(string prefix)
        {
            while (true)
            {
                byte[] rndNum = new byte[13];
                rngCsp.GetBytes(rndNum);
                var rnd = new Random(BitConverter.ToInt32(rndNum, 0));

                var orderCode = string.Format("{0}{1}", prefix, (rnd.NextDouble() * int.MaxValue).ToString().PadLeft(10, '0'));
                if (!dal.IsExist(orderCode))
                {
                    var model = new OrderRandomInfo();
                    model.OrderCode = orderCode;
                    model.Prefix = prefix;
                    model.LastUpdatedDate = DateTime.Now;
                    Insert(model);

                    return orderCode;
                }
            }
        }

        public string GetRndCodeByDateTime(string prefix)
        {
            while (true)
            {
                var sPre = string.Format("{0}{1}", prefix, DateTime.Now.ToString("yyMMddHHmm"));
                var orderCode = string.Format("{0}{1}", sPre, dal.GetMax(sPre).ToString().PadLeft(3, '0'));
                if (!dal.IsExist(orderCode))
                {
                    Insert(new OrderRandomInfo(orderCode, prefix, DateTime.Now));

                    return orderCode;
                }
            }
        }

        #endregion
    }
}
