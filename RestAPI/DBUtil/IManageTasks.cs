using System.Collections.Generic;
using Library.models;

namespace RestAPI.DBUtil
{
    public interface IManageTasks
    {
        List<Task> GetAllTasks();


        Task GetTaskFromId(int id);


        bool CreateTask(Task item);


        bool UpdateTask(Task item, int id);


        bool deleteTask(int id);
    }
}