using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Library.models;
namespace RestAPI.DBUtil
{
    public class ManageComment
    {
        private static ConnectionString connst = new ConnectionString();
        private string connectionString = connst.ConnectionStr;
        public bool CreateComment(Comment item)
        {
            string queryString = "INSERT INTO Comment (TaskID, UserID, Date, Message) VALUES (@TaskID, @UserID, @Date, @Message)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@TaskID", item.TaskID);
                command.Parameters.AddWithValue("@UserID", item.UserID);
                command.Parameters.AddWithValue("@Date", item.Date);
                command.Parameters.AddWithValue("@Message", item.Message);
                connection.Open();

                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
        }

        public bool deleteComment(int id)
        {
            string queryString = "DELETE FROM Comment WHERE ID = @ID";

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

        public List<Comment> getAllComments()
        {
            List<Comment> CommentList = new List<Comment>();
            string queryString = "SELECT * FROM Comment";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Comment comment = new Comment();
                        comment.ID = reader.GetInt32(0);
                        comment.TaskID = reader.GetInt32(1);
                        comment.UserID = reader.GetInt32(2);
                        comment.Date = reader.GetDateTime(3);
                        comment.Message = reader.GetString(4);
                        CommentList.Add(comment);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return CommentList;
        }

        public Comment GetCommentFromId(int id)
        {
            Comment comment = new Comment();

            string queryString = "SELECT * FROM Comment WHERE ID = @ID";
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
                        comment.ID = reader.GetInt32(0);
                        comment.TaskID = reader.GetInt32(1);
                        comment.UserID = reader.GetInt32(2);
                        comment.Date = reader.GetDateTime(3);
                        comment.Message = reader.GetString(4);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }

            return comment;
        }

        public bool UpdateComment(Comment item, int id)
        {
            string queryString = "UPDATE Comment SET TaskID = @TaskID, UserID = @UserID, Date = @Date, Message = @Message WHERE ID = @ID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@ID", id);
                command.Parameters.AddWithValue("@TaskID", item.TaskID);
                command.Parameters.AddWithValue("@UserID", item.UserID);
                command.Parameters.AddWithValue("@Date", item.Date);
                command.Parameters.AddWithValue("@Message", item.Message);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
        }
    }
}
