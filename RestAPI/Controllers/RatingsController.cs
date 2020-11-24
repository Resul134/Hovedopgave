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
