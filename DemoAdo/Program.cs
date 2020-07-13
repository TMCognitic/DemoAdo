using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DemoAdo
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DemoAdo;Integrated Security=True;"; // Windows Authentication
            //string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DemoAdo;User Id=DemoAdo;Password=DemoAdo;"; // Sql Server Authentication

            using (SqlConnection sqlConnection = new SqlConnection())
            {
                sqlConnection.ConnectionString = connectionString;

                using(SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "Select Count(*) from AppUser.V_Users;";
                    //sqlCommand.CommandText = "Select LastName from AppUser.V_Users Where Id = 1;";

                    sqlConnection.Open();
                    int count = (int)sqlCommand.ExecuteScalar();
                    Console.WriteLine(count);
                }
            }

            Console.ReadLine();
        }
    }
}
