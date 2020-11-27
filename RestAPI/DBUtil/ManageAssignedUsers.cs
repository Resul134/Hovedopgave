﻿using Library.models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;


namespace RestAPI.DBUtil
{
    public class ManageAssignedUsers
    {
        private static ConnectionString connst = new ConnectionString();
        private string connectionString = connst.ConnectionStr;

        public bool createAssignedUsers(AssignedUser item)
        {
            string queryString = "INSERT INTO AssignedUser (TaskID, UserID, Accepted) VALUES (@TaskID, @UserID, @Accepted)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@TaskID", item.TaskID);
                command.Parameters.AddWithValue("@UserID", item.UserID);
                command.Parameters.AddWithValue("@Accepted", item.Accepted);
                connection.Open();

                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
        }

        public bool deleteAssignedUsers(int id)
        {
            string queryString = "DELETE FROM AssignedUser WHERE ID = @ID";

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

        public List<AssignedUser> getAllAssignedUsers()
        {
            List<AssignedUser> assignedUsersList = new List<AssignedUser>();
            string queryString = "SELECT * FROM AssignedUser";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        AssignedUser assignedUsers = new AssignedUser();
                        assignedUsers.ID = reader.GetInt32(0);
                        assignedUsers.TaskID = reader.GetInt32(1);
                        assignedUsers.UserID = reader.GetInt32(2);
                        assignedUsers.Accepted = reader.GetBoolean(3);
                        assignedUsersList.Add(assignedUsers);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return assignedUsersList;
        }

        public AssignedUser GetAssignedUsersFromId(int id)
        {
            AssignedUser AssignedUsers = new AssignedUser();

            string queryString = "SELECT * FROM AssignedUser WHERE ID = @ID";
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
                        AssignedUsers.ID = reader.GetInt32(0);
                        AssignedUsers.TaskID = reader.GetInt32(1);
                        AssignedUsers.UserID = reader.GetInt32(2);
                        AssignedUsers.Accepted = reader.GetBoolean(3);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }

            return AssignedUsers;
        }

        public bool UpdateAssignedUsers(AssignedUser item, int id)
        {
            string queryString = "UPDATE AssignedUser SET TaskID = @TaskID, UserID = @UserID, Accepted = @Accepted WHERE ID = @ID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@ID", id);
                command.Parameters.AddWithValue("@TaskID", item.TaskID);
                command.Parameters.AddWithValue("@UserID", item.UserID);
                command.Parameters.AddWithValue("@Accepted", item.Accepted);

                connection.Open();

                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
        }
    }
}