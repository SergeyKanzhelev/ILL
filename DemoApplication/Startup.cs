using DemoApplication.Models;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using NuGet;
using Owin;
using SignalRChat;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

[assembly: OwinStartup(typeof(DemoApplication.Startup))]

namespace DemoApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            PackagesList list = (PackagesList)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(PackagesList));
            list.AddPackage += List_AddPackage;

            GlobalHost.DependencyResolver.Register(
                typeof(ChatHub),
                () => new ChatHub(list));

            app.MapSignalR();
        }

        private static void List_AddPackage(PackagesList self, IPackage package)
        {
            Task t = new Task(
            //Thread t = new Thread(new ThreadStart(
                () =>
                {
                    foreach (var dependencySet in package.DependencySets)
                    {
                        foreach (var dependency in dependencySet.Dependencies)
                        {
                            if (!self.Contains(dependency.Id))
                            {
                                IPackageRepository repo = PackageRepositoryFactory.Default.CreateRepository("https://packages.nuget.org/api/v2");
                                var dependencyPackage = repo.FindPackagesById(dependency.Id).FirstOrDefault();

                                if (!self.Contains(dependency.Id))
                                {
                                    self.Add(dependencyPackage);
                                    Console.WriteLine("Add dependency " + dependency.Id + "  for: " + package.Id);
                                }
                            }
                        }
                    }
                });

            t.Start();
        }

    }
}
