using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using System.Web.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmployNetTools.DataLayer.Models.TempWorks;
using EmployNetTools.DataLayer;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

//using FromBodyAttribute = System.Web.Http.FromBodyAttribute;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployNetTools.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class TempWorks : ControllerBase
    {
        private readonly DataSurfContext _context;

        public TempWorks(DataSurfContext context)
        {
            context.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking;
            _context = context;
        }

       
        // GET: <TextTools>
        [HttpGet(Name = nameof(WhoAmI))]
        public IActionResult WhoAmI()
        {

            return Ok("Hi");
        }

        // GET <TextTools>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        [ActionName("AddEmployees")]
        public IActionResult AddEmployees([FromBody] Employees models)
        {

//            DbContextOptionsBuilder options = new DbContextOptionsBuilder();

            string conStr = "Server=employnetdata.database.windows.net;Database=DataSurf;Trusted_Connection=false;User Id=employnet;password=Employ1Now!";
 
            using (SqlConnection con = new SqlConnection(conStr))
            {

                try
                {
                    con.Open();
                    if (models.data == null || models.data.Count() == 0)
                    {
                        _context.SaveError("Nothing to process, end of employees", "");
                        return Ok("success");
                    }
                    int start = models.data[0].employeeId;
                    _context.SaveError("Add Bulk Employee", "Total " + models.data.Count() + " records, starting at id " + start.ToString());

                    
                    int i = 0; // debug useage
                    foreach (Employee model in models.data)
                    {
                        i++;
                        //DataLayer.TempWorks.AddEmployee(con, model);
                        DataLayer.TempWorks.AddEmployeeProc(con, model);
                    }
                    // int count = _context.SaveChangesAsync();
                    //con.SaveChanges();


                    _context.SaveError("End of bulk employees", "saved " + models.data.Count().ToString() + " records, starting at id " + start.ToString());
                    //con.SaveError("End of bulk employees", "saved " + models.data.Count().ToString() + " records, starting at id " + start.ToString());
                }
                catch (Exception ex)
                {
                    _context.SaveError("Error in employees", ex.Message);

                    return BadRequest(ex.Message);
                }
            }
            return Ok();
        }
        
        [HttpPost]
        [ActionName("AddAssignments")]
        public IActionResult AddAssignments([FromBody] Assignments models)
        {
            string conStr = "Server=employnetdata.database.windows.net;Database=DataSurf;Trusted_Connection=false;User Id=employnet;password=Employ1Now!";

            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    con.Open();

                    if (models.data == null || models.data.Count() == 0)
                    {
                        _context.SaveError("Nothing to process, end of asssignments", "");
                        return Ok("success");
                    }
                    int start = models.data[0].assignmentId;
                    _context.SaveError("Add Bulk Assignment", "Total " + models.data.Count() + " records, starting at id " + start.ToString());
                    foreach (Assignment model in models.data)
                    {
                        DataLayer.TempWorks.AddAssignmentProc(con, model);
                    }
                    //_context.SaveChanges();
                }
                catch (Exception ex)
                {
                    _context.SaveError("Error in assignments", ex.Message);
                    return BadRequest(ex.Message);
                }
                _context.SaveError("End of bulk assignments", "Total " + models.data.Count() + " records, starting at id " + models.data[0].assignmentId);
            }
            return Ok("Success");
        }

        [HttpPost]
        [ActionName("AddCustomer")]
        public IActionResult AddCustomer([FromBody] Customer model)
        {
            try {

                
                _context.SaveError("Starting Add Customer", "CustomerId = " + model.CustomerId.ToString());

                    model.BranchName = string.IsNullOrEmpty(model.BranchName) == true ? "" : model.BranchName;
                    model.CustomerName = string.IsNullOrEmpty(model.CustomerName) == true ? "" : model.CustomerName.Replace("\r\n", "");
                    //model.Municipality = string.IsNullOrEmpty(model.Municipality) == true ? "" : model.Municipality;
                    //model.OfficePhone = string.IsNullOrEmpty(model.OfficePhone) == true ? "" : model.OfficePhone;
                    //model.Region = string.IsNullOrEmpty(model.Region) == true ? "" : model.Region;
                    //model.Status = string.IsNullOrEmpty(model.Status) == true ? "" : model.Status;

                DataLayer.TempWorks.AddCustomer(_context, model);

                _context.SaveError("Ending Add Customer", "CustomerId = " + model.CustomerId.ToString());

                return Ok();
                
               
               
            } catch(Exception ex) {

                _context.SaveError("Error in Add Customer", ex.Message);
                return BadRequest(ex.Message);

            }
        }

        [HttpPost]
        [ActionName("AddBranch")]
        public IActionResult AddBranch([FromBody] Branch model)
        {
            try
            {

                _context.SaveError("Starting Add Branch", "BranchId = " + model.BranchId.ToString());


                DataLayer.TempWorks.AddBranch(_context, model);

                _context.SaveError("Ending Add Branch", "BranchId = " + model.BranchId.ToString());

                return Ok();



            }
            catch (Exception ex)
            {

                _context.SaveError("Error in Add Branch", ex.Message);
                return BadRequest(ex.Message);

            }
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
