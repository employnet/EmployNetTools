using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using EmployNetTools.DataLayer.Models;

namespace EmployNetTools.DataLayer
{
    public class CovidTest
    {
        //public void InitializeAsync(IServiceProvider service, Models.CovidTestJson model)
        //{

        //    AddCovidRecord(service.GetRequiredService<DataSurfContext>(), model);

        //}

        public static bool AddGeniueSignup(DataSurfContext context, Models.CovidTestGenieJSon model)
        {
            Models.CovidTestDBModel find = context.CovidTests.FirstOrDefault(rec => rec.patient_first_name == model.firstname && rec.patient_last_name == model.lastname && rec.patient_email == model.email);
            if(find!=null) // we don't want to override the record in this case
                return false;

            find = new CovidTestDBModel();
            find.patient_email = model.email;
            find.patient_first_name = model.firstname;
            find.patient_last_name = model.lastname;
            find.patient_phone_number = model.phone;
            find.patient_schedule_date = DateTime.Parse(model.startdatestring);
            find.time_slot = model.time_slot;
            find.AppointmentID = Convert.ToInt32(model.itemnumberid);
            find.consent_date = DateTime.MinValue;

            context.CovidTests.Add(find);
            context.SaveChanges();
            return true;
        }
        public static bool AddCovidRecord(DataSurfContext context, Models.CovidTestJson model)
        {

            Models.CovidTestSearch search = new Models.CovidTestSearch();
            if (!string.IsNullOrEmpty(model.AppointmentID))
            {
                search.CovidTestId = model.AppointmentID;
            }
            else
            {
                search.CovidTestId = model.CovidtestId;
            }
                
            search.patient_first_name = model.patient_first_name;
            search.patient_last_name = model.patient_last_name;
            search.patient_email = model.patient_email;



            Models.CovidTestDBModel save = null;
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
            save.SSN = model.SSN;
            save.identification = model.identification;
            save.last_mod_dt = DateTime.Now;
            save.consent_date = model.patient_consent.ToLower() == "yes" ? DateTime.Now:DateTime.MinValue ;

            if (save.CovidtestId == 0)
                context.CovidTests.Add(save);
            else
                context.CovidTests.Update(save);




            Models.CovidQuestionsModel questions = context.CovidQuestions.FirstOrDefault(result => result.CovidTestId == save.CovidtestId);
            if (questions == null)
            {
                questions = new Models.CovidQuestionsModel();
                questions.CovidTestId = save.CovidtestId;
                questions.covid_in_household = model.covid_in_household == "Yes" ? true:false;
                questions.traveled_outside_us = model.traveled_outside_us == "Yes" ? true : false;
                questions.covid_positive = model.covid_positive == "Yes" ? true: false;
                questions.covid_recovered = model.covid_recovered == "Yes" ? true: false;
                questions.had_covid_contact = model.had_covid_contact == "Yes" ? true : false;
                questions.was_at_covid_risk_location = model.was_at_covid_risk_location == "Yes" ? true : false;

                context.CovidQuestions.Add(questions);
            }
            else
            {
                questions.CovidTestId = save.CovidtestId;
                questions.covid_in_household = model.covid_in_household == "Yes" ? true : false;
                questions.traveled_outside_us = model.traveled_outside_us == "Yes" ? true : false;
                questions.covid_positive = model.covid_positive == "Yes" ? true : false;
                questions.covid_recovered = model.covid_recovered == "Yes" ? true : false;
                questions.had_covid_contact = model.had_covid_contact == "Yes" ? true : false;
                questions.was_at_covid_risk_location = model.was_at_covid_risk_location == "Yes" ? true : false;
                context.CovidQuestions.Update(questions);
            }

            context.SaveChangesAsync();

            return true;
           
        }

        public static void AddEventRecord(DataSurfContext context, CovidTestEvents model)
        {
             CovidTestEvents result = context.CovidTestEvents.FirstOrDefault(result => result.SignupID == model.SignupID);

            if (result == null)
            { 
                result = new CovidTestEvents();
                result.SignupID = model.SignupID;
                result.startdate = model.startdate;
                result.enddate = model.enddate;
                result.Title = model.Title;
                result.TotalSlots = model.TotalSlots;
                result.FilledSlots = model.FilledSlots;
                context.CovidTestEvents.Add(result);
            }
            else
            {
                result.startdate = model.startdate;
                result.enddate = model.enddate;
                result.Title = model.Title;
                result.TotalSlots = model.TotalSlots;
                result.FilledSlots = model.FilledSlots;
                context.CovidTestEvents.Update(result);
            }
            context.SaveChangesAsync();
        }

        public static Models.CovidTestEvents GetCovidTestEvents(DataSurfContext context, int ID)
        {
            CovidTestEvents results = context.CovidTestEvents.FirstOrDefault(results => results.SignupEventId == ID);

            return results;
        
        }

        public static Models.CovidTestJson GetCovidRecord(DataSurfContext context, int ID)
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

                


                Models.CovidTestJson json =  MapModels(model);

                Models.CovidQuestionsModel questions = null;
                questions = context.CovidQuestions.FirstOrDefault(result => result.CovidTestId == model.CovidtestId);
                if (questions != null)
                {
                    json.covid_in_household = questions.covid_in_household == false ? "No" : "Yes";
                    json.covid_positive = questions.covid_positive == false ? "No" : "Yes";
                    json.covid_recovered = questions.covid_recovered == false ? "No" : "Yes";
                    json.had_covid_contact = questions.had_covid_contact == false ? "No" : "Yes";
                    json.was_at_covid_risk_location = questions.was_at_covid_risk_location == false ? "No" : "Yes";
                    json.traveled_outside_us = questions.traveled_outside_us == false ? "No" : "Yes";
                }



                return json;

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

            json.CovidtestId = model.CovidtestId.ToString();
            json.AppointmentID = model.AppointmentID.ToString();


            //json.appointmentLocationID = "";
            DateTime scd;
            if (model.patient_date_of_birth == null)
                scd = DateTime.MinValue;
            else
                scd = model.patient_date_of_birth.Value;
            json.sample_collection_date = scd.ToString("yyyy-MM-dd");

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
            DateTime dob;
            if (model.patient_date_of_birth == null)
                dob = DateTime.MinValue;
            else
                dob = model.patient_date_of_birth.Value;

            json.patient_date_of_birth = dob.ToString("yyyy-MM-dd");
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
            json.identification = model.identification;
            json.SSN = model.SSN;
            json.patient_consent = "No";
                if(model.consent_date != DateTime.MinValue)
            {
                json.patient_consent = "Yes";
            }
            
            return json;
        }

        public static Models.CovidTestJson GetCovidJson(DataSurfContext context, Models.CovidTestSearch search)
        {
            Models.CovidTestJson json = MapModels(GetCovidRecord(context, search));
            Models.CovidQuestionsModel questions = context.CovidQuestions.FirstOrDefault(results => results.CovidTestId == Convert.ToInt32(json.CovidtestId));
            if (questions != null)
            {
                json.covid_in_household = questions.covid_in_household == false ? "No" : "Yes";
                json.covid_positive = questions.covid_positive == false ? "No" : "Yes";
                json.covid_recovered = questions.covid_recovered == false ? "No" : "Yes";
                json.had_covid_contact = questions.had_covid_contact == false ? "No" : "Yes";
                json.was_at_covid_risk_location = questions.was_at_covid_risk_location == false ? "No" : "Yes";
                json.traveled_outside_us = questions.traveled_outside_us == false ? "No" : "Yes";
            }
            return json;
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

                model = context.CovidTests.FirstOrDefault(results => results.CovidtestId == ID);
                if(model==null)
                {
                    model = context.CovidTests.FirstOrDefault(results => results.patient_first_name == search.patient_first_name
                                   && results.patient_last_name == search.patient_last_name
                                   //&& results.patient_zip_code == search.patient_zip_code
                                   && results.patient_email == search.patient_email);


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
