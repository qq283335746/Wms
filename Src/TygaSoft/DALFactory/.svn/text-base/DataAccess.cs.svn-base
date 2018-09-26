using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Reflection;
using TygaSoft.IDAL;

namespace TygaSoft.DALFactory
{
    public sealed class DataAccess
    {
        private static readonly string path = ConfigurationManager.AppSettings["WebDAL"];

        #region 公共

        public static IOrderMap CreateOrderMap()
        {
            string className = path + ".OrderMap";
            return (IOrderMap)Assembly.Load(path).CreateInstance(className);
        }

        public static IRFID CreateRFID()
        {
            string className = path + ".RFID";
            return (IRFID)Assembly.Load(path).CreateInstance(className);
        }

        public static IRole CreateRole()
        {
            string className = path + ".Role";
            return (IRole)Assembly.Load(path).CreateInstance(className);
        }

        public static IStockLocationProduct CreateStockLocationProduct()
        {
            string className = path + ".StockLocationProduct";
            return (IStockLocationProduct)Assembly.Load(path).CreateInstance(className);
        }

        public static ISitePicture CreateSitePicture()
        {
            string className = path + ".SitePicture";
            return (ISitePicture)Assembly.Load(path).CreateInstance(className);
        }

        public static IBarcodeTemplate CreateBarcodeTemplate()
        {
            string className = path + ".BarcodeTemplate";
            return (IBarcodeTemplate)Assembly.Load(path).CreateInstance(className);
        }

        public static IOrderRandom CreateOrderRandom()
        {
            string className = path + ".OrderRandom";
            return (IOrderRandom)Assembly.Load(path).CreateInstance(className);
        }

        public static ILogisticsDistribution CreateLogisticsDistribution()
        {
            string className = path + ".LogisticsDistribution";
            return (ILogisticsDistribution)Assembly.Load(path).CreateInstance(className);
        }

        public static IFeatureUser CreateFeatureUser()
        {
            string className = path + ".FeatureUser";
            return (IFeatureUser)Assembly.Load(path).CreateInstance(className);
        }

        public static IFeaturePicture CreateFeaturePicture()
        {
            string className = path + ".FeaturePicture";
            return (IFeaturePicture)Assembly.Load(path).CreateInstance(className);
        }

        #endregion

        #region 系统管理

        public static ISiteMulti CreateSiteMulti()
        {
            string className = path + ".SiteMulti";
            return (ISiteMulti)Assembly.Load(path).CreateInstance(className);
        }

        public static ISiteMenus CreateSiteMenus()
        {
            string className = path + ".SiteMenus";
            return (ISiteMenus)Assembly.Load(path).CreateInstance(className);
        }

        public static ISiteMenusAccess CreateSiteMenusAccess()
        {
            string className = path + ".SiteMenusAccess";
            return (ISiteMenusAccess)Assembly.Load(path).CreateInstance(className);
        }

        #endregion

        #region 成员资格

        public static ISiteRoles CreateSiteRoles()
        {
            string className = path + ".SiteRoles";
            return (ISiteRoles)Assembly.Load(path).CreateInstance(className);
        }

        public static IApplications CreateApplications()
        {
            string className = path + ".Applications";
            return (IApplications)Assembly.Load(path).CreateInstance(className);
        }

        #endregion

        #region 基础数据

        public static IPackage CreatePackage()
        {
            string className = path + ".Package";
            return (IPackage)Assembly.Load(path).CreateInstance(className);
        }

        public static ICustomer CreateCustomer()
        {
            string className = path + ".Customer";
            return (ICustomer)Assembly.Load(path).CreateInstance(className);
        }

        public static IZone CreateZone()
        {
            string className = path + ".Zone";
            return (IZone)Assembly.Load(path).CreateInstance(className);
        }

        public static IStockLocation CreateStockLocation()
        {
            string className = path + ".StockLocation";
            return (IStockLocation)Assembly.Load(path).CreateInstance(className);
        }
        public static IStockLocationCtr CreateStockLocationCtr()
        {
            string className = path + ".StockLocationCtr";
            return (IStockLocationCtr)Assembly.Load(path).CreateInstance(className);
        }

        public static ICategory CreateCategory()
        {
            string className = path + ".Category";
            return (ICategory)Assembly.Load(path).CreateInstance(className);
        }

        public static IProduct CreateProduct()
        {
            string className = path + ".Product";
            return (IProduct)Assembly.Load(path).CreateInstance(className);
        }

        public static ISupplier CreateSupplier()
        {
            string className = path + ".Supplier";
            return (ISupplier)Assembly.Load(path).CreateInstance(className);
        }

        public static IStockWarning CreateStockWarning()
        {
            string className = path + ".StockWarning";
            return (IStockWarning)Assembly.Load(path).CreateInstance(className);
        }

        public static ICompany CreateCompany()
        {
            string className = path + ".Company";
            return (ICompany)Assembly.Load(path).CreateInstance(className);
        }

        public static IVehicle CreateVehicle()
        {
            string className = path + ".Vehicle";
            return (IVehicle)Assembly.Load(path).CreateInstance(className);
        }

        #endregion

        #region 预收货



        #endregion

        #region 收货

        public static IOrderReceipt CreateOrderReceipt()
        {
            string className = path + ".OrderReceipt";
            return (IOrderReceipt)Assembly.Load(path).CreateInstance(className);
        }

        public static IOrderReceiptAttr CreateOrderReceiptAttr()
        {
            string className = path + ".OrderReceiptAttr";
            return (IOrderReceiptAttr)Assembly.Load(path).CreateInstance(className);
        }

        public static IOrderReceiptProduct CreateOrderReceiptProduct()
        {
            string className = path + ".OrderReceiptProduct";
            return (IOrderReceiptProduct)Assembly.Load(path).CreateInstance(className);
        }

        public static IOrderReceiptProductAttr CreateOrderReceiptProductAttr()
        {
            string className = path + ".OrderReceiptProductAttr";
            return (IOrderReceiptProductAttr)Assembly.Load(path).CreateInstance(className);
        }

        public static IOrderReceiptProductQuality CreateOrderReceiptProductQuality()
        {
            string className = path + ".OrderReceiptProductQuality";
            return (IOrderReceiptProductQuality)Assembly.Load(path).CreateInstance(className);
        }

        public static IOrderReceiptRecord CreateOrderReceiptRecord()
        {
            string className = path + ".OrderReceiptRecord";
            return (IOrderReceiptRecord)Assembly.Load(path).CreateInstance(className);
        }

        #endregion

        #region 库存

        public static IStockProduct CreateStockProduct()
        {
            string className = path + ".StockProduct";
            return (IStockProduct)Assembly.Load(path).CreateInstance(className);
        }

        #endregion

        #region 上架任务

        public static IShelfMissionProduct CreateShelfMissionProduct()
        {
            string className = path + ".ShelfMissionProduct";
            return (IShelfMissionProduct)Assembly.Load(path).CreateInstance(className);
        }

        public static IShelfMission CreateShelfMission()
        {
            string className = path + ".ShelfMission";
            return (IShelfMission)Assembly.Load(path).CreateInstance(className);
        }

        #endregion

        #region 发货

        public static IOrderSend CreateOrderSend()
        {
            string className = path + ".OrderSend";
            return (IOrderSend)Assembly.Load(path).CreateInstance(className);
        }
        public static IOrderSendProduct CreateOrderSendProduct()
        {
            string className = path + ".OrderSendProduct";
            return (IOrderSendProduct)Assembly.Load(path).CreateInstance(className);
        }

        #endregion

        #region 拣货

        public static IOrderPicked CreateOrderPicked()
        {
            string className = path + ".OrderPicked";
            return (IOrderPicked)Assembly.Load(path).CreateInstance(className);
        }
        public static IOrderPickProduct CreateOrderPickProduct()
        {
            string className = path + ".OrderPickProduct";
            return (IOrderPickProduct)Assembly.Load(path).CreateInstance(className);
        }

        #endregion

        #region 盘点

        public static IPandian CreatePandian()
        {
            string className = path + ".Pandian";
            return (IPandian)Assembly.Load(path).CreateInstance(className);
        }

        public static IPandianProduct CreatePandianProduct()
        {
            string className = path + ".PandianProduct";
            return (IPandianProduct)Assembly.Load(path).CreateInstance(className);
        }

        #endregion

        #region MES

        public static IMesCategory CreateMesCategory()
        {
            string className = path + ".MesCategory";
            return (IMesCategory)Assembly.Load(path).CreateInstance(className);
        }

        public static IMesProduct CreateMesProduct()
        {
            string className = path + ".MesProduct";
            return (IMesProduct)Assembly.Load(path).CreateInstance(className);
        }

        public static IMesOrder CreateMesOrder()
        {
            string className = path + ".MesOrder";
            return (IMesOrder)Assembly.Load(path).CreateInstance(className);
        }

        #endregion

        #region 广州矽云

        public static IInfoneCustomer CreateInfoneCustomer()
        {
            string className = path + ".InfoneCustomer";
            return (IInfoneCustomer)Assembly.Load(path).CreateInstance(className);
        }
        public static IInfoneProjectReportPrepare CreateInfoneProjectReportPrepare()
        {
            string className = path + ".InfoneProjectReportPrepare";
            return (IInfoneProjectReportPrepare)Assembly.Load(path).CreateInstance(className);
        }
        public static IInfoneDeviceRepairRecord CreateInfoneDeviceRepairRecord()
        {
            string className = path + ".InfoneDeviceRepairRecord";
            return (IInfoneDeviceRepairRecord)Assembly.Load(path).CreateInstance(className);
        }
        public static IInfoneDeviceBorrowRecord CreateInfoneDeviceBorrowRecord()
        {
            string className = path + ".InfoneDeviceBorrowRecord";
            return (IInfoneDeviceBorrowRecord)Assembly.Load(path).CreateInstance(className);
        }

        #endregion
    }
}
