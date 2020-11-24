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

        public bool CreateUser(User user)
        {
            int noRows;
            string queryString = "INSERT INTO User (FirstName,LastName,Birthday,Gender,Phone,Email,Username,Password,Rating,Suspended) VALUES (@FirstName,@LastName,@Birthday,@Gender,@Phone,@Email,@Username,@Password,@Rating,@Suspended)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@FirstName", user.FirstName);
                    command.Parameters.AddWithValue("@LastName", user.LastName);
                    command.Parameters.AddWithValue("@Birthday", user.Birthday);
                    command.Parameters.AddWithValue("@Gender", user.Gender);
                    command.Parameters.AddWithValue("@Phone", user.Phone);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.Parameters.AddWithValue("@Rating", user.Rating);
                    command.Parameters.AddWithValue("@Suspended", user.Suspended);

                    command.Connection.Open();
                    noRows = command.ExecuteNonQuery();
                    command.Connection.Close();
                }
                catch (SqlException)
                {
                    return false;
                }
            }

            return noRows == 1;
        }

        public bool deleteUser(int id)
        {
            string queryString = "DELETE FROM [User] WHERE ID=@id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@id", id);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                    command.Connection.Close();
                }
                catch (SqlException)
                {

                    return false;
                }
            }

            return true;
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
                        user.ID = reader.GetInt32(0);
                        user.FirstName = reader.GetString(1);
                        user.LastName = reader.GetString(2);
                        user.Birthday = reader.GetDateTime(3);
                        user.Gender = reader.GetString(4);
                        user.Phone = reader.GetInt32(5);
                        user.Email = reader.GetString(6);
                        user.Username = reader.GetString(7);
                        user.Password = reader.GetString(8);
                        user.Rating = reader.GetDouble(9);
                        user.Suspended = reader.GetBoolean(10);
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
            User user = new User();
            string queryString = "SELECT * FROM [User] WHERE ID=" + id;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        user.ID = reader.GetInt32(0);
                        user.FirstName = reader.GetString(1);
                        user.LastName = reader.GetString(2);
                        user.Birthday = reader.GetDateTime(3);
                        user.Gender = reader.GetString(4);
                        user.Phone = reader.GetInt32(5);
                        user.Email = reader.GetString(6);
                        user.Username = reader.GetString(7);
                        user.Password = reader.GetString(8);
                        user.Rating = reader.GetDouble(9);
                        user.Suspended = reader.GetBoolean(10);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return user;
        }

        public bool UpdateUser(User user, int id)
        {
            string queryString = "UPDATE [User] SET FirstName=@FirstName, LastName=@LastName, Birthday=@Birthday, Gender=@Gender, Phone=@Phone, Email=@Email, Username=@Username, Password=@Password, Rating=@Rating, Suspended=@Suspended WHERE ID=@Id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@Id", user.ID);
                    command.Parameters.AddWithValue("@FirstName", user.FirstName);
                    command.Parameters.AddWithValue("@LastName", user.LastName);
                    command.Parameters.AddWithValue("@Birthday", user.Birthday);
                    command.Parameters.AddWithValue("@Gender", user.Gender);
                    command.Parameters.AddWithValue("@Phone", user.Phone);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.Parameters.AddWithValue("@Rating", user.Rating);
                    command.Parameters.AddWithValue("@Suspended", user.Suspended);
                    
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                    command.Connection.Close();
                }
                catch (SqlException)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
