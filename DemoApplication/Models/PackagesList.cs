using NuGet;
using System.Collections.Generic;

namespace DemoApplication.Models
{
    public class PackagesList
    {
        private class PackageEntry
        {
            public string Id;
            public string Query; 
        }

        private IList<PackageEntry> packages = new List<PackageEntry>();

        public delegate void AddPackageDelegate(PackagesList self, string query, IPackage package);

        public event AddPackageDelegate AddPackage;

        public void Add(string query, IPackage package)
        {
            this.packages.Add(new PackageEntry() { Id = package.Id, Query = query});
            this.AddPackage(this, query, package);
        }

        public bool Contains(string id)
        {
            bool result = false;

            foreach (var package in this.packages)
            {
                if (package.Id == id)
                {
                    result = true;
                }
            }

            return result;
        }
    }
}
