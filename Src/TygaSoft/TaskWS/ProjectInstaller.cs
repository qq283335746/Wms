using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace TygaSoft.TaskWS.Wms
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
            service.ServiceName = "TygaSoft.Wms.Service";
            service.Description = "矽云科技后台服务：为仓储配送一体化平台（Wms）提供后台运行支持！技术支持：天涯孤岸，QQ283335746";
            Installers.Add(process);
            Installers.Add(service);
        }
    }
}
