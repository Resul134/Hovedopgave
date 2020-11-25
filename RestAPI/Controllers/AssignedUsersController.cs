using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Library.models;
using RestAPI.DBUtil;

namespace RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignedUsersController : ControllerBase
    {
        // GET: api/<AssignedUserController>
        [HttpGet]
        public IEnumerable<AssignedUser> Get()
        {
            ManageAssignedUsers manageAssignedUser = new ManageAssignedUsers();
            return manageAssignedUser.getAllAssignedUsers();
        }

        // GET api/<AssignedUserController>/5
        [HttpGet("{id}")]
        public AssignedUser Get(int id)
        {
            ManageAssignedUsers manageAssignedUser = new ManageAssignedUsers();
            return manageAssignedUser.GetAssignedUsersFromId(id);
        }

        // POST api/<AssignedUserController>
        [HttpPost]
        public void Post([FromBody] AssignedUser value)
        {
            ManageAssignedUsers manageAssignedUser = new ManageAssignedUsers();
            manageAssignedUser.createAssignedUsers(value);
        }

        // PUT api/<AssignedUserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] AssignedUser value)
        {
            ManageAssignedUsers manageAssignedUser = new ManageAssignedUsers();
            manageAssignedUser.UpdateAssignedUsers(value, id);
        }

        // DELETE api/<AssignedUserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            ManageAssignedUsers manageAssignedUser = new ManageAssignedUsers();
            manageAssignedUser.deleteAssignedUsers(id);
        }
    }
}