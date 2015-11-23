using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DemoApplication.Controllers
{
    public class HeartbeatController : ApiController
    {
        public string Get()
        {
            return "PID: " + Process.GetCurrentProcess().Id.ToString() + " time: " + DateTimeOffset.Now.ToString("G");
        }
    }
}
