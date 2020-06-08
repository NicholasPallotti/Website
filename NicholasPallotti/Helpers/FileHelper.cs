using NicholasPallotti.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace NicholasPallotti
{
    public class FileHelper
    {
        public static void savePackageListToJsonFile(List<Package> packages)
        {
            PackageData packageData = new PackageData();

            foreach (Package item in packages)
            {
                packageData.Packages.Add(item);
            }

            string filePath = HttpContext.Current.Server.MapPath("~/App_Data") + "\\PackageList.json";

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented
            };

            string json = JsonConvert.SerializeObject(packageData, settings);

            //Save our json string as a file on disk
            using (StreamWriter streamWriter = new StreamWriter(filePath))
            {
                streamWriter.Write(json);
            }
        }

        public static List<Package> GetPackageListFromJsonFile()
        {
            PackageData packageData = new PackageData();

            string filePath = HttpContext.Current.Server.MapPath("~/App_Data") + "\\PackageList.json";

            //Look for a file
            if (File.Exists(filePath))
            {
                //Read the json file back in
                string json;
                using (StreamReader streamReader = new StreamReader(filePath))
                {
                    json = streamReader.ReadToEnd();
                }

                //required to preserve the correct inherited types
                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                };

                //Get an instance of PersonList by calling Deserialize
                packageData = JsonConvert.DeserializeObject<PackageData>(json, settings);
            }

            //if there is no existing file, we are just returning an empty list of persons
            return packageData.Packages;
        }
    }
}