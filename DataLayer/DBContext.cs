using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace EmployNetTools.DataLayer
{
    [Keyless]
    public class DataSurfContext : DbContext
    {


        public DataSurfContext(DbContextOptions<DataSurfContext> options) : base(options)
        {


        }
        public DbSet<Models.CovidTestDBModel> CovidTests { get; set; }

        public DbSet<Models.ACTIVITY_LOG> Activity_log { get; set; }

        public DbSet<Models.CovidQuestionsModel> CovidQuestions { get; set; }

        public DbSet<Models.CovidTestEvents> CovidTestEvents { get; set; }

        public DbSet<Models.TempWorksDB.Customer> Customer { get; set; }
        public DbSet<Models.TempWorksDB.Address> Address { get; set; }

        public DbSet<Models.TempWorksDB.Branch> Branch { get; set; }

        public DbSet<Models.TempWorksDB.Assignment> Assignment { get; set; }

        public DbSet<Models.TempWorksDB.Employee> Employee { get; set; }

        // list of employees not updated only
        public DbSet<Models.TempWorksDB.EmployeeList> EmployeeList { get; set; }

        // list of all employees regardless of update status
        public DbSet<Models.TempWorksDB.EmployeeAllList> EmployeeAllList { get; set; }

        public DbSet<Models.TempWorksDB.CustomerList> CustomerList { get; set; }
        public DbSet<Models.EmploynetDB.LoginAccount> Login { get; set; }

        public DbSet<Models.TempWorksDB.RepUserId> RepUserList { get; set; }

        public async System.Threading.Tasks.Task<Models.TempWorksDB.CustomerList[]> GetCustomerListAsync()
        {

            return await CustomerList.ToArrayAsync();

        }

        /// <summary>
        /// Get all employee id that have not being updated from detail
        /// </summary>
        /// <returns></returns>
        public async System.Threading.Tasks.Task<Models.TempWorksDB.EmployeeList[]> GetEmployeeListAsync()
        {
            
            return await EmployeeList.ToArrayAsync();

        }
        /// <summary>
        /// Get all employee Id 
        /// </summary>
        /// <returns></returns>
        public async System.Threading.Tasks.Task<Models.TempWorksDB.EmployeeAllList[]> GetEmployeeAllListAsync()
        {

            return await EmployeeAllList.ToArrayAsync();

        }


        public void SaveError(string activity, string error)
        {
            Activity_log.Add(new Models.ACTIVITY_LOG { RecordTimeStamp = DateTime.Now, ActivityDesc = activity, Error = error });
            SaveChanges();

        }

        public void SaveErrorProc(SqlConnection con, string Activity, string Error)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@ActivityDesc", Activity));
                cmd.Parameters.Add(new SqlParameter("@Error", Error));
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "prAddLog";
                cmd.ExecuteNonQuery();

            }

        }

        public async System.Threading.Tasks.Task<Models.TempWorksDB.RepUserId[]> GetUsersListAsync()
        {
            return await RepUserList.ToArrayAsync();
        }
    }

}
