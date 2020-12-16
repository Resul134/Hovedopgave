using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Library.models;
namespace RestAPI.DBUtil
{
    public class ManageQualification
    {
        private static ConnectionString connst = new ConnectionString();
        private string connectionString = connst.ConnectionStr;
        public bool CreateQualification(Qualification item)
        {
            string queryString = "INSERT INTO Qualification (UserID, Skill) VALUES (@UserID, @Skill)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@UserID", item.UserID);
                command.Parameters.AddWithValue("@Skill", item.Skill);
                connection.Open();

                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
        }

        public bool deleteQualification(int id)
        {
            string queryString = "DELETE FROM Qualification WHERE ID = @ID";

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

        public List<Qualification> getAllQualifications()
        {
            List<Qualification> QualificationList = new List<Qualification>();
            string queryString = "SELECT * FROM Qualification";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Qualification qualification = new Qualification();
                        qualification.ID = reader.GetInt32(0);
                        qualification.UserID = reader.GetInt32(1);
                        qualification.Skill = reader.GetString(2);
                        QualificationList.Add(qualification);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return QualificationList;
        }

        public List<Qualification> GetQualificationsByUserId(int userId)
        {
            List<Qualification> QualificationList = new List<Qualification>();
            string queryString = "SELECT * FROM Qualification WHERE UserID = @UserId";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.Parameters.AddWithValue("@UserId", userId);
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Qualification qualification = new Qualification();
                        qualification.ID = reader.GetInt32(0);
                        qualification.UserID = reader.GetInt32(1);
                        qualification.Skill = reader.GetString(2);
                        QualificationList.Add(qualification);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return QualificationList;
        }

        public Qualification GetQualificationFromId(int id)
        {
            Qualification qualification = new Qualification();

            string queryString = "SELECT * FROM Qualification WHERE ID = @ID";
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
                        qualification.ID = reader.GetInt32(0);
                        qualification.UserID = reader.GetInt32(1);
                        qualification.Skill = reader.GetString(2);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }

            return qualification;
        }

        public bool UpdateQualification(Qualification item, int id)
        {
            string queryString = "UPDATE Qualification SET UserID = @UserID, Skill = @Skill WHERE ID = @ID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@ID", id);
                command.Parameters.AddWithValue("@UserID", item.UserID);
                command.Parameters.AddWithValue("@Skill", item.Skill);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
        }
    }
}