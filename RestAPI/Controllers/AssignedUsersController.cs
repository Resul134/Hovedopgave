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

        [HttpGet]
        [Route("AssignedUsersMyTask/{taskID}")]
        public IEnumerable<AssignedUser> GetAssignedUsersByTaskID(int taskID)
        {
            ManageAssignedUsers manageAssignedUser = new ManageAssignedUsers();
            return manageAssignedUser.getAllAssignedUsersMyTask(taskID);
        }

        // GET api/<AssignedUserController>/5
        [HttpGet("{id}")]
        public AssignedUser Get(int id)
        {
            ManageAssignedUsers manageAssignedUser = new ManageAssignedUsers();
            return manageAssignedUser.GetAssignedUsersFromId(id);
        }

        // GET api/<AssignedUserController>/taskid/5/userid/2
        [HttpGet]
        [Route("task/{taskId}/user/{userId}")]
        public AssignedUser Get(int taskId, int userId)
        {
            ManageAssignedUsers mngAssignedUsers = new ManageAssignedUsers();
            return mngAssignedUsers.GetMatch(taskId, userId);
        }

        // POST api/<AssignedUserController>
        [HttpPost]
        public bool Post([FromBody] AssignedUser value)
        {
            ManageAssignedUsers manageAssignedUser = new ManageAssignedUsers();
            return manageAssignedUser.createAssignedUsers(value);
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
        public bool Delete(int id)
        {
            ManageAssignedUsers manageAssignedUser = new ManageAssignedUsers();
            return manageAssignedUser.deleteAssignedUsers(id);
        }

        [HttpGet]
        [Route("GetAcceptedAssignments/{id}")]
        public List<AssignedUser> AcceptedAssignments(int id)
        {
            ManageAssignedUsers mngAssignedUsers = new ManageAssignedUsers();
            return mngAssignedUsers.getAcceptedAssignments(id);
        }

        [HttpGet]
        [Route("GetDeniedAssignments/{id}")]
        public List<AssignedUser> DeniedAssignments(int id)
        {
            ManageAssignedUsers mngAssignedUsers = new ManageAssignedUsers();
            return mngAssignedUsers.getDeniedAssignments(id);
        }

        [HttpDelete("deniedUsers/{taskid}")]
        public bool DeleteDeniedUsers(int taskid)
        {
            ManageAssignedUsers manageAssignedUser = new ManageAssignedUsers();
            return manageAssignedUser.RemoveAssignedUsers_If_Denied(taskid);
        }

        [HttpDelete]
        [Route("task/RemoveAssignedUser/{userid}")]
        public bool DeleteAssignedUserID(int userid)
        {
            ManageAssignedUsers manageAssignedUser = new ManageAssignedUsers();
            return manageAssignedUser.RemoveAssignedUser(userid);
        }
    }
}