using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebUI.Example.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebUI.Example.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> GetValues()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string GetValue(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public ValuesResponseViewModel PostValue([FromBody] ValuesRequestViewModel value)
        {
            return new ValuesResponseViewModel { Values = new List<string> { "value1", "value2" } };
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void PutValue(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void DeleteValue(int id)
        {
        }
    }
}
