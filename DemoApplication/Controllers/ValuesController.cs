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
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            if (id == 1)
            {
                throw new InvalidOperationException();
            }

            if (id == 2)
            {

                ThreadStart ts = new ThreadStart(ThreadWorker);
                Thread thread = new Thread(ts);
                thread.Start();


                Task.Factory.StartNew(() => { throw new Exception(); });

                Task t1 = new Task(() =>
                {
                    throw new Exception("Fire-and-forget thread faulted!");
                });
                t1.Start();


                List<Task> tasks = new List<Task>();
                var taskFactory = new TaskFactory();

                tasks.Add(taskFactory.StartNew(() =>
                {
                    throw new Exception("Thread failed!");
                }));
                tasks.Add(taskFactory.StartNew(() =>
                {
                    throw new Exception("Yet another thread failed!");
                }));

                HttpClient client = new HttpClient();
                client.GetStringAsync("http://localhost:27117/api/values/1");
            }

            return Process.GetCurrentProcess().Id.ToString();
        }


        void ThreadWorker()
        {
            Thread.Sleep(1000);
            throw new InvalidOperationException();
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
