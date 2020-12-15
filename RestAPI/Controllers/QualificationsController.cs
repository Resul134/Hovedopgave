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
        public class QualificationsController : ControllerBase
        {
            // GET: api/<QualificationsController>
            [HttpGet]
            public IEnumerable<Qualification> Get()
            {
                ManageQualification qualification = new ManageQualification();
                return qualification.getAllQualifications();
            }

            // GET api/<QualificationsController>/5
            [HttpGet("{id}")]
            public Qualification Get(int id)
            {
                ManageQualification qualification = new ManageQualification();
                return qualification.GetQualificationFromId(id);
            }

            // POST api/<QualificationsController>
            [HttpPost]
            public void Post([FromBody] Qualification value)
            {
            ManageQualification qualification = new ManageQualification();
            qualification.CreateQualification(value);
            }

            // PUT api/<QualificationsController>/5
            [HttpPut("{id}")]
            public void Put(int id, [FromBody] Qualification value)
            {
             ManageQualification qualification = new ManageQualification();
            qualification.UpdateQualification(value, id);
            }

            // DELETE api/<QualificationsController>/5
            [HttpDelete("{id}")]
            public void Delete(int id)
            {
             ManageQualification qualification = new ManageQualification();
            qualification.deleteQualification(id);
            }
        }
    }