using NuGet;
using System.Collections.Generic;

namespace DemoApplication.Models
{
    public class PackagesList
    {
        private IDictionary<string, string> packages = new Dictionary<string, string>();

        public delegate void AddPackageDelegate(PackagesList self, string query, IPackage package);

        public event AddPackageDelegate AddPackage;

        public void Add(string query, IPackage package)
        {
            this.packages.Add(package.Id, query);
            this.AddPackage(this, query, package);
        }

        public bool Contains(string id)
        {
            bool result = false;

            foreach (var package in this.packages)
            {
                if (package.Key == id)
                {
                    result = true;
                }
            }

            return result;
        }
    }
}
