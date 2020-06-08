using NicholasPallotti.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Configuration;

namespace NicholasPallotti.Helpers
{
    public class PackageDataAccess
    {
        public static void InsertPackage(Package package)
        {
            PersonDataAccess.InsertPerson(package.Sender);
            PersonDataAccess.InsertPerson(package.Recipient);

            using (SqlConnection dbConnection = new SqlConnection(GetConnectionString()))
            {
                string sql = @"INSERT INTO PackageTable(UniqueId, SenderUniqueId, DestinationUniqueId, Weight, CostPerOunce, PackageType)
                               VALUES(@UniqueId, @SenderUniqueId, @DestinationUniqueId, @Weight, @CostPerOunce, @PackageType)";

                string packageType = Package.getType(package);

                //Open the database
                dbConnection.Open();

                //Get a Sql Command object to hold the SQL query we want to run
                using (SqlCommand command = new SqlCommand(sql, dbConnection))
                {
                    //fill up the parameters with the values from the person properties
                    command.Parameters.Add(new SqlParameter("@UniqueId", package.uniqueId));
                    command.Parameters.Add(new SqlParameter("@SenderUniqueId", package.Sender.UniqueId));
                    command.Parameters.Add(new SqlParameter("@DestinationUniqueId", package.Recipient.UniqueId));
                    command.Parameters.Add(new SqlParameter("@Weight", package.weight));
                    command.Parameters.Add(new SqlParameter("@CostPerOunce", package.costPerOunce));
                    command.Parameters.Add(new SqlParameter("@PackageType", packageType));

                    int rowsEffected = command.ExecuteNonQuery();
                }

            }
        }

        public static List<Package> GetPackageList()
        {
            List<Package> packages = new List<Package>();

            using (SqlConnection dbConnection = new SqlConnection(GetConnectionString()))
            {
                string sql = @"SELECT *
                               FROM PackageTable";

                dbConnection.Open();

                using (SqlCommand command = new SqlCommand(sql, dbConnection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string packageType = reader.GetString(reader.GetOrdinal("PackageType"));

                        switch (packageType)
                        {
                            case "package":
                                Package standard = new Package();
                                MapPackage(standard, reader);

                                packages.Add(standard);
                                break;
                            case "TwoDayPackage":
                                TwoDayPackage twoDayPackage = new TwoDayPackage();
                                MapPackage(twoDayPackage, reader);
                                packages.Add(twoDayPackage);
                                break;
                            case "Overnight":
                                OvernightPackage overnightPackage = new OvernightPackage();
                                MapPackage(overnightPackage, reader);
                                packages.Add(overnightPackage);
                                break;
                        }

                        //Package package = new Package();

                        //MapPackage(package, reader);
                        //packages.Add(package);
                    }
                }
                dbConnection.Close();
            }

            return packages;
        }

        private static void MapPackage(Package package, SqlDataReader reader)
        {
            //convert and map from sql server types and names to C# types and names
            package.uniqueId = reader.GetGuid(reader.GetOrdinal("UniqueId")).ToString();

            string senderId = reader.GetGuid(reader.GetOrdinal("SenderUniqueId")).ToString();
            package.Sender = PersonDataAccess.GetPerson(senderId);

            string recipientId = reader.GetGuid(reader.GetOrdinal("DestinationUniqueId")).ToString();
            package.Recipient = PersonDataAccess.GetPerson(recipientId);
            
            package.weight = reader.GetDecimal(reader.GetOrdinal("Weight"));
            package.costPerOunce = reader.GetDecimal(reader.GetOrdinal("CostPerOunce"));

            //package.Sender = PersonDataAccess.GetPerson(package.Sender.UniqueId);
            //package.Recipient = PersonDataAccess.GetPerson(package.Recipient.UniqueId);
        }

        public static Package GetPackage(string UniqueId)
        {
            Package result = null;

            using (SqlConnection dbConnection = new SqlConnection(GetConnectionString()))
            {
                string sql = @"SELECT *
                               FROM PackageTable
                               WHERE UniqueId = @UniqueId";

                dbConnection.Open();

                using (SqlCommand command = new SqlCommand(sql, dbConnection))
                {
                    command.Parameters.Add(new SqlParameter("@UniqueId", UniqueId));

                    //A data reader is a class that runs the Sql command we have given it
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string packageType = reader.GetString(reader.GetOrdinal("PackageType"));

                        switch (packageType)
                        {
                            case "package":
                                Package package = new Package();
                                MapPackage(package, reader);

                                result = package;
                                break;
                            case "TwoDayPackage":
                                TwoDayPackage twoDayPackage = new TwoDayPackage();
                                MapPackage(twoDayPackage, reader);
                                result = twoDayPackage;
                                break;
                            case "Overnight":
                                OvernightPackage overnightPackage = new OvernightPackage();
                                MapPackage(overnightPackage, reader);
                                result = overnightPackage;
                                break;
                        }

                        //MapPackage(result, reader);
                    }
                }
                dbConnection.Close();
            }

            return result;
        }


        public static void UpdatePackage(Package package)
        {

            PersonDataAccess.UpdatePerson(package.Sender);
            PersonDataAccess.UpdatePerson(package.Recipient);

            using (SqlConnection dbConnection = new SqlConnection(GetConnectionString()))
            {
                string sql = @"UPDATE PackageTable SET SenderUniqueId = @SenderUniqueId, DestinationUniqueId = @DestinationUniqueId, 
                               Weight = @weight, CostPerOunce = @costPerOunce
                               WHERE UniqueId = @UniqueId";

                string packageType = Package.getType(package);

                dbConnection.Open();

                using (SqlCommand command = new SqlCommand(sql, dbConnection))
                {
                    //fill up the parameters with the values from the person properties
                    command.Parameters.Add(new SqlParameter("@SenderUniqueId", package.Sender.UniqueId));
                    command.Parameters.Add(new SqlParameter("@DestinationUniqueId", package.Recipient.UniqueId));
                    command.Parameters.Add(new SqlParameter("@CostPerOunce", package.costPerOunce));
                    command.Parameters.Add(new SqlParameter("@Weight", package.weight));
                    command.Parameters.Add(new SqlParameter("@UniqueId", package.uniqueId));

                    //PersonDataAccess.UpdatePerson(package.Sender);
                    //PersonDataAccess.UpdatePerson(package.Recipient);

                    int rowsEffected = command.ExecuteNonQuery();
                }

                //Close the database
                dbConnection.Close();
            }
        }

        public static void DeletePackage(Package package)
        {
            using (SqlConnection dbConnection = new SqlConnection(GetConnectionString()))
            {
                string sql = @"DELETE FROM PackageTable
                               WHERE UniqueId = @UniqueId";

                dbConnection.Open();

                using (SqlCommand command = new SqlCommand(sql, dbConnection))
                {
                    //string senderId = null;
                    //string recipientId = null;

                    command.Parameters.Add(new SqlParameter("@UniqueId", package.uniqueId));
                    //command.Parameters.Add(new SqlParameter("@SenderUniqueId", senderId));
                    //command.Parameters.Add(new SqlParameter("@DestinationUniqueId", recipientId));

                    PersonDataAccess.DeletePerson(package.Sender.UniqueId);
                    PersonDataAccess.DeletePerson(package.Recipient.UniqueId);

                    int rowsEffected = command.ExecuteNonQuery();
                }

                dbConnection.Close();
            }
        }

        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["PackageConnectionString"].ConnectionString;
        }
    }
}