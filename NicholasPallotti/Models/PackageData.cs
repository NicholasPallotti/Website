using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace NicholasPallotti.Models
{
    [Serializable]
    public class PackageData
    {
        private readonly List<Package> _packages = new List<Package>();
        //object Types
        [XmlArrayItem(Type = typeof(Package))]
        [XmlArrayItem(Type = typeof(TwoDayPackage))]
        [XmlArrayItem(Type = typeof(OvernightPackage))]
        public List<Package> Packages
        {
            get
            {
                return _packages;
            }
        }
    }
}