using NuGet;
using System.Collections.Generic;

namespace DemoApplication.Models
{
    public class PackagesList
    {
        private IList<IPackage> packages = new List<IPackage>();

        public delegate void AddPackageDelegate(PackagesList self, IPackage package);

        public event AddPackageDelegate AddPackage;

        public void Add(IPackage package)
        {
            this.packages.Add(package);
            this.AddPackage(this, package);
        }

        public void AddRange(IEnumerable<IPackage> packageList)
        {
            this.packages.AddRange(packageList);
            foreach (var p in packageList)
            {
                this.AddPackage(this, p);
            }
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
