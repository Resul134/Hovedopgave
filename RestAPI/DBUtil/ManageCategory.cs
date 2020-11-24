using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Library.models;

namespace RestAPI.DBUtil
{
    public class ManageCategory
    {
        private static ConnectionString connst = new ConnectionString();
        private string connectionString = connst.ConnectionStr;

        public bool CreateCategory(Category item)
        {
            string queryString = "INSERT INTO Category (Name) VALUES (@Name)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@Name", item.Name);
                connection.Open();

                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
        }

        public bool deleteCategory(int id)
        {
            string queryString = "DELETE FROM Category WHERE ID = @ID";

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

        public List<Category> getAllCategories()
        {
            List<Category> CategoryList = new List<Category>();
            string queryString = "SELECT * FROM Category";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Category category = new Category();
                        category.ID = reader.GetInt32(0);
                        category.Name = reader.GetString(1);
                        CategoryList.Add(category);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return CategoryList;
        }

        public Category GetCategoryFromId(int id)
        {
            Category category = new Category();

            string queryString = "SELECT * FROM Category WHERE ID = @ID";
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
                        category.ID = reader.GetInt32(0);
                        category.Name = reader.GetString(1);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }

            return category;
        }

        public bool UpdateCategory(Category item, int id)
        {
            string queryString = "UPDATE Category SET Name = @Name WHERE ID = @ID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@ID", id);
                command.Parameters.AddWithValue("@Name", item.Name);

                connection.Open();

                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
        }
    }
}
