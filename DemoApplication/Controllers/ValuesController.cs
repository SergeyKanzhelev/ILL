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
            Thread.Sleep(1000);
            throw new InvalidOperationException();
        }
    }
}
