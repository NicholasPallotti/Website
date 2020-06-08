using NicholasPallotti.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace NicholasPallotti.Helpers
{
    public class PersonDataAccess
    {
        public static void InsertPerson(Person person)
        {
            //get a new connection to the database using the connection string stored in web.config
            using (SqlConnection dbConnection = new SqlConnection(GetConnectionString()))
            {
                //@ is the sql server symbol for a variable
                //@UniqueId means that is a place holder for a value to be supplied later
                //string sql = @"INSERT INTO Person(UniqueId, FirstName, LastName, Address, City, State, ZipCode, Type)
                //               VALUES(@UniqueId, @FirstName, @LastName, @Address, @City, @State, @ZipCode, @Type)";

                //FROM JAY: you had type sort of partially in here
                //and your column names (like ZipCode don't match your table
                string sql = @"INSERT INTO Person(UniqueId, FirstName, LastName, Address, City, State, Zip)
                               VALUES(@UniqueId, @FirstName, @LastName, @Address, @City, @State, @Zip)";

                //string personType = Person.GetType(person);

                //Open the database
                dbConnection.Open();

                //Get a Sql Command object to hold the SQL query we want to run
                using (SqlCommand command = new SqlCommand(sql, dbConnection))
                {
                    //fill up the parameters with the values from the person properties
                    command.Parameters.Add(new SqlParameter("@UniqueId", person.UniqueId));
                    command.Parameters.Add(new SqlParameter("@FirstName", person.FirstName));
                    command.Parameters.Add(new SqlParameter("@LastName", person.LastName));
                    command.Parameters.Add(new SqlParameter("@Address", person.Address));
                    command.Parameters.Add(new SqlParameter("@City", person.City));
                    command.Parameters.Add(new SqlParameter("@State", person.State));
                    command.Parameters.Add(new SqlParameter("@Zip", person.Zip));
                    //command.Parameters.Add(new SqlParameter("@Type", personType));

                    int rowsEffected = command.ExecuteNonQuery();
                }

                //Close the database
                dbConnection.Close();
            }
        }

        public static Person GetPerson(string id)
        {
            Person person = null;

            using (SqlConnection dbConnection = new SqlConnection(GetConnectionString()))
            {

                string sql = @"SELECT *
                             From Person
                             WHERE UniqueId = @UniqueId";

                dbConnection.Open();

                using (SqlCommand command = new SqlCommand(sql, dbConnection))
                {
                    command.Parameters.Add(new SqlParameter("@UniqueId", id));

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        person = new Person();
                        //MapPerson(person, reader);
                        person.UniqueId = reader.GetGuid(reader.GetOrdinal("UniqueId")).ToString();
                        person.FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                        person.LastName = reader.GetString(reader.GetOrdinal("LastName"));
                        person.Address = reader.GetString(reader.GetOrdinal("Address"));
                        person.City = reader.GetString(reader.GetOrdinal("City"));
                        person.State = reader.GetString(reader.GetOrdinal("State"));
                        person.Zip = reader.GetString(reader.GetOrdinal("Zip"));
                        //result = person;
                    }
                }

                dbConnection.Close();
               
            }
                
            return person;
        }

        public static void UpdatePerson(Person person)
        {
            using (SqlConnection dbConnection = new SqlConnection(GetConnectionString()))
            {
                string sql = @"UPDATE Person SET FirstName = @FirstName, LastName = @LastName, 
                               Address = @Address, City = @City, State = @State, Zip = @Zip
                               WHERE UniqueId = @UniqueId";

                dbConnection.Open();

                using (SqlCommand command = new SqlCommand(sql, dbConnection))
                {
                    command.Parameters.Add(new SqlParameter("@FirstName", person.FirstName));
                    command.Parameters.Add(new SqlParameter("@LastName", person.LastName));
                    command.Parameters.Add(new SqlParameter("@Address", person.Address));
                    command.Parameters.Add(new SqlParameter("@City", person.City));
                    command.Parameters.Add(new SqlParameter("@State", person.State));
                    command.Parameters.Add(new SqlParameter("@Zip", person.Zip));
                    command.Parameters.Add(new SqlParameter("@UniqueId", person.UniqueId));

                    int rowsEffected = command.ExecuteNonQuery();
                }
                dbConnection.Close();
            }
        }

        public static void DeletePerson(string id)
        {
            using (SqlConnection dbConnection = new SqlConnection(GetConnectionString()))
            {
                string sql = @"DELETE FROM Person
                               WHERE UniqueId = @UniqueId";

                dbConnection.Open();

                using (SqlCommand command = new SqlCommand(sql, dbConnection))
                {
                    command.Parameters.Add(new SqlParameter("@UniqueId", id));

                    int rowsEffected = command.ExecuteNonQuery();
                }

                dbConnection.Close();
            }
        }
        private static void MapPerson(Person person, SqlDataReader reader)
        {
            
            person.UniqueId = reader.GetGuid(reader.GetOrdinal("UniqueId")).ToString();
            person.FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
            person.LastName = reader.GetString(reader.GetOrdinal("LastName"));
            person.Address = reader.GetString(reader.GetOrdinal("Address"));
            person.City = reader.GetString(reader.GetOrdinal("City"));
            person.State = reader.GetString(reader.GetOrdinal("State"));
            person.Zip = reader.GetString(reader.GetOrdinal("Zip"));
        }
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["PackageConnectionString"].ConnectionString;
        }
    }
}