using System.Collections.Generic;
using System.Data.SqlClient;
using Library.models;

namespace RestAPI.DBUtil
{
    public class ManageTasks : IManageTasks
    {
        private static ConnectionString connst = new ConnectionString();
        private string connectionString = connst.ConnectionStr;


        public List<Task> GetAllTasks()
        {
            List<Task> taskList = new List<Task>();
            string queryString = "SELECT * FROM Task";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Task task = new Task();
                        task.Id = reader.GetInt32(0);
                        task.UserId = reader.GetInt32(1);
                        task.CategoryId = reader.GetInt32(2);
                        task.Date = reader.GetDateTime(3);
                        task.Title = reader.GetString(4);
                        task.Price = reader.GetDouble(5);
                        task.Description = reader.GetString(6);
                        task.Status = reader.GetString(7);
                        task.Promoted = reader.GetBoolean(8);
                        task.Region = reader.GetString(9);
                        task.PromotedEnd = reader.GetDateTime(10);
                        task.PageViews = reader.GetInt32(11);
                        taskList.Add(task);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return taskList;
        }


        public List<Task> GetTasksFromTitle(string title)
        {
            List<Task> taskList = new List<Task>();
            string queryString = "SELECT * FROM Task WHERE Title LIKE '%' + @title + '%'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@title", title);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Task task = new Task();
                        task.Id = reader.GetInt32(0);
                        task.UserId = reader.GetInt32(1);
                        task.CategoryId = reader.GetInt32(2);
                        task.Date = reader.GetDateTime(3);
                        task.Title = reader.GetString(4);
                        task.Price = reader.GetDouble(5);
                        task.Description = reader.GetString(6);
                        task.Status = reader.GetString(7);
                        task.Promoted = reader.GetBoolean(8);
                        task.Region = reader.GetString(9);
                        task.PromotedEnd = reader.GetDateTime(10);
                        task.PageViews = reader.GetInt32(11);
                        taskList.Add(task);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }

