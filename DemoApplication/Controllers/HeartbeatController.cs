using System;
using System.Diagnostics;
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
