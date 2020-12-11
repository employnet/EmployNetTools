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
    [Route("api/[controller]/[Action]")]
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

            var test = new CovidTest();
            return Ok(test);
        }

        // GET api/<CovidTests>/5
       [HttpGet("{id}")]
       [ActionName("ID")]
        public IActionResult Get(int id)
        {
            DataLayer.Models.CovidTestJson model = CovidTest.GetCovidRecord(_context, id);
            return Ok(model);
        }

        //[HttpPost("{id}")]
        //[ActionName("ID")]
        //public IActionResult Post(int id)
        //{
        //    DataLayer.Models.CovidTestDBModel model = CovidTest.GetCovidRecord(_context, id);
        //    return Ok(model);
        //}

        // POST api/<CovidTests>
        [HttpPost]
        [ActionName("Update")]
        public IActionResult Update([FromBody] DataLayer.Models.CovidTestJson model)
        {

            _context.SaveError("Entering Update", "");
            try
            { 
                CovidTest.AddCovidRecord(_context, model);
            }
            catch(Exception ex)
            {
                _context.SaveError("In Update", ex.Message);
                return BadRequest(ex.Message);
            }
            return Ok();

        }

        [HttpGet("details")]
        [ActionName("Search")]
        public IActionResult Find(string id, string patient_first_name, string patient_last_name, string patient_email)
        {

            DataLayer.Models.CovidTestJson json;
            DataLayer.Models.CovidTestSearch search = new DataLayer.Models.CovidTestSearch();
            search.CovidTestId = id;
            search.patient_email = patient_email;
            search.patient_first_name = patient_first_name;
            search.patient_last_name = patient_last_name;

            json = DataLayer.CovidTest.GetCovidJson(_context, search);

            return Ok(json);
        }

        [HttpPost]
        [ActionName("search")]
        public IActionResult Search([FromBody]DataLayer.Models.CovidTestSearch model)
        {
            try
            {
                DataLayer.Models.CovidTestDBModel result = CovidTest.GetCovidRecord(_context, model);
                return Ok(result);
            }
            catch(Exception ex)
            {

                return BadRequest(ex.Message);
            }
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
