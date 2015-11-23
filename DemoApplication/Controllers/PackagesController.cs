using DemoApplication.Models;
using NuGet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace DemoApplication.Controllers
{
    public class PackagesController : ApiController
    {
        private readonly PackagesList packagesList;

        public PackagesController(PackagesList list)
        {
            this.packagesList = list;

        }

        // GET api/values?q=Microsoft.ApplicationInsights
        public string Get([FromUri]string q)
        {
            Thread t = new Thread(new ThreadStart(
                () =>
                {
                    IPackageRepository repo = PackageRepositoryFactory.Default.CreateRepository("https://packages.nuget.org/api/v2");
                    List<IPackage> packages = repo.Search(q, true).ToList();

                    this.packagesList.AddPackage += List_AddPackage;

                    foreach(var p in packages)
                    {
                        if (!this.packagesList.Contains(p.Id))
                        {
                            this.packagesList.Add(p);
                        }
                        Thread.Sleep(10);
                    }
                }));

            t.Start();

            return Process.GetCurrentProcess().Id.ToString();
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

                                self.AddRange(repo.FindPackagesById(dependency.Id));
                                Console.WriteLine("Add dependency " + dependency.Id + "  for: " + package.Id);
                            }
                        }
                    }
                });

            t.Start();
        }
    }
}
