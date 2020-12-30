using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text;

namespace EmployNetTools.DataLayer.Models.TempWorksDB
{
    class TempWorksDBModel
    {

    }

    

    public class Customer
        {
        [Key]
            public int CustomerId { get; set; }

            public string CustomerName { get; set; }

            public string DepartmentName { get; set; }

            public int ParentCustomerId { get; set; }

            public int RootCustomerId { get; set; }

            public int BranchId { get; set; }

            public string BranchName { get; set; }

            public string CustomerStatusId { get; set; }

            public string CustomerStatus { get; set; }

            public string Website { get; set; }

            public bool IsActive { get; set; }

            public DateTime DateActivated { get; set; }

            public int AddressID { get; set; }

            public int BillingAddressID { get; set; }

            public int WorksiteId { get; set; }

            public string WorksiteName { get; set; }

            public int WorksiteAddressID { get; set; }

            public string Note { get; set; }
        }

        public class Address
        {
            [Key]
            public int AddressId { get; set; }
            public string Street1 { get; set; }

            public string Street2 { get; set; }

            public string Municipality { get; set; }

            public string Region { get; set; }

            public string PostalCode { get; set; }

            public string Country { get; set; }

            public int CountryCode { get; set; }

            public string AttentionTo { get; set; }

            public DateTime dateAddressStandardized { get; set; }
        }

    public class Branch
    {
        public int BranchId { get; set; }

        public string BranchName { get; set; }

        public string BranchFullName { get; set; }

        public bool IsActive { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        public int AddressId { get; set; }

        public string Region { get; set; }

        public int EmployerId { get; set; }

        public string Employer { get; set; }

        public int HierId { get; set; }

//        public object DistanceToLocation { get; set; }

//        public object DistanceUnit { get; set; }
    }


    public class Assignment
    {
        [Key]
        public int assignmentId { get; set; }
        public string lastName { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public int employeeId { get; set; }
        public int customerId { get; set; }
        public string customerName { get; set; }
        public string departmentName { get; set; }
        public string jobTitle { get; set; }
        public decimal payRate { get; set; }
        public decimal billRate { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public int branchId { get; set; }
        public string branchName { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public int jobOrderId { get; set; }
        public string supervisor { get; set; }
        public string supervisorContactInfo { get; set; }
        public DateTime originalStartDate { get; set; }
        public DateTime expectedEndDate { get; set; }
        public int activeStatus { get; set; }
        public int assignmentStatusId { get; set; }
        public string assignmentStatus { get; set; }
        public string performanceNote { get; set; }
        public bool isTimeclockOrder { get; set; }

    }

    public class Employee
    {
        [Key]
        public int employeeId { get; set; }
        public string employeeGuid { get; set; }
        public int branchId  { get; set; }
        public string branchName { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string alias { get; set; }
        public string namePrefix { get; set; }
        public string nameSuffix { get; set; }
        public string governmentPersonalId { get; set; }
        public bool isActive { get; set; }
        public DateTime activationDate { get; set; }
        public DateTime deactivationDate { get; set; }
        public int resumeDocumentId { get; set; }
        public string resumeFileName { get; set; }
        public bool isI9OnFile { get; set; }
        public DateTime i9ExpirationDate { get; set; }
        public string jobTitle { get; set; }
        public string note { get; set; }
        public int numericRating { get; set; }
        public int serviceRepId { get; set; }
        public string serviceRep { get; set; }
        public string serviceRepChatName { get; set; }
        public int createdByServiceRepId { get; set; }
        public string createdByServiceRep { get; set; }
        public int companyId { get; set; }
        public string company { get; set; }
        public int alternateEmployeeId { get; set; }
        public int employerId { get; set; }
        public string employer { get; set; }
        public string driverLicenseNumber { get; set; }
        public string driverLicenseClass { get; set; }
        public string driverLicenseExpire { get; set; }
        public int addressId { get; set; }
    }
}
