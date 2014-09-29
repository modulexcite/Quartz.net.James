using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Topshelf;
using Topshelf.Shelving;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using log4net;

namespace Topshelf.WCF
{
    public class CoolServiceBootstrapper : Bootstrapper<WebServiceHost>
    {
        private ILog log4netLogger = LogManager.GetLogger(typeof(CoolServiceBootstrapper));
        public static WebServiceHost WebService = null;
        public void InitializeHostedService(Topshelf.Configuration.Dsl.IServiceConfigurator<WebServiceHost> cfg)
        {
            log4net.Config.XmlConfigurator.Configure();
            cfg.Named("Topshelf.WCF.CoolService");
            cfg.HowToBuildService(n =>
            {
                if (WebService != null)
                {
                    WebService.Close();
                }
                WebService = new WebServiceHost(typeof(CoolService));

                log4netLogger.Info("Test cool service created");
                log4netLogger.Info("Test cool service endpoint: " + WebService.Description.Endpoints.Select(e => e.ListenUri.ToString()).Aggregate((e, ne) => e + ", " + ne));

                return WebService;
            });
            cfg.WhenStarted(s =>
            {
                s.Open();
                log4netLogger.Info("Test cool service started");
            });
            cfg.WhenStopped(s =>
            {
                s.Close();
                s = null;
            });
        }
    }
}