using DemoApplication.Models;
using NuGet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Http;
using System.Web.Mvc;

namespace DemoApplication.Controllers
{
    public class PackagesController : Controller
    {
        // GET: Packages
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Post([FromBody]string searchTerm)
        {
            return View();
        }
    }
}