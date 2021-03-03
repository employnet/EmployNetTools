using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployNetTools.DataLayer.Models.TempWorks;
using EmployNetTools.DataLayer;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using RingCentral.Paths.Restapi;

namespace EmployNetTools.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class RingCentral : ControllerBase
    {
        private readonly DataSurfContext _context;
        private readonly string ConnectionString = "Server=employnetdata.database.windows.net;Database=DataSurf;Trusted_Connection=false;User Id=employnet;password=Employ1Now!";
        private bool[] isRunning;

        public RingCentral(DataSurfContext context)
        {
            context.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking;
            _context = context;
            isRunning = new bool[100];
        }

        [HttpGet]
        [ActionName("GetAccount")]
        public async Task<IActionResult> GetAccount()
        {
            DataLayer.RingCentralAPI api = new RingCentralAPI();
            var huh = await api.GetAccount();
            return Ok(huh);
        }
        [HttpGet]
        [ActionName("GetCallLogs")]
        public async Task<IActionResult> GetCallLogs()
        {
            DataLayer.RingCentralAPI api = new RingCentralAPI();
            await api.GetCallLogs();
            return Ok();
        }
        [HttpGet]
        [ActionName("GetContacts")]
        public async Task<IActionResult> GetContacts()
        {
            DataLayer.RingCentralAPI api = new RingCentralAPI();
            await api.GetContacts();
            return Ok();

        }

        [HttpGet]
        [ActionName("GetMessages")]
        public async Task<IActionResult> GetMessages()
        {
            DataLayer.RingCentralAPI api = new RingCentralAPI();
            await api.ReadMessages();
            return Ok();
        }

        [HttpGet]
        [ActionName("getGlipPerson")]
        public async Task<IActionResult> getGlipPerson(string id)
        {
            DataLayer.RingCentralAPI api = new RingCentralAPI();
            await api.GetGlipPerson(id);
            return Ok();

        }
    }
}
    
