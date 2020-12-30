using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using System.Web.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmployNetTools.DataLayer.Models.TempWorks;
//using FromBodyAttribute = System.Web.Http.FromBodyAttribute;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployNetTools.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class TextTools : ControllerBase
    {
        // GET: <TextTools>
        [HttpGet(Name = nameof(GetTools))]
        public IActionResult GetTools()
        {
            var response = new
            {
                href = Url.Link(nameof(GetTools), null),
                texttools = new
                {
                    href = Url.Link(nameof(CovidTests.GetTests), null)
                }
            };

            return Ok(response);
        }

        // GET <TextTools>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet("{text}")]
        [ActionName("CustomerReplaceNulls")]
        public IActionResult CustomerReplaceNulls([FromBody] Customer model)
        {
            try {


                    model.BranchName = string.IsNullOrEmpty(model.BranchName) == true ? "" : model.BranchName;
                    model.CustomerName = string.IsNullOrEmpty(model.CustomerName) == true ? "" : model.CustomerName.Replace("\r\n", "");
                    //model.Municipality = string.IsNullOrEmpty(model.Municipality) == true ? "" : model.Municipality;
                    //model.OfficePhone = string.IsNullOrEmpty(model.OfficePhone) == true ? "" : model.OfficePhone;
                    //model.Region = string.IsNullOrEmpty(model.Region) == true ? "" : model.Region;
                    //model.Status = string.IsNullOrEmpty(model.Status) == true ? "" : model.Status;
                return Ok(model);
                
               
               
            } catch(Exception ex) {

                return BadRequest(ex.Message);

            }
        }

        // POST api/<TextTools>
        [HttpPost]
        public string Post([FromBody] string value)
        {
            return value.Replace("null", @"NA");
        }

        // PUT api/<TextTools>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TextTools>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
