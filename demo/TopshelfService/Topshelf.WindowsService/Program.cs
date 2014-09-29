using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using log4net;
using log4net.Config;
using Topshelf;

namespace Topshelf.WindowsService
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlConfigurator.ConfigureAndWatch(new FileInfo(".\\log4net.config"));
            var host = HostFactory.New(x =>
            {
                x.EnableDashboard();
                x.Service<SampleService>(s =>
                {
                    s.SetServiceName("SampleService");
                    s.ConstructUsing(name => new SampleService());
                    s.WhenStarted(tc =>
                    {
                        XmlConfigurator.ConfigureAndWatch(
                            new FileInfo(".\\log4net.config"));
                        tc.Start();
                    });
                    s.WhenStopped(tc => tc.Stop());
                });

                x.RunAsLocalSystem();
                x.SetDescription("SampleService Description");
                x.SetDisplayName("SampleService");
                x.SetServiceName("SampleService");
            });

            host.Run();
        }
    }
}
