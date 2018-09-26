using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;
using System.Configuration;
using System.ServiceModel;
using TygaSoft.WcfService;
using System.Text.RegularExpressions;
using TygaSoft.SysHelper;

namespace TygaSoft.WcfCA
{
    class Program
    {
        static void Main(string[] args)
        {
            var sss = "00000000000012345678912345678912";
            var len = sss.Length;
            var g = Guid.Parse(sss);

            var s = "\u5e7f\u4e1c";
            var sssa = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(s));

            //string sTID = "123456";
            //string sEPC = "1269874";
            //var sUrl = "http://localhost/Wms/Services/PdaService.svc/SaveRFIDQueue";
            //var sData = string.Format(@"{{""model"":{{""TID"":""{0}"",""EPC"":""{1}""}}}}", sTID, sEPC);
            //var statusCode = -1;
            //var result = "";
            //HttpHelper.DoHttpPost(sUrl, sData, "", "application/json; charset=utf-8", out statusCode, out result);

            //var oldList = new List<string>();
            //oldList.Add("最大库存量预警");
            //var list = new List<string>();
            //list.Add("最大库存量预警");
            //bool isaaa = oldList.Equals(list);
            //return;

            //var s = "aaaa12345我我";
            //Regex r = new Regex(@"^[\@A-Za-z0-9_\-\!\#\$\%\^\&\*\.\~]{6,30}$");
            //var f = r.IsMatch(s);
            //return;


            //Random rnd = new Random();
            //int n = 0;
            //while (true)
            //{
            //    n++;
            //    var s = string.Format("{0}", rnd.NextDouble() * int.MaxValue);
            //    Console.WriteLine("{0},{1}位",s,s.Length);
            //    if (n > 100) break;
            //}

            //Console.ReadLine();

            //string sysQueue = ConfigurationManager.AppSettings["SysQueue"];
            //if (!MessageQueue.Exists(sysQueue))
            //    MessageQueue.Create(sysQueue, true);
            //string userBaseQueue = ConfigurationManager.AppSettings["UserBaseQueue"];
            //if (!MessageQueue.Exists(userBaseQueue))
            //    MessageQueue.Create(userBaseQueue, true);
            //string accessStatisticQueue = ConfigurationManager.AppSettings["AccessStatisticQueue"];
            //if (!MessageQueue.Exists(accessStatisticQueue))
            //    MessageQueue.Create(accessStatisticQueue, true);

            //ServiceHost selfHost = new ServiceHost(typeof(PdaService));
            //ServiceHost shopSelfHost = new ServiceHost(typeof(ECShopService));
            //ServiceHost securitySelfHost = new ServiceHost(typeof(WebSecurityService));
            //ServiceHost syslogSelfHost = new ServiceHost(typeof(HnztcSysService));
            //ServiceHost queueSelfHost = new ServiceHost(typeof(HnztcQueueService));

            try
            {
                //selfHost.Open();
                //shopSelfHost.Open();
                //securitySelfHost.Open();
                //syslogSelfHost.Open();
                //queueSelfHost.Open();
                //Console.WriteLine("海南直通车数据服务（直通车功能模块、商城、安全、系统日志、队列等）已启动就绪！");
                //Console.WriteLine("http://x:18881/Services/ECShopService/");
                //Console.WriteLine("http://x:18881/Services/HnztcService/");
                //Console.WriteLine("http://x:18881/Services/HnztcSecurityService/");
                //Console.WriteLine("http://x:18881/Services/HnztcSysService/");
                //Console.WriteLine("http://x:18881/Services/HnztcQueueService/");
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine("终止服务请按任意键！");
                Console.ReadLine();

                //selfHost.Close();
                //shopSelfHost.Close();
                //securitySelfHost.Close();
                //syslogSelfHost.Close();
                //queueSelfHost.Close();
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("发生异常: {0}", ce.Message);
                //selfHost.Abort();
                //shopSelfHost.Abort();
                //securitySelfHost.Abort();
                //syslogSelfHost.Abort();
                //queueSelfHost.Abort();
                Console.ReadLine();
            }
        }
    }
}
