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

        // GET api/tasks/5
        [HttpGet("{id}")]
        public Task Get(int id)
        {
            ManageTasks mngTasks = new ManageTasks();
            return mngTasks.GetTaskFromId(id);
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
    }
}
