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
        IList<string> NuGetPackages = new List<string>();

        // GET api/values/5
        public string Get(int id)
        {
            if (id == 2)
            {
                ThreadStart ts = new ThreadStart(ThreadWorker);
                Thread thread = new Thread(ts);
                thread.Start();
            }

            return Process.GetCurrentProcess().Id.ToString();
        }


        void ThreadWorker()
        {
            //ID of the package to be looked up
            string packageID = "Microsoft";
            //string packageID = "Microsoft.ApplicationInsights";

            //Connect to the official package repository
            IPackageRepository repo = PackageRepositoryFactory.Default.CreateRepository("https://packages.nuget.org/api/v2");

            //Get the list of all NuGet packages with ID 'EntityFramework'       
            //List<IPackage> packages = repo.FindPackagesById("Microsoft").ToList();

            List<IPackage> packages = repo.Search(packageID, true).ToList();

            //Filter the list of packages that are not Release (Stable) versions
            packages = packages.Where(item => (item.IsReleaseVersion() == false)).ToList();

            PackagesList list = new PackagesList();
            list.AddPackage += List_AddPackage;

            foreach (IPackage p in packages)
            {
                list.Add(p);
            }
        }

        private static void List_AddPackage(PackagesList self, IPackage package)
        {
            Task t1 = new Task(() =>
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
            t1.Start();
        }

    }
}
