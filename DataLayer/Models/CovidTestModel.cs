using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace EmployNetTools.DataLayer.Models
{


    public class CovidTestSearch
    {

        // ID that comes from Calendaring system
        public string CovidTestId { get; set; }
        //public int appointmentLocationID { get; set; }
        //public int AppointmentID { get; set; }
        //public string identifier { get; set; }
        //public string sample_collection_date { get; set; }
        //public string test_panel_code { get; set; }
        //public int num_samples { get; set; }
        //public string sample_1_identifier { get; set; }
        //public string sample_1_group_name { get; set; }
        //public string sample_1_collected_by { get; set; }
        //public string sample_1_date_received { get; set; }
        //public string sample_1_state_label { get; set; }
        //public string sample_1_state_position { get; set; }
        //public string sample_1_container_type { get; set; }
        //public string sample_1_container_barcode { get; set; }

        public string patient_first_name { get; set; }
        public string patient_last_name { get; set; }
        public string patient_email { get; set; }
        // public string patient_date_of_birth { get; set; }
        //public string patient_street_address { get; set; }
        // public string patient_city { get; set; }
        // public string patient_state { get; set; }

        public string patient_zip_code { get; set; }
        // public string patient_phone_number { get; set; }
        // are their specific sex types
        //public string patient_sex { get; set; }

        //public string provider_account { get; set; }
        //public string provider_npi { get; set; }

        //// are their specific bill types?
        //public string bill_type { get; set; }
        //public string primary_insurance_insurance_provider { get; set; }
        //public string primary_insurance_id_number { get; set; }

        //// are their specific relationships
        //public string primary_insurance_relationship_to_insured { get; set; }

        //public string institutional_bill_name { get; set; }

        //public string patient_bill_name { get; set; }

        //public string patient_bill_phone_number { get; set; }
        //public string patient_bill_street { get; set; }
        //public string patient_bill_city { get; set; }
        //public string patient_bill_state { get; set; }
        //public string patient_bill_zip { get; set; }

        //public string patient_schedule_date { get; set; }

    }

    public class CovidTestJson
    {

        // ID that comes from Calendaring system
        public string CovidTestId { get; set; }
        //public int appointmentLocationID { get; set; }
        public string AppointmentID { get; set; }
        //public string identifier { get; set; }
        public string sample_collection_date { get; set; }
        //public string test_panel_code { get; set; }
        //public string num_samples { get; set; }
        //public string sample_1_identifier { get; set; }
        //public string sample_1_group_name { get; set; }
        //public string sample_1_collected_by { get; set; }
        //public string sample_1_date_received { get; set; }
        //public string sample_1_state_label { get; set; }
        //public string sample_1_state_position { get; set; }
        //public string sample_1_container_type { get; set; }
        public string sample_1_container_barcode { get; set; }

        public string patient_first_name { get; set; }
        public string patient_last_name { get; set; }
        public string patient_email { get; set; }
        public string patient_date_of_birth { get; set; }
        public string patient_street_address { get; set; }
        public string patient_city { get; set; }
        public string patient_state { get; set; }

        public string patient_zip_code { get; set; }
        public string patient_phone_number { get; set; }
        // are their specific sex types
        public string patient_sex { get; set; }

        public string provider_account { get; set; }
        public string provider_npi { get; set; }

        //// are their specific bill types?
        public string bill_type { get; set; }
        public string primary_insurance_insurance_provider { get; set; }
        public string primary_insurance_id_number { get; set; }

        //// are their specific relationships
        public string primary_insurance_relationship_to_insured { get; set; }

        public string institutional_bill_name { get; set; }

        public string patient_bill_name { get; set; }

        public string patient_bill_phone_number { get; set; }
        public string patient_bill_street { get; set; }
        public string patient_bill_city { get; set; }
        public string patient_bill_state { get; set; }
        public string patient_bill_zip { get; set; }

        //public string patient_schedule_date { get; set; }

    }


    public class CovidTestDBModel
    {
        // ID that comes from Calendaring system
        [Key]
        public int CovidtestId { get; set; }
        public int appointmentLocationID { get; set; }
        public int AppointmentID { get; set; }
        public string identifier { get; set; }
        public DateTime sample_collection_date { get; set; }
        public string test_panel_code { get; set; }
        public int num_samples { get; set; }
        public string sample_1_identifier { get; set; }
        public string sample_1_group_name { get; set; }
        public string sample_1_collected_by { get; set; }
        public DateTime sample_1_date_received { get; set; }

        public string sample_1_state_label { get; set; }
        public string sample_1_state_position { get; set; }
        public string sample_1_container_type { get; set; }
        public string sample_1_container_barcode { get; set; }

        public string patient_first_name { get; set; }
        public string patient_last_name { get; set; }
        public string patient_email { get; set; }
        public DateTime patient_date_of_birth { get; set; }
        public string patient_street_address { get; set; }
        public string patient_city { get; set; }
        public string patient_state { get; set; }

        public string patient_zip_code { get; set; }
        public string patient_phone_number { get; set; }
        // are their specific sex types
        public string patient_sex { get; set; }

        public string provider_account { get; set; }
        public string provider_npi { get; set; }

        // are their specific bill types?
        public string bill_type { get; set; }
        public string primary_insurance_insurance_provider { get; set; }
        public string primary_insurance_id_number { get; set; }

        // are their specific relationships
        public string primary_insurance_relationship_to_insured { get; set; }

        public string institutional_bill_name { get; set; }

        public string patient_bill_name { get; set; }

        public string patient_bill_phone_number { get; set; }
        public string patient_bill_street { get; set; }
        public string patient_bill_city { get; set; }
        public string patient_bill_state { get; set; }
        public string patient_bill_zip { get; set; }

        public DateTime patient_schedule_date { get; set; }

        public string time_slot { get; set; }

    }

    public class ACTIVITY_LOG
    {

        [Key]
        public DateTime RecordTimeStamp {get; set;}

        public string ActivityDesc { get; set; }
        public string Error { get; set; }
    }

}