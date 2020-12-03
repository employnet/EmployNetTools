using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployNetTools.DataLayer
{
    [Keyless]
    public class DataSurfContext:DbContext
    {
        public DataSurfContext(DbContextOptions<DataSurfContext> options) : base(options)
        {
            
        }

        
        
        public DbSet<Models.CovidTestDBModel> CovidTests { get; set; }

    }
}
