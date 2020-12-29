using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RestAPI.DBUtil;
using Library.models;

namespace RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        // GET: api/tasks
        [HttpGet]
        public IEnumerable<Task> Get()
        {
            ManageTasks mngTasks = new ManageTasks();
            return mngTasks.GetAllTasks();
        }

        [HttpGet]
        [Route("MyTasks/{userID}")]
        public List<Task> GetMyTasks(int userID)
        {
            ManageTasks mngTasks = new ManageTasks();
            return mngTasks.GetMyTasksFromUserID(userID);
        }

        // GET api/tasks/title/{titel name}
        [HttpGet]
        [Route("title/{title}")]
        public List<Task> GetByTitel(string title)
        {
            ManageTasks mngTasks = new ManageTasks();
            return mngTasks.GetTasksFromTitle(title);
        }

        // GET api/tasks/category/{categoryId}
        [HttpGet]
        [Route("category/{categoryId}")]
        public List<Task> GetByCategory(int categoryId)
        {
            ManageTasks mngTasks = new ManageTasks();
            return mngTasks.GetTasksFromCategory(categoryId);
        }

        // GET api/tasks/5
        [HttpGet("{id}")]
        public Task Get(int id)
        {
            ManageTasks mngTasks = new ManageTasks();
            return mngTasks.GetTaskFromId(id);
        }

        // GET api/tasks/filter?xxxx
        [HttpGet]
        [Route("filter")]
        public List<Task> FilterTasks([FromQuery] FilterTask qtask)
        {
            ManageTasks mngTasks = new ManageTasks();
            return mngTasks.GetTasksByFilter(qtask);
        }

        // POST api/tasks
        [HttpPost]
        public void Post([FromBody] Task task)
        {
            ManageTasks mngTasks = new ManageTasks();
            mngTasks.CreateTask(task);
        }

        // PUT api/<ValuesController1>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Task task)
        {
            ManageTasks mngTasks = new ManageTasks();
            mngTasks.UpdateTask(task, id);
        }

        // DELETE api/<ValuesController1>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            ManageTasks mngTasks = new ManageTasks();
            mngTasks.deleteTask(id);
        }
        // DEL api/tasks/deleteTaskByID/2
        [HttpDelete]
        [Route("deleteTaskByID/{userid}")]
        public void DeleteAllTasksByID(int userid)
        {
            ManageTasks mngTasks = new ManageTasks();
            mngTasks.deleteAllTasksByUserID(userid);
        }
    }
}
