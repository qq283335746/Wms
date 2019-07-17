using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TygaSoft.Model;
using TygaSoft.BLL;

namespace TygaSoft.WebHelper
{
    public class PrintOrder
    {
        public PrintOrderInfo GetPrintPreOrderReceipt(Guid Id)
        {
            var data = new PrintOrderInfo();
            data.Title = "预收货单";
            data.SPrintDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

            var bll = new OrderReceipt();
            var oInfo = bll.GetModel(Id);

            data.OrderCode = oInfo.OrderCode;
            data.PurchaseOrderCode = "";
            data.SupplierName = "";
            data.SPlanArrivalTime = "";
            data.SPlanArrivalTime = "";

            var t1 = Task.Factory.StartNew(() =>
            {
                BarcodeHelper bh = new BarcodeHelper();
                data.BarcodeImageUri = bh.CreateBarcode(data.OrderCode);
            });

            var orpBll = new OrderReceiptProduct();
            var sqlWhere = "and orp.OrderId = @OrderId ";
            var parm = new SqlParameter("@OrderId", oInfo.Id);
            var pList = orpBll.GetListByJoin(sqlWhere, parm);

            if (pList != null && pList.Count > 0)
            {
                var cargoList = new List<PrintOrderCargoInfo>();
                foreach (var item in pList)
                {
                    cargoList.Add(new PrintOrderCargoInfo("", item.ProductCode, item.ProductName, "", "", item.PackageCode, item.PackageName, item.Unit, 0, item.ExpectedQty, ""));
                }
                data.CargoList = cargoList;
                //data.CargoList = JsonConvert.SerializeObject(cargoList);
            }

            t1.Wait();

            return data;
        }

        public PrintOrderInfo GetPrintOrderReceipt(Guid Id)
        {
            var data = new PrintOrderInfo();
            data.Title = "收货单";
            data.SPrintDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

            var bll = new OrderReceipt();
            var oInfo = bll.GetModel(Id);

            data.OrderCode = oInfo.OrderCode;
            data.PurchaseOrderCode = "";
            data.SupplierName = "";
            data.SPlanArrivalTime = "";
            data.SPlanArrivalTime = "";

            var t1 = Task.Factory.StartNew(() =>
            {
                BarcodeHelper bh = new BarcodeHelper();
                data.BarcodeImageUri = bh.CreateBarcode(data.OrderCode);
            });

            var orpBll = new OrderReceiptProduct();
            var sqlWhere = "and orp.OrderId = @OrderId ";
            var parm = new SqlParameter("@OrderId", oInfo.Id);
            var pList = orpBll.GetListByJoin(sqlWhere, parm);

            if (pList != null && pList.Count > 0)
            {
                var cargoList = new List<PrintOrderCargoInfo>();
                foreach (var item in pList)
                {
                    cargoList.Add(new PrintOrderCargoInfo("", item.ProductCode, item.ProductName, "", "", item.PackageCode, item.PackageName, item.Unit, item.ExpectedQty, item.ReceiptQty, ""));
                }
                data.CargoList = cargoList;
                //data.CargoList = JsonConvert.SerializeObject(cargoList);
            }

            t1.Wait();

            return data;
        }

