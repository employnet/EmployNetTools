using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using System.Web.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmployNetTools.DataLayer.Models.TempWorks;
using EmployNetTools.DataLayer;

//using FromBodyAttribute = System.Web.Http.FromBodyAttribute;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployNetTools.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class TextTools : ControllerBase
    {
        private readonly DataSurfContext _context;
        public  TextTools(DataSurfContext context)
        {
            _context = context;
        }

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

        [HttpPost]
        [ActionName("GetJsonFromCSV")]
        public IActionResult GetJsonFromCSV([FromBody] DataLayer.Models.GenericModels.GenericString fileContent)
        {
            int succesCount = 0;
            int errorCount = 0;
            int totalCount = 0;

            try
            {
                if (string.IsNullOrEmpty(fileContent.Message) == true)
                    return Ok("No content");

                string[] rows = fileContent.Message.Split("\n");
                totalCount = rows.Count();
                // first row has headers
                string[] headers = rows[0].Split(",");
                // skip second row
                int i = 2;
                _context.SaveError("Start importing file", "Row count " + rows.Count().ToString());
                _context.SaveChanges();
                foreach(string row in rows)
                //for (i = 2; i <= rows.Count() - 2; i++)
                {
                    if (string.IsNullOrEmpty(row) == true)
                        continue;

                    string[] columns = row.Split(",");
                    // cases where we don't want the records
                    if (columns[14] == columns[17])// inTime == outTime then skip
                    {
                        errorCount++;
                        _context.SaveError("Skipping PDID - In and Out same", columns[1] + " " + columns[2]);
 //                       _context.SaveChanges();
                        continue; 
                    }
                    float outHours;
                    bool ret = float.TryParse(columns[19], out outHours);
                    if(outHours==0 || ret==false) // total hours == 0
                    {
                        errorCount++;
                        _context.SaveError("Skipping PDID - no TotalHours", columns[1] + " " + columns[2] + " " + columns[19]);
 //                       _context.SaveChanges();
                        continue; }

                    DataLayer.Models.EmploynetDB.PeopleNet_PDID model = new DataLayer.Models.EmploynetDB.PeopleNet_PDID();
                    model.SSN = columns[0];
                    model.LastName = columns[1];
                    model.FirstName = columns[2];
                    int outValue;
                    int.TryParse(columns[3], out outValue);
                    model.EmployeeNo = outValue;
                    model.Department = columns[8];
                    model.Shift = columns[9];
                    DateTime outDate;
                    DateTime.TryParse(columns[11], out outDate);
                    model.DateTime = outDate;
                    model.InDay = columns[13];
                    model.InTime = columns[14];
                    model.OutDay = columns[16];
                    model.OutTime = columns[17];
                    model.Adjust = columns[18];
                    float outFloat;
                    float.TryParse(columns[19], out outFloat);
                    model.TotalHours = outFloat;
                    float.TryParse(columns[20], out outFloat);
                    model.RegHours = outFloat;
                    float.TryParse(columns[21], out outFloat);
                    model.OTHours = outFloat;
                    float.TryParse(columns[22], out outFloat);
                    model.DTHours = outFloat;

                    try
                    {
                        _context.PeopleNet_PDID.Add(model);
   //                     _context.SaveChanges();
                        succesCount++;
                    }
                    catch(Exception ex)
                    {
//                        _context.PeopleNet_PDID.Remove(model);
                        _context.SaveError("Add PDID", model.FirstName + " " + model.LastName + " " + model.DateTime.ToString() + " " + model.InTime + " " + model.OutTime + " " + model.TotalHours + " "
                            + ex.InnerException!=null?ex.InnerException.Message:ex.Message);
                        errorCount++;
//                        _context.SaveChanges();
                    }

                }
                _context.SaveChanges();
            }catch(Exception ex)
            {
                return Ok(ex.InnerException!=null?ex.InnerException.Message:ex.Message);
            }

            return Ok("Total count " + totalCount.ToString() + " Success Count " + succesCount.ToString() + " Error count " + errorCount.ToString());


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
