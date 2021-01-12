using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text;

namespace EmployNetTools.DataLayer.Models.TempWorks
{
    class TempWorksModel
    {

    }


    public class Customers
    {
        public int count { get; set; }
        public Customer[] data { get; set; }
    }

    public class Customer
    {
        public Int64 CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string DepartmentName { get; set; }

        private Int64 parentCustomerId;
        public Int64? ParentCustomerId { get; set; }

        public Int64 RootCustomerId { get; set; }

        public int BranchId { get; set; }

        public string BranchName { get; set; }

        public string CustomerStatusId { get; set; }

        public string CustomerStatus { get; set; }

        public string Website { get; set; }

        public bool IsActive { get; set; }

        public string DateActivated { get; set; }

        public Address Address { get; set; }

        public Address BillingAddress { get; set; }

        public int WorksiteId { get; set; }

        public string WorksiteName { get; set; }

        public Address WorksiteAddress { get; set; }

        public string Note { get; set; }
    }

    public class Address
    {
        public string Street1 { get; set; }

        public string Street2 { get; set; }

        public string Municipality { get; set; }

        public string Region { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        //   public string CountryCode { get; set; }

        public string AttentionTo { get; set; }

        // public DateTime DateAddressStandardized { get; set; }
    }


    public class Branch
    {
        [Key]
        public int BranchId { get; set; }

        public string BranchName { get; set; }

        public string BranchFullName { get; set; }

        public bool IsActive { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        public Address Address { get; set; }

        //    public object Region { get; set; }

        public int EmployerId { get; set; }

        public string Employer { get; set; }

        public int HierId { get; set; }

        //    public object DistanceToLocation { get; set; }

        //    public object DistanceUnit { get; set; }
    }


    public class Assignments
    {
        public Assignment[] data { get; set; }
        public int totalCount { get; set; }
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
        public float payRate { get; set; }
        public float billRate { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public int branchId { get; set; }
        public string branchName { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public int jobOrderId { get; set; }
        public string supervisor { get; set; }
        public string supervisorContactInfo { get; set; }
        public string originalStartDate { get; set; }
        public string expectedEndDate { get; set; }
        public int activeStatus { get; set; }
        public int assignmentStatusId { get; set; }
        public string assignmentStatus { get; set; }
        public string performanceNote { get; set; }
        public bool isTimeclockOrder { get; set; }

    }

    public class Employees
    {
        public Employee[] data { get; set; }
        public int totalCount { get; set; }
    }

    public class Employee
    {
        public int employeeId { get; set; }
        public string employeeGuid { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string alias { get; set; }

        public string namePrefix { get; set; }
        public string nameSuffix { get; set; }

        public string governmentPersonalId { get; set; }
        public string branch { get; set; }

        public int branchid { get; set; }
        public string phoneNumber { get; set; }
        public bool isActive { get; set; }
        public bool isAssigned { get; set; }
        public string lastMessage { get; set; }
        public string postalCode { get; set; }
        public bool hasResumeOnFile { get; set; }
        public string cellPhoneNumber { get; set; }
        public string emailAddress { get; set; }
        public string activationDate { get; set; }

        public string deactivationDate { get; set; }

        public string resumeDocumentId { get; set; }
        public string resumeFileName { get; set; }

        public bool isI9OnFile { get; set; }
        public string i9ExpirationDate { get; set; }
        public string jobTitle { get; set; }
        public string note { get; set; }
        public int numericRating { get; set; }
        public int serviceRepId { get; set; }
        public string serviceRep { get; set; }
        public string serviceRepChatName { get; set; }
        public int createdByServiceRepId { get; set; }
        public string createdByServiceRep { get; set; }
        public string companyId { get; set; }
        public string alternateEmployeeId { get; set; }
        public string company { get; set; }
        public int employerId { get; set; }
        public string employer { get; set; }
        public Address address { get; set; }
        public string driverLicenseNumber { get; set; }
        public string driverLicenseState { get; set; }
        public string driverLicenseClass { get; set; }
        public string driverLicenseExpire { get; set; }
    }

    public class Documents
    {
        public Document[] data { get; set; }
        public int count { get; set; }
    }

    public class Document
    {
        public int documentId { get; set; }
        public string documentDate { get; set; }
        public string description { get; set; }
        public string fileType { get; set; }
        public int documentTypeId { get; set; }
        public string documentType { get; set; }
        public string documentName { get; set; }
        public string fileName { get; set; }
        public bool canBeDeleted { get; set; }
        public bool canBeEdited { get; set; }
    }

    public class JobOrders
    {
        public int count { get; set; }
        public JobOrder[] data { get; set; }
    }
    public class JobOrder
    {
        public Int64 orderId { get; set; }
        public Int64 customerId { get; set; }
        public string customerName { get; set; }
        public string departmentName  { get; set; }
        public string jobTitle  { get; set; }
        public string orderType { get; set; }
        public double payRate { get; set; }
        public double billRate { get; set; }
        public string startDate { get; set; }
        public string orderStatus { get; set; }
        public Int32 positionsRequired { get; set; }
        public Int32 positionsFilled { get; set; }
        public string worksite { get; set; }
    }
}
    