        public PrintOrderInfo GetPrintShelfMission(Guid Id)
        {
            var data = new PrintOrderInfo();
            data.Title = "上架单";
            data.SPrintDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

            var bll = new ShelfMission();
            var shelfMissionInfo = bll.GetModel(Id);

            data.OrderCode = shelfMissionInfo.OrderCode;
            data.PurchaseOrderCode = "";
            data.SupplierName = "";
            data.SPlanArrivalTime = "";
            data.SPlanArrivalTime = "";

            var t1 = Task.Factory.StartNew(() =>
            {
                BarcodeHelper bh = new BarcodeHelper();
                data.BarcodeImageUri = bh.CreateBarcode(data.OrderCode);
            });

            var smpBll = new ShelfMissionProduct();
            var sqlWhere = "and smp.ShelfMissionId = @ShelfMissionId ";
            var parm = new SqlParameter("@ShelfMissionId", shelfMissionInfo.Id);
            var pList = smpBll.GetListByJoin(sqlWhere, parm);

            if (pList != null && pList.Count > 0)
            {
                var cargoList = new List<PrintOrderCargoInfo>();
                foreach (var item in pList)
                {
                    cargoList.Add(new PrintOrderCargoInfo(item.OrderReceiptCode, item.ProductCode, item.ProductName, "", "", "", "", "", item.StayQty, item.Qty, ""));
                }
                data.CargoList = cargoList;
                //data.CargoList = JsonConvert.SerializeObject(cargoList);
            }

            t1.Wait();

            return data;
        }

        public PrintOrderInfo GetPrintOrderSend(Guid Id)
        {
            var data = new PrintOrderInfo();
            data.Title = "发货单";
            data.SPrintDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

            var bll = new OrderSend();
            var oInfo = bll.GetModel(Id);

            data.OrderCode = oInfo.OrderCode;
            data.PurchaseOrderCode = "";
            data.SupplierName = "";
            data.SPlanArrivalTime = "";
            data.SPlanArrivalTime = "";

            Task[] tasks = new Task[2];
            tasks[0] = Task.Factory.StartNew(() =>
            {
                var bh = new BarcodeHelper();
                data.BarcodeImageUri = bh.CreateBarcode(data.OrderCode);
            });
            tasks[1] = Task.Factory.StartNew(() =>
            {
                var ospBll = new OrderSendProduct();
                var sqlWhere = "and osp.OrderId = @OrderId ";
                var parm = new SqlParameter("@OrderId", oInfo.Id);
                var pList = ospBll.GetListByJoin(sqlWhere, parm);

                if (pList != null && pList.Count > 0)
                {
                    var cargoList = new List<PrintOrderCargoInfo>();
                    foreach (var item in pList)
                    {
                        cargoList.Add(new PrintOrderCargoInfo("", item.ProductCode, item.ProductName, item.CustomerCode, item.CustomerName, "", "", "", 0, item.Qty, ""));
                    }
                    data.CargoList = cargoList;
                    //data.CargoList = JsonConvert.SerializeObject(cargoList);
                }
            });

            Task.WaitAll(tasks);

            return data;
        }

        public PrintOrderInfo GetPrintOrderPick(Guid Id)
        {
            var data = new PrintOrderInfo();
            data.Title = "拣货单";
            data.SPrintDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

            var bll = new OrderPicked();
            var oInfo = bll.GetModel(Id);

            data.OrderCode = oInfo.OrderCode;
            data.PurchaseOrderCode = "";
            data.SupplierName = "";
            data.SPlanArrivalTime = "";
            data.SPlanArrivalTime = "";

            var t1 = Task.Factory.StartNew(() =>
            {
                BarcodeHelper bh = new BarcodeHelper();
                data.BarcodeImageUri = bh.CreateBarcode(data.OrderCode);
            });

            var oppBll = new OrderPickProduct();
            var sqlWhere = "and opp.OrderPickId = @OrderId ";
            var parm = new SqlParameter("@OrderId", oInfo.Id);
            var pList = oppBll.GetListByJoin(sqlWhere, parm);

            if (pList != null && pList.Count > 0)
            {
                var cargoList = new List<PrintOrderCargoInfo>();
                foreach (var item in pList)
                {
                    cargoList.Add(new PrintOrderCargoInfo("", item.ProductCode, item.ProductName, item.CustomerCode, item.CustomerName, "", "", "", item.StayQty, item.Qty, ""));
                }
                data.CargoList = cargoList;
                //data.CargoList = JsonConvert.SerializeObject(cargoList);
            }

            t1.Wait();

            return data;
        }
    }
}
