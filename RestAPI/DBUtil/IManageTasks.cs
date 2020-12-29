using System.Collections.Generic;
using Library.models;

namespace RestAPI.DBUtil
{
    public interface IManageTasks
    {
        List<Task> GetAllTasks();

        List<Task> GetMyTasksFromUserID(int userID);

        Task GetTaskFromId(int id);

        Task GetTaskByIdAndStatus(int id, string status);

        List<Task> GetTasksFromTitle(string title);

        bool deleteAllTasksByUserID(int userID);


        bool CreateTask(Task item);


        bool UpdateTask(Task item, int id);


        bool deleteTask(int id);
    }
}