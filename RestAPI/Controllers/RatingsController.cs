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
    public class RatingsController : ControllerBase
    {
        // GET: api/<RatingsController>
        [HttpGet]
        public IEnumerable<Rating> Get()
        {
            ManageRating rating = new ManageRating();
            return rating.getAllRatings();
        }

        // GET api/<RatingsController>/5
        [HttpGet("{id}")]
        public Rating Get(int id)
        {
            ManageRating rating = new ManageRating();
            return rating.GetRatingFromId(id);
        }

        //GET api/<RatingsController>/user/5
        [HttpGet]
        [Route("user/{userId}")]
        public List<Rating> GetByUserId(int userId)
        {
            ManageRating mngRating = new ManageRating();
            return mngRating.GetRatingFromUser(userId);
        }

        //Get api/<RatingsController>/user/5/task/10
        [HttpGet]
        [Route("user/{userId}/task/{taskId}")]
        public Rating GetByUserIdAndTaskId(int userId, int taskId)
        {
            ManageRating mngRating = new ManageRating();
            return mngRating.GetRatingFromUserAndTask(userId, taskId);
        }

        // POST api/<RatingsController>
        [HttpPost]
        public void Post([FromBody] Rating value)
        {
            ManageRating rating = new ManageRating();
            rating.CreateRating(value);
        }

        // PUT api/<RatingsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Rating value)
        {
            ManageRating rating = new ManageRating();
            rating.UpdateRating(value, id);
        }

        // DELETE api/<RatingsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            ManageRating rating = new ManageRating();
            rating.deleteRating(id);
        }
    }
}
