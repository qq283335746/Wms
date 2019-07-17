using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace TygaSoft.WcfWS
{
    [RunInstaller(true)]
    public class ProjectInstaller : Installer
    {
        private ServiceProcessInstaller process;
        private ServiceInstaller service;

        public ProjectInstaller()
        {
            process = new ServiceProcessInstaller();
            process.Account = ServiceAccount.LocalSystem;
            service = new ServiceInstaller();
            service.ServiceName = "Wms.Service";
            service.Description = "仓储物流一体化（WMS）服务。技术支持：天涯孤岸，QQ283335746";
            Installers.Add(process);
            Installers.Add(service);
        }
    }
}
