using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Library.models;

namespace RestAPI.DBUtil
{
    public class ManageRating
    {
        private static ConnectionString connst = new ConnectionString();
        private string connectionString = connst.ConnectionStr;
        public bool CreateRating(Rating item)
        {
            string queryString = "INSERT INTO Rating (UserID, TaskID, Date, Rating, Message) VALUES (@UserID, @TaskID, @Date, @Rating, @Message)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@UserID", item.UserID);
                command.Parameters.AddWithValue("@TaskID", item.TaskID);
                command.Parameters.AddWithValue("@Date", item.Date);
                command.Parameters.AddWithValue("@Rating", item.rating);
                command.Parameters.AddWithValue("@Message", item.Message);
                connection.Open();

                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
        }

        public bool deleteRating(int id)
        {
            string queryString = "DELETE FROM Rating WHERE ID = @ID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@ID", id);

                connection.Open();

                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
        }

        public List<Rating> getAllRatings()
        {
            List<Rating> RatingList = new List<Rating>();
            string queryString = "SELECT * FROM Rating";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Rating rating = new Rating();
                        rating.ID = reader.GetInt32(0);
                        rating.UserID = reader.GetInt32(1);
                        rating.TaskID = reader.GetInt32(2);
                        rating.Date = reader.GetDateTime(3);
                        rating.rating = reader.GetInt32(4);
                        rating.Message = reader.GetString(5);
                        RatingList.Add(rating);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return RatingList;
        }

        public Rating GetRatingFromId(int id)
        {
            Rating rating = new Rating();

            string queryString = "SELECT * FROM Rating WHERE ID = @ID";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();

                command.Parameters.AddWithValue("@ID", id);

                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        rating.ID = reader.GetInt32(0);
                        rating.UserID = reader.GetInt32(1);
                        rating.TaskID = reader.GetInt32(2);
                        rating.Date = reader.GetDateTime(3);
                        rating.rating = reader.GetInt32(4);
                        rating.Message = reader.GetString(5);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }

            return rating;
        }

        public List<Rating> GetRatingFromUser(int userId)
        {
            List<Rating> RatingList = new List<Rating>();

            string queryString = "SELECT * FROM Rating WHERE UserID = @ID";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();

                command.Parameters.AddWithValue("@ID", userId);

                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Rating rating = new Rating();
                        rating.ID = reader.GetInt32(0);
                        rating.UserID = reader.GetInt32(1);
                        rating.TaskID = reader.GetInt32(2);
                        rating.Date = reader.GetDateTime(3);
                        rating.rating = reader.GetInt32(4);
                        rating.Message = reader.GetString(5);
                        RatingList.Add(rating);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }

            return RatingList;
        }

        public Rating GetRatingFromUserAndTask(int userId, int taskId)
        {
            Rating rating = new Rating();

            string queryString = "SELECT * FROM Rating WHERE UserID = @ID AND TaskID = @Task";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();

                command.Parameters.AddWithValue("@ID", userId);
                command.Parameters.AddWithValue("@Task", taskId);

                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        rating.ID = reader.GetInt32(0);
                        rating.UserID = reader.GetInt32(1);
                        rating.TaskID = reader.GetInt32(2);
                        rating.Date = reader.GetDateTime(3);
                        rating.rating = reader.GetInt32(4);
                        rating.Message = reader.GetString(5);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }

            return rating;
        }

        public bool UpdateRating(Rating item, int id)
        {
            string queryString = "UPDATE Rating SET UserID = @UserID, TaskID = @TaskID, Date = @Date, Rating = @Rating, Message = @Message WHERE ID = @ID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@ID", id);
                command.Parameters.AddWithValue("@UserID", item.UserID);
                command.Parameters.AddWithValue("@TaskID", item.TaskID);
                command.Parameters.AddWithValue("@Date", item.Date);
                command.Parameters.AddWithValue("@Rating", item.rating);
                command.Parameters.AddWithValue("@Message", item.Message);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
        }
    }
}
