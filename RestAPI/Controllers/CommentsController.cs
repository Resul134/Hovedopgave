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
    public class CommentsController : ControllerBase
    {
        // GET: api/<CommentsController>
        [HttpGet]
        public IEnumerable<Comment> Get()
        {
            ManageComment comment = new ManageComment();
            return comment.getAllComments();
        }

        // GET api/<CommentsController>/5
        [HttpGet("{id}")]
        public Comment Get(int id)
        {
            ManageComment comment = new ManageComment();
            return comment.GetCommentFromId(id);
        }

        // POST api/<CommentsController>
        [HttpPost]
        public void Post([FromBody] Comment value)
        {
            ManageComment comment = new ManageComment();
            comment.CreateComment(value);
        }

        // PUT api/<CommentsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Comment value)
        {
            ManageComment comment = new ManageComment();
            comment.UpdateComment(value, id);
        }

        // DELETE api/<CommentsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            ManageComment comment = new ManageComment();
            comment.deleteComment(id);
        }
    }
}
