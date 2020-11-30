using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestAPI.DBUtil;

namespace RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // GET: api/Users
        [HttpGet]
        public IEnumerable<User> Get()
        {
            ManageUsers mngUser =  new ManageUsers();
            return mngUser.getAllUsers();
        }

        // Get user for log in
        [HttpGet]
        [Route("login/{username},{password}")]
        public User Get(string username, string password)
        {
            ManageUsers mngUser = new ManageUsers();
            return mngUser.Login(username, password);
        }


        // GET: api/Users/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            ManageUsers mngUser = new ManageUsers();
            return mngUser.GetUserFromId(id);
        }

        // POST: api/Users
        [HttpPost]
        public void Post([FromBody] User value)
        {
            ManageUsers mngUser = new ManageUsers();
            mngUser.CreateUser(value);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] User value)
        {
            ManageUsers mngUser = new ManageUsers();
            mngUser.UpdateUser(value, id);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            ManageUsers mngUser = new ManageUsers();
            mngUser.deleteUser(id);
        }
    }
}
