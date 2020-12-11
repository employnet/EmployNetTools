using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace EmployNetTools.DataLayer
{
    public class CovidTest
    {
        //public void InitializeAsync(IServiceProvider service, Models.CovidTestJson model)
        //{

        //    AddCovidRecord(service.GetRequiredService<DataSurfContext>(), model);

        //}
        public static bool AddCovidRecord(DataSurfContext context, Models.CovidTestJson model)
        {

            Models.CovidTestSearch search = new Models.CovidTestSearch();
            if (!string.IsNullOrEmpty(model.AppointmentID))
                search.CovidTestId = model.AppointmentID;
            else
                search.CovidTestId = model.CovidTestId;

            Models.CovidTestDBModel save = null;

            Models.CovidTestJson json;
            save = GetCovidRecord(context, search);
            //Models.CovidTestDBModel save = new Models.CovidTestDBModel();

            if (save == null)
            {
                save = new Models.CovidTestDBModel();
                save.AppointmentID = 0;
                save.CovidtestId = 0;
            }

            save.appointmentLocationID = 0;
            save.sample_collection_date = DateTime.UtcNow;
            save.patient_first_name = model.patient_first_name;
            save.patient_last_name = model.patient_last_name;
            save.patient_phone_number = model.patient_phone_number;
            save.patient_sex = model.patient_sex;
            save.patient_state = model.patient_state;
            save.patient_city = model.patient_city;
            save.patient_street_address = model.patient_street_address;
            save.patient_zip_code = model.patient_zip_code;
            save.patient_bill_city = model.patient_bill_city;
            save.patient_bill_name = model.patient_bill_name;
            save.patient_bill_state = model.patient_bill_state;
            save.patient_bill_street = model.patient_bill_street;
            save.patient_bill_zip = model.patient_bill_zip;
            save.patient_date_of_birth = Convert.ToDateTime(model.patient_date_of_birth);
            save.patient_email = model.patient_email;
            save.sample_1_container_barcode = model.sample_1_container_barcode;
            save.identifier = model.sample_1_container_barcode;
            save.sample_1_collected_by = "Maricopa Technician";
            save.sample_1_container_type = "TUBE";
            save.identifier = model.sample_1_container_barcode;
            save.sample_1_state_position = "A01";
            save.test_panel_code = "IDT-COVID-19";
            save.sample_1_identifier = "CLINTEST01";
            save.num_samples = 1;
            save.provider_npi = "1639369036";
            save.institutional_bill_name = model.institutional_bill_name;
            save.primary_insurance_id_number = model.primary_insurance_id_number;
            save.primary_insurance_insurance_provider = model.primary_insurance_insurance_provider;
            save.primary_insurance_relationship_to_insured = model.primary_insurance_relationship_to_insured;
            save.bill_type = string.IsNullOrEmpty(save.primary_insurance_insurance_provider) ? "Institutional Bill" : "Insurance";
            save.patient_bill_phone_number = model.patient_bill_phone_number;
            save.provider_account = model.provider_account;

                if (save.CovidtestId == 0)
                    context.CovidTests.Add(save);
                else
                    context.CovidTests.Update(save);

                context.SaveChangesAsync();
                return true;
           
        }

        public static Models.CovidTestJson GetCovidRecord(DataSurfContext context, long ID)
        {
            try
            {
                Models.CovidTestDBModel model = null;
                
                model = context.CovidTests.FirstOrDefault(result => result.CovidtestId == ID);

                if(model==null)
                {
                    model = context.CovidTests.FirstOrDefault(result => result.AppointmentID == ID);
                }

                if (model == null)
                    return null;

                


                return MapModels(model);
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        private static Models.CovidTestJson MapModels(Models.CovidTestDBModel model)
        {

            if (model == null) return null;

            Models.CovidTestJson json = new Models.CovidTestJson();

            json.CovidTestId = model.CovidtestId.ToString();
            json.AppointmentID = model.AppointmentID.ToString();


            //json.appointmentLocationID = "";
            json.sample_collection_date = model.sample_collection_date.ToString("dd-MM-yyyy");
            json.patient_first_name = model.patient_first_name;
            json.patient_last_name = model.patient_last_name;
            json.patient_phone_number = model.patient_phone_number;
            json.patient_sex = model.patient_sex;
            json.patient_state = model.patient_state;
            json.patient_city = model.patient_city;
            json.patient_street_address = model.patient_street_address;
            json.patient_zip_code = model.patient_zip_code;
            json.patient_bill_city = model.patient_bill_city;
            json.patient_bill_name = model.patient_bill_name;
            json.patient_bill_state = model.patient_bill_state;
            json.patient_bill_street = model.patient_bill_street;
            json.patient_bill_zip = model.patient_bill_zip;
            json.patient_date_of_birth = model.patient_date_of_birth.ToString("dd-MM-yyy");
            json.patient_email = model.patient_email;
            json.sample_1_container_barcode = model.sample_1_container_barcode;
            //json.identifier = model.identifier;
            //json.sample_1_collected_by = "Maricopa Technician";
            //json.sample_1_container_type = "TUBE";
            //save.identifier = "CLINTEST01";
            //json.sample_1_state_position = "A01";
            //json.test_panel_code = "IDT-COVID-19";
            //json.sample_1_identifier = "";
            //json.num_samples = 1;
            //json.provider_npi = "1639369036";
            json.institutional_bill_name = model.institutional_bill_name;
            json.primary_insurance_id_number = model.primary_insurance_id_number;
            json.primary_insurance_insurance_provider = model.primary_insurance_insurance_provider;
            json.primary_insurance_relationship_to_insured = model.primary_insurance_relationship_to_insured;
            json.bill_type = model.bill_type;
            json.patient_bill_phone_number = model.patient_bill_phone_number;
            json.provider_account = model.provider_account;

            return json;
        }

        public static Models.CovidTestJson GetCovidJson(DataSurfContext context, Models.CovidTestSearch search)
        {
            return MapModels(GetCovidRecord(context, search));

        }
        public static Models.CovidTestDBModel GetCovidRecord(DataSurfContext context, Models.CovidTestSearch search)
        {
            Models.CovidTestDBModel model = null;
            Int32 ID = 0;
            if (string.IsNullOrEmpty(search.CovidTestId))
                ID = 0;
            else
                ID = Convert.ToInt32(search.CovidTestId);


            if (ID!=0)
            {

                model = context.CovidTests.FirstOrDefault(results => results.AppointmentID == Convert.ToInt32(search.CovidTestId));
                if(model==null)
                {
                    model = context.CovidTests.FirstOrDefault(results => results.CovidtestId == ID);
                }
                if (model != null)
                    return model;
            
            }
            else
            {
                model = context.CovidTests.FirstOrDefault(results => results.patient_first_name == search.patient_first_name
                    && results.patient_last_name == search.patient_last_name
                    //&& results.patient_zip_code == search.patient_zip_code
                    && results.patient_email == search.patient_email);


                return model;
            }

            return null;

        }
    }
}
