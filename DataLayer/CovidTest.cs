using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace EmployNetTools.DataLayer
{
    public class CovidTest
    {
        public static async Task InitializeAsync(IServiceProvider service, Models.CovidTestDBModel model)
        {

            await AddCovidRecord(service.GetRequiredService<DataSurfContext>(), model);

        }
        public static async Task AddCovidRecord(DataSurfContext context, Models.CovidTestDBModel model)
        {
            try
            {
                if (model.sample_1_date_received == DateTime.MinValue)
                    model.sample_1_date_received = DateTime.Now;
                if (model.patient_date_of_birth == DateTime.MinValue)
                    model.sample_1_date_received = DateTime.Now;
                if (model.sample_collection_date == DateTime.MinValue)
                    model.sample_collection_date = DateTime.Now;


                var covidTest = context.CovidTests.Add(model);
                await context.SaveChangesAsync();
                return;
            }
            catch (Exception ex)
            {
                return;
            }
        }

        public static Models.CovidTestDBModel GetCovidRecord(DataSurfContext context, int ID)
        {
            try
            {
                Models.CovidTestDBModel model = new Models.CovidTestDBModel();
                
                model = context.CovidTests.Find(ID);
                return model;
            }
            catch
            {
                return null;
            }
        }
    }
}
