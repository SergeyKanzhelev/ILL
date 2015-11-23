using DemoApplication.Models;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using NuGet;
using SignalRChat;
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

                    foreach (var p in packages)
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
    }
}
