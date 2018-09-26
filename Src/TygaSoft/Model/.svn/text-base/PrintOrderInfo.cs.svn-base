using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TygaSoft.Model
{
    [Serializable]
    public class PrintOrderInfo
    {
        #region 单号信息

        public string Title { get; set; }

        public string BarcodeImageUri { get; set; }

        public string SPrintDate { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderCode { get; set; }

        /// <summary>
        /// 采购订单号
        /// </summary>
        public string PurchaseOrderCode { get; set; }

        /// <summary>
        /// 计划到货时间
        /// </summary>
        public string SPlanArrivalTime { get; set; }

        /// <summary>
        /// 实际到货时间
        /// </summary>
        public string SActualArrivalTime { get; set; }

        /// <summary>
        /// 供应商
        /// </summary>
        public string SupplierName { get; set; }

        public List<PrintOrderCargoInfo> CargoList { get; set; }

        //public string CargoList { get; set; }

        #endregion
    }

    [Serializable]
    public class PrintOrderCargoInfo
    {
        public PrintOrderCargoInfo() { }
        public PrintOrderCargoInfo(string orderCode,string productCode,string productName,string customerCode,string customerName,string packageCode,string packageName,string unitName, double stayQty, double qty, string storageName)
        {
            this.OrderCode = orderCode;
            this.ProductCode = productCode;
            this.ProductName = productName;
            this.CustomerCode = customerCode;
            this.CustomerName = customerName;
            this.PackageCode = packageCode;
            this.PackageName = packageName;
            this.UnitName = unitName;
            this.StayQty = stayQty;
            this.Qty = qty;
            this.StorageName = storageName;
        }

        public string OrderCode { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string PackageCode { get; set; }
        public string PackageName { get; set; }
        public string UnitName { get; set; }
        public double StayQty { get; set; }
        public double Qty { get; set; }
        public string StorageName { get; set; }
    }
}
