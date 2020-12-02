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
    public class CategoriesController : ControllerBase
    {
        // GET: api/<CategoriesController>
        [HttpGet]
        public IEnumerable<Category> Get()
        {
            ManageCategory mngCategory = new ManageCategory();
            return mngCategory.getAllCategories();
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public Category Get(int id)
        {
            ManageCategory mngCategory = new ManageCategory();
            return mngCategory.GetCategoryFromId(id);
        }

        // GET api/<CategoriesController>/udendørs
        [HttpGet("name/{name}")]
        public Category Get(string name)
        {
            ManageCategory mngCategory = new ManageCategory();
            return mngCategory.GetCategoryFromName(name);
        }

        // POST api/<CategoriesController>
        [HttpPost]
        public void Post([FromBody] Category value)
        {
            ManageCategory mngCategory = new ManageCategory();
            mngCategory.CreateCategory(value);
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Category value)
        {
            ManageCategory mngCategory = new ManageCategory();
            mngCategory.UpdateCategory(value, id);
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            ManageCategory mngCategory = new ManageCategory();
            mngCategory.deleteCategory(id);
        }
    }
}
