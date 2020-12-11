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

        public DbSet<Models.ACTIVITY_LOG> Activity_log { get; set; }

        public void SaveError(string activity, string error)
        {
            Activity_log.Add(new Models.ACTIVITY_LOG { RecordTimeStamp = DateTime.Now, ActivityDesc = activity, Error = error });
            SaveChanges();

        }
    
    }


}
