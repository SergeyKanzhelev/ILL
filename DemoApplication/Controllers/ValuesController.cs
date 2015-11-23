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
    public class ValuesController : ApiController
    {
        private readonly PackagesList packagesList;

        public ValuesController(PackagesList list)
        {
            this.packagesList = list;

        }

        // GET api/values?q=Microsoft.ApplicationInsights
        public string Get([FromUri]string q)
        {
            IPackageRepository repo = PackageRepositoryFactory.Default.CreateRepository("https://packages.nuget.org/api/v2");

            List<IPackage> packages = repo.Search(q, true).ToList();

            this.packagesList.AddPackage += List_AddPackage;

            this.packagesList.AddRange(packages);

            return Process.GetCurrentProcess().Id.ToString();
        }

        private static void List_AddPackage(PackagesList self, IPackage package)
        {
            //Task t = new Task(
            ThreadStart ts = new ThreadStart(
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
            Thread t = new Thread(ts);

            t.Start();
        }
    }
}
