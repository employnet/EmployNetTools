using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text;

namespace EmployNetTools.DataLayer.Models.EmploynetDB
{
    class EmploynetDB
    {
    }

    public class PeopleNet_PDID
    {
        public string SSN { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int EmployeeNo { get; set; }
        public string Department { get; set; }
        public string Shift {get; set;}
        public DateTime DateTime { get; set; }
        public string InDay { get; set; }
        public string InTime { get; set; }
        public string OutDay { get; set; }
        public string OutTime { get; set; }
        public string Adjust { get; set; }
        public float TotalHours { get; set; }
        public float RegHours { get; set; }
        public float OTHours { get; set; }
        public float DTHours { get; set; }
    }

    public class LoginAccount
    {
        [Key]
        public int LoginId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PassCode { get; set; }

        public Guid Token { get; set; }

        public DateTime LastLoggedIn {get; set;}
    }
}
