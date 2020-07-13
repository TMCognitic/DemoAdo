using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Tools.Database;

namespace DemoAdoCore
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DemoAdo;Integrated Security=True;"; // Windows Authentication
            string specialUserconnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DemoAdo;User Id=DemoAdo;Password=DemoAdo;"; // Sql Server Authentication


            ////EXecuteScalar
            //using (SqlConnection sqlConnection = new SqlConnection())
            //{
            //    sqlConnection.ConnectionString = connectionString;

            //    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
            //    {
            //        sqlCommand.CommandText = "Select FirstName from AppUser.V_Users Where Id = 2;";

            //        sqlConnection.Open();
            //        string firstName = (string)sqlCommand.ExecuteScalar();
            //        Console.WriteLine(firstName);
            //    }
            //}

            Console.WriteLine("ExecuteScalar");
            Connection connection = new Connection(connectionString, SqlClientFactory.Instance);
            Command command1 = new Command("Select FirstName from AppUser.V_Users Where Id = @Id;");
            command1.AddParameter("Id", 2);

            Console.WriteLine(connection.ExecuteScalar(command1));
            Console.WriteLine();

            ////ExecuteReader - Connecté
            //using (SqlConnection sqlConnection = new SqlConnection())
            //{
            //    sqlConnection.ConnectionString = connectionString;

            //    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
            //    {
            //        sqlCommand.CommandText = "Select Id, FirstName, LastName as Nom from AppUser.V_Users;";

            //        sqlConnection.Open();
            //        using (SqlDataReader reader = sqlCommand.ExecuteReader())
            //        {
            //            while (reader.Read())
            //            {
            //                string nom = (string)reader["FirstName"];
            //                string prenom = (string)reader["Nom"];

            //                Console.WriteLine($"{nom} {prenom}");
            //            }
            //        }                    
            //    }
            //}

            Console.WriteLine("ExecuteReader");
            Command command2 = new Command("Select Id, FirstName, LastName as Nom from AppUser.V_Users;");

            IEnumerable<User> users = connection.ExecuteReader(command2, (dr) => new User() { Id = (int)dr["Id"], Nom = (string)dr["Nom"], Prenom = (string)dr["FirstName"] });
            foreach(User user in users)
            {
                Console.WriteLine($"{user.Nom} {user.Prenom}");
            }
            Console.WriteLine();

            ////SqlDataAdapter.Fill() - Déconnecté
            //using (SqlConnection sqlConnection = new SqlConnection())
            //{
            //    sqlConnection.ConnectionString = connectionString;

            //    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
            //    {
            //        sqlCommand.CommandText = "Select Id, FirstName, LastName as Nom from AppUser.V_Users;";

            //        using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
            //        {
            //            sqlDataAdapter.SelectCommand = sqlCommand;
            //            DataTable dataTable = new DataTable();

            //            sqlDataAdapter.Fill(dataTable); // Open, Execute, Fetch, Close

            //            foreach (DataRow dataRow in dataTable.Rows)
            //            {
            //                string nom = (string)dataRow["FirstName"];
            //                string prenom = (string)dataRow["Nom"];

            //                Console.WriteLine($"{nom} {prenom}");
            //            }
            //        }
            //    }
            //}

            Console.WriteLine("GetDataTable");
            Command command3 = new Command("Select Id, FirstName, LastName as Nom from AppUser.V_Users;");
            DataTable dataTable = connection.GetDataTable(command3);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                string nom = (string)dataRow["FirstName"];
                string prenom = (string)dataRow["Nom"];

                Console.WriteLine($"{nom} {prenom}");
            }
            Console.WriteLine();

            Console.WriteLine("GetDataSet");
            DataSet dataSet = connection.GetDataSet(command3);
            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string nom = (string)dataRow["FirstName"];
                string prenom = (string)dataRow["Nom"];

                Console.WriteLine($"{nom} {prenom}");
            }

            ////ExecuteNonQuery + Requete paramétrée
            //using (SqlConnection sqlConnection = new SqlConnection())
            //{
            //    sqlConnection.ConnectionString = connectionString;

            //    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
            //    {
            //        sqlCommand.CommandText = "Insert into Users (LastName, FirstName, Email, Passwd) values (@LastName, @FirstName, @Email, HASHBYTES('SHA2_512', dbo.DASF_GetPreSalt() + @Passwd + dbo.DASF_GestPostSalt())); ";
            //        sqlCommand.Parameters.AddWithValue("LastName", "Herssens");
            //        sqlCommand.Parameters.AddWithValue("FirstName", "Caroline");
            //        sqlCommand.Parameters.AddWithValue("Email", "caroline.herssens@cognitic.be");
            //        sqlCommand.Parameters.AddWithValue("Passwd", "Test1234=");

            //        using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
            //        {
            //            sqlConnection.Open();
            //            Console.WriteLine(sqlCommand.ExecuteNonQuery());
            //        }
            //    }
            //}

            //StoredProcedure
            //using (SqlConnection sqlConnection = new SqlConnection())
            //{
            //    sqlConnection.ConnectionString = specialUserconnectionString;

            //    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
            //    {
            //        sqlCommand.CommandText = "AppUser.DASP_Register";
            //        sqlCommand.CommandType = CommandType.StoredProcedure;

            //        sqlCommand.Parameters.AddWithValue("LastName", "Mc Claine");
            //        sqlCommand.Parameters.AddWithValue("FirstName", "John");
            //        sqlCommand.Parameters.AddWithValue("Email", "john.mcclaine@nakatomiplaza.com");
            //        sqlCommand.Parameters.AddWithValue("Passwd", "YiPiKay");

            //        using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
            //        {
            //            sqlConnection.Open();
            //            Console.WriteLine(sqlCommand.ExecuteScalar());
            //        }
            //    }
            //}

            Console.WriteLine("Stored Procedure");
            Connection connection2 = new Connection(specialUserconnectionString, SqlClientFactory.Instance);
            Command command4 = new Command("AppUser.DASP_Register", true);
            command4.AddParameter("LastName", "Doe");
            command4.AddParameter("FirstName", "John");
            command4.AddParameter("Email", "john.doe@unknow.com");
            command4.AddParameter("Passwd", "Jane");

            int Id = (int)connection2.ExecuteScalar(command4);
            Console.WriteLine(Id);
            Console.WriteLine();
        }
    }
}
