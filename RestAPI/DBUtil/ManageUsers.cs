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
        private static ConnectionString connst = new ConnectionString();
        private string connectionString = connst.ConnectionStr;

        public bool CreateUser(User item)
        {
            throw new NotImplementedException();
        }

        public bool deleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public List<User> getAllUsers()
        {
            List<User> UserList = new List<User>();
            string queryString = "SELECT * FROM [User]";
            using (SqlConnection connection = new SqlConnection(connectionString))
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
                        user.Rating = reader.GetDouble(5);
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

        public User GetUserFromId(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateUser(User item, int id)
        {
            throw new NotImplementedException();
        }
    }
}