            return taskList;

        }

        public List<Task> GetTasksFromCategory(int categoryId)
        {
            List<Task> taskList = new List<Task>();
            string queryString = "SELECT * FROM Task WHERE CategoryID=@CATID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@CATID", categoryId);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Task task = new Task();
                        task.Id = reader.GetInt32(0);
                        task.UserId = reader.GetInt32(1);
                        task.CategoryId = reader.GetInt32(2);
                        task.Date = reader.GetDateTime(3);
                        task.Title = reader.GetString(4);
                        task.Price = reader.GetDouble(5);
                        task.Description = reader.GetString(6);
                        task.Status = reader.GetString(7);
                        task.Promoted = reader.GetBoolean(8);
                        task.Region = reader.GetString(9);
                        task.PromotedEnd = reader.GetDateTime(10);
                        task.PageViews = reader.GetInt32(11);
                        taskList.Add(task);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return taskList;

        }

        public List<Task> GetMyTasksFromUserID(int userID)
        {
            List<Task> taskList = new List<Task>();
            string queryString = "SELECT * FROM Task WHERE UserID=@UserID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@UserID", userID);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Task task = new Task();
                        task.Id = reader.GetInt32(0);
                        task.UserId = reader.GetInt32(1);
                        task.CategoryId = reader.GetInt32(2);
                        task.Date = reader.GetDateTime(3);
                        task.Title = reader.GetString(4);
                        task.Price = reader.GetDouble(5);
                        task.Description = reader.GetString(6);
                        task.Status = reader.GetString(7);
                        task.Promoted = reader.GetBoolean(8);
                        task.Region = reader.GetString(9);
                        task.PromotedEnd = reader.GetDateTime(10);
                        task.PageViews = reader.GetInt32(11);
                        taskList.Add(task);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return taskList;
        }

        public List<Task> GetTasksByFilter(FilterTask qtask)
        {
            bool queryCategory = false;
            bool queryRegion = false;
            bool queryPrice = false;
            bool querySearch = false;
            if (qtask.DateStart == new System.DateTime())
            {
                qtask.DateStart = new System.DateTime(1793, 1, 1);
            }
            if (qtask.DateEnd == new System.DateTime())
            {
                qtask.DateEnd = new System.DateTime(3000, 1, 1);
            }
            List<Task> taskList = new List<Task>();
            string queryString = "SELECT * From Task WHERE ";
            if (qtask.CategoryId != 0)
            {
                queryString += "CategoryID=@CATID AND ";
                queryCategory = true;
            }
            if (qtask.Region != null)
            {
                queryString += "Region=@REGION AND ";
                queryRegion = true;
            }
            if (qtask.PriceHigh != 0 && qtask.PriceHigh > qtask.PriceLow)
            {
                queryString += "Price BETWEEN @PRICELOW AND @PRICEHIGH AND ";
                queryPrice = true;
            }
            if (qtask.Search != null)
            {
                queryString += "Title LIKE '%' + @TITLE + '%' AND ";
                querySearch = true;
            }
            queryString += "Date BETWEEN @DATESTART AND @DATEEND";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                if (queryCategory)
                {
                    command.Parameters.AddWithValue("@CATID", qtask.CategoryId);
                }
                if (queryRegion)
                {
                    command.Parameters.AddWithValue("@REGION", qtask.Region);
                }
                if (queryPrice)
                {
                    command.Parameters.AddWithValue("@PRICELOW", qtask.PriceLow);
                    command.Parameters.AddWithValue("@PRICEHIGH", qtask.PriceHigh);
                }
                if (querySearch)
                {
                    command.Parameters.AddWithValue("@TITLE", qtask.Search);
                }
                command.Parameters.AddWithValue("@DATESTART", qtask.DateStart);
                command.Parameters.AddWithValue("@DATEEND", qtask.DateEnd);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Task task = new Task();
                        task.Id = reader.GetInt32(0);
                        task.UserId = reader.GetInt32(1);
                        task.CategoryId = reader.GetInt32(2);
                        task.Date = reader.GetDateTime(3);
                        task.Title = reader.GetString(4);
                        task.Price = reader.GetDouble(5);
                        task.Description = reader.GetString(6);
                        task.Status = reader.GetString(7);
                        task.Promoted = reader.GetBoolean(8);
                        task.Region = reader.GetString(9);
                        task.PromotedEnd = reader.GetDateTime(10);
                        task.PageViews = reader.GetInt32(11);
                        taskList.Add(task);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return taskList;

        }

        public Task GetTaskFromId(int id)
        {
            Task task = new Task();
            string queryString = "SELECT * FROM Task WHERE ID=@id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@id", id);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        task.Id = reader.GetInt32(0);
                        task.UserId = reader.GetInt32(1);
                        task.CategoryId = reader.GetInt32(2);
                        task.Date = reader.GetDateTime(3);
                        task.Title = reader.GetString(4);
                        task.Price = reader.GetDouble(5);
                        task.Description = reader.GetString(6);
                        task.Status = reader.GetString(7);
                        task.Promoted = reader.GetBoolean(8);
                        task.Region = reader.GetString(9);
                        task.PromotedEnd = reader.GetDateTime(10);
                        task.PageViews = reader.GetInt32(11);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }

            return task;
        }

        public bool CreateTask(Task item)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(
                        "INSERT INTO Task (UserID, CategoryID, Date, Title, Price, Description, Status, Promoted, Region, PromotedEnd, PageViews) VALUES (@userid, @catid, @date, @title, @price, @desc, @status, @promo, @region, @promoend, @views)", connection);
                    command.Parameters.AddWithValue("@userid", item.UserId);
                    command.Parameters.AddWithValue("@catid", item.CategoryId);
                    command.Parameters.AddWithValue("@date", item.Date);
                    command.Parameters.AddWithValue("@title", item.Title);
                    command.Parameters.AddWithValue("@price", item.Price);
                    command.Parameters.AddWithValue("@desc", item.Description);
                    command.Parameters.AddWithValue("@status", item.Status);
                    command.Parameters.AddWithValue("@promo", item.Promoted);
                    command.Parameters.AddWithValue("@region", item.Region);
                    command.Parameters.AddWithValue("@promoend", item.PromotedEnd);
                    command.Parameters.AddWithValue("@views", item.PageViews);

                    command.Connection.Open();
                    command.ExecuteNonQuery();
                    command.Connection.Close();

                    return true;
                }

                catch (SqlException)
                {
                    return false;
                }
            }
        }

        public bool UpdateTask(Task item, int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(
                        "Update Task SET UserID=@userid, CategoryID=@catid, Date=@date, Title=@title, Price=@price, Description=@desc, Status=@status, Promoted=@promo, Region=@region, PromotedEnd=@promoend, PageViews=@views WHERE ID=@id", connection);
                    command.Parameters.AddWithValue("@userid", item.UserId);
                    command.Parameters.AddWithValue("@catid", item.CategoryId);
                    command.Parameters.AddWithValue("@date", item.Date);
                    command.Parameters.AddWithValue("@title", item.Title);
                    command.Parameters.AddWithValue("@price", item.Price);
                    command.Parameters.AddWithValue("@desc", item.Description);
                    command.Parameters.AddWithValue("@status", item.Status);
                    command.Parameters.AddWithValue("@promo", item.Promoted);
                    command.Parameters.AddWithValue("@region", item.Region);
                    command.Parameters.AddWithValue("@promoend", item.PromotedEnd);
                    command.Parameters.AddWithValue("@views", item.PageViews);
                    command.Parameters.AddWithValue("@id", id);

                    command.Connection.Open();
                    command.ExecuteNonQuery();
                    command.Connection.Close();

                    return true;
                }

                catch (SqlException)
                {
                    return false;
                }
            }
        }

        public bool deleteAllTasksByUserID(int userID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                try
                {
                    SqlCommand command = new SqlCommand("DELETE FROM Task WHERE UserID=@userID", connection);
                    command.Parameters.AddWithValue("@userID", userID);
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

        public bool deleteTask(int id)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                try
                {
                    SqlCommand command = new SqlCommand("DELETE FROM Task WHERE Id=@id", connection);
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
    }
}