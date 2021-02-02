using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text;

namespace EmployNetTools.DataLayer.Models.EmploynetDB
{
    class EmploynetDB
    {
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
