using Library.models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.DBUtil
{
    public class ManageUsers : IManageUsers
    {
        private const string ConnectionString =
            "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog = master; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


        public List<User> getAllUsers()
        {
            List<User> UserList = new List<User>();
            string queryString = "SELECT * FROM User";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        User user = new User();
                        user.Id = reader.GetInt32(0);
                        user.Username = reader.GetString(1);
                        user.Password = reader.GetString(2);
                        user.Age = reader.GetInt32(3);
                        user.Date = reader.GetDateTime(4);
                        user.Rating = reader.GetFloat(5);
                        user.Suspended = reader.GetBoolean(6);
                        UserList.Add(user);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return UserList;
        }
    }
}
