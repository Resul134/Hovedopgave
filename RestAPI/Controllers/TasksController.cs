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

        // GET api/<ValuesController1>/5
        [HttpGet("{id}")]
        public Task Get(int id)
        {
            ManageTasks mngTasks = new ManageTasks();
            return mngTasks.GetTaskFromId(id);
        }

        // POST api/<ValuesController1>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController1>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController1>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
