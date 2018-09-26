using System.Configuration;
using System.Messaging;
using System.ServiceModel;
using System.ServiceProcess;

namespace TygaSoft.WcfWS
{
    public class Service : ServiceBase
    {
        public Service()
        {
            ServiceName = "Wms.Service";
        }

        public static void Main()
        {
            ServiceBase.Run(new Service());
        }

        protected override void OnStart(string[] args)
        {
            
        }

        protected override void OnStop()
        {
            
        }
    }
}
