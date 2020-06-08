using System;
using System.Collections.Generic;

namespace NicholasPallotti.Models
{
    public class PackageListViewModel
    {
        public List<Package> Packages { get; set; }

        public PackageListViewModel()
        {
            Packages = new List<Package>();
        }
    }
}