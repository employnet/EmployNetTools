using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmployNetTools.DataLayer;
using System.Globalization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployNetTools.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CovidTests : ControllerBase
    {
        private readonly DataSurfContext _context;

        public CovidTests(DataSurfContext context)
        {
            _context = context;
        }

        // GET: api/<CovidTests>
        [HttpGet(Name = nameof(GetTests))]
        public IActionResult GetTests()
        {
            
            var    test = new CovidTest();
            return Ok(test);
        }

        // GET api/<CovidTests>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            DataLayer.Models.CovidTestDBModel model = CovidTest.GetCovidRecord(_context,id);
            return Ok(model);
        }

        // POST api/<CovidTests>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] DataLayer.Models.CovidTestDBModel model)
        {

             

            await CovidTest.AddCovidRecord(_context, model);

            return Ok();

        }

        // PUT api/<CovidTests>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CovidTests>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
