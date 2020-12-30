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
            return Ok("Success");

        }

        [HttpPost]
        [ActionName("AddSignup")]
        public IActionResult AddSignup([FromBody] DataLayer.Models.CovidTestGenieJSon model)
        {
            _context.SaveError("Adding Signup", "");
            try
            {
                if (CovidTest.AddGeniueSignup(_context, model) == true)
                    return Ok("Success");
                else
                    return NotFound(); //Ok("Skip");
            }
            catch (Exception ex)
            {
                _context.SaveError("Error in Signup", ex.Message);
                return BadRequest();
            }
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

            if (json == null)
                return NotFound();

            return Ok(json);
        }

        [HttpPost]
        [ActionName("search")]
        public IActionResult Search([FromBody]DataLayer.Models.CovidTestSearch model)
        {
            try
            {
                DataLayer.Models.CovidTestDBModel result = CovidTest.GetCovidRecord(_context, model);
                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch(Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [ActionName("CovidTestEvents")]
        public IActionResult SaveEvent([FromBody] DataLayer.Models.CovidTestEventsJSon json)
        {

            _context.SaveError("Entering Save CovidtestEvents", "");
            try
            {

                DataLayer.Models.CovidTestEvents model = new DataLayer.Models.CovidTestEvents();
                model.enddate = DateTime.Parse(json.enddate);
                model.startdate = DateTime.Parse(json.startdate);
                model.Title = json.Title;
                model.SignupID = json.SignupID;
                model.TotalSlots = json.TotalSlots;
                model.FilledSlots = json.FilledSlots;

                CovidTest.AddEventRecord(_context, model);
            }
            catch (Exception ex)
            {
                _context.SaveError("In Update", ex.Message);
                return BadRequest(ex.Message);
            }
            return Ok("Success");

        }

        [HttpGet("{CovidTestEvents}")]
        [ActionName("CovidTestEvents")]
        public IActionResult GetEvent(int CovidTestEvents)
        {
            DataLayer.Models.CovidTestEvents result = CovidTest.GetCovidTestEvents(_context, CovidTestEvents);
            if (result == null)
                return NoContent();
            else
                return Ok(result);
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
