using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDBTweetsApp.Services;
using MongoDB.Bson;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MongoDBTweetsApp.Controllers
{
    [Route("api/[controller]")]
    public class TweetsController : Controller
    {
        // GET: api/tweets
        [HttpGet]
        public IActionResult GetCount()
        {
            Mongo mongo = new Mongo();
            long count = mongo.getNumberOfUsers();

            if(count != 0)
            {
                return Ok(count);
            } else
            {
                return NotFound();
            }
        }

        // GET api/tweets/grumpy
        [HttpGet("grumpy")]
        public IList<String> GetGrumpy()
        {
            Mongo mongo = new Mongo();
            IList <String> grumpyUsers = mongo.mostGrumpyUsers();
            return grumpyUsers;
        }

        // GET api/tweets/grumpy
        [HttpGet("happy")]
        public IList<String> GetHappy()
        {
            Mongo mongo = new Mongo();
            IList<String> happyUsers = mongo.mostHappyUsers();
            return happyUsers;
        }

        // GET api/tweets/grumpy
        [HttpGet("active")]
        public IList<String> GetMostActive()
        {
            Mongo mongo = new Mongo();
            IList<String> mostActive = mongo.mostActiveUsers();
            return mostActive;
        }

        // PUT api/tweets/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/tweets/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
