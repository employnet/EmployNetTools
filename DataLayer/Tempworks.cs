﻿using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using EmployNetTools.DataLayer;
using EmployNetTools.DataLayer.Models.TempWorks;

namespace EmployNetTools.DataLayer
{
    public class TempWorks
    {

        public static int AddAddressProc(SqlConnection con, DataLayer.Models.TempWorks.Address model)
        {
            if (String.IsNullOrEmpty(model.Street1)) return 0;

            int AddressId = 0;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@street1", model.Street1));
                cmd.Parameters.Add(new SqlParameter("@street2", string.IsNullOrEmpty(model.Street2) ? "" : model.Street2));
                cmd.Parameters.Add(new SqlParameter("@postalCode", model.PostalCode));
                cmd.Parameters.Add(new SqlParameter("@region", model.Region));
                cmd.Parameters.Add(new SqlParameter("@municipality", model.Municipality));
                cmd.Parameters.Add(new SqlParameter("@attentionTo", string.IsNullOrEmpty(model.AttentionTo) ? "" : model.AttentionTo));
                cmd.Parameters.Add(new SqlParameter("@country", model.Country));

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "prAddAddress";
                var ret = cmd.ExecuteScalar();
                if (ret != null)
                    AddressId = (int)ret;

            }

            return AddressId;
        }



        public static int AddAddress(DataSurfContext context, DataLayer.Models.TempWorksDB.Address model)
        {
            if (String.IsNullOrEmpty(model.Street1)) return 0;

            Models.TempWorksDB.Address result = context.Address.FirstOrDefault(result => result.Street1 == model.Street1 && result.PostalCode == model.PostalCode);
            bool addRec = false;
            if (result == null)
            {
                addRec = true;
                result = new Models.TempWorksDB.Address();
            }
            result.Street1 = model.Street1;
            result.Street2 = model.Street2;
            result.PostalCode = model.PostalCode;
            result.Region = model.Region;
            result.Municipality = model.Municipality;
            result.AttentionTo = model.AttentionTo;
            result.Country = model.Country;

            if (addRec)
                context.Address.Add(result);
            else
                context.Address.Update(result);

            context.SaveChanges();

            return result.AddressId;
        }

        public static bool AddCustomerProc(SqlConnection con, DataLayer.Models.TempWorks.Customer model, Int64 customerId = 0, bool replace = false)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@override", replace == false ? 0 : 1));
                cmd.Parameters.Add(new SqlParameter("@customerId", model.CustomerId));
                cmd.Parameters.Add(new SqlParameter("@customerName", model.CustomerName));
                cmd.Parameters.Add(new SqlParameter("@departmentName", model.DepartmentName));
                cmd.Parameters.Add(new SqlParameter("@parentCustomerId", customerId != 0 ? customerId : model.ParentCustomerId));
                cmd.Parameters.Add(new SqlParameter("@rootCustomerId", model.RootCustomerId));
                cmd.Parameters.Add(new SqlParameter("@branchId", model.BranchId));
                cmd.Parameters.Add(new SqlParameter("@branchName", model.BranchName));
                cmd.Parameters.Add(new SqlParameter("@customerStatusId", model.CustomerStatusId));
                cmd.Parameters.Add(new SqlParameter("@customerStatus", model.CustomerStatus));
                cmd.Parameters.Add(new SqlParameter("@website", String.IsNullOrEmpty(model.Website) ? "" : model.Website));
                cmd.Parameters.Add(new SqlParameter("@isActive", model.IsActive));
                cmd.Parameters.Add(new SqlParameter("@dateActivated", DateTime.Parse(String.IsNullOrEmpty(model.DateActivated) ? "01/01/1900" : model.DateActivated)));
                cmd.Parameters.Add(new SqlParameter("@worksiteId", model.WorksiteId));
                cmd.Parameters.Add(new SqlParameter("@worksiteName", model.WorksiteName));

                cmd.Parameters.Add(new SqlParameter("@note", model.Note));

                cmd.Parameters.Add(new SqlParameter("@addressId", AddAddressProc(con, model.Address)));
                cmd.Parameters.Add(new SqlParameter("@billingAddressId", AddAddressProc(con, model.BillingAddress)));
                cmd.Parameters.Add(new SqlParameter("@worksiteAddressId", AddAddressProc(con, model.WorksiteAddress)));
                cmd.Parameters.Add(new SqlParameter("@isKronosTime", 0));

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "prAddCustomer";
                cmd.ExecuteNonQuery();

            }
            return true;
        }

        public static bool AddCustomer(DataSurfContext context, DataLayer.Models.TempWorks.Customer model)
        {


            Models.TempWorksDB.Customer result = context.Customer.FirstOrDefault(results => results.CustomerId == model.CustomerId);
            bool addRec = false;
            if (result == null)
            {
                addRec = true;
                result = new Models.TempWorksDB.Customer();
            }
            result.CustomerId = model.CustomerId;
            result.CustomerName = model.CustomerName;
            result.CustomerStatus = model.CustomerStatus;
            result.CustomerStatusId = model.CustomerStatusId;
            result.DateActivated = DateTime.Parse(model.DateActivated);
            result.DepartmentName = model.DepartmentName;
            //result.ParentCustomerId = model.ParentCustomerId;
            result.RootCustomerId = model.RootCustomerId;


            Models.TempWorksDB.Address add = new Models.TempWorksDB.Address();
            add.Street1 = model.BillingAddress.Street1;
            add.Street2 = model.BillingAddress.Street2;
            add.PostalCode = model.BillingAddress.PostalCode;
            add.Municipality = model.BillingAddress.Municipality;
            add.Region = model.BillingAddress.Region;
            add.AttentionTo = model.BillingAddress.AttentionTo;
            add.Country = model.BillingAddress.Country;

            result.BillingAddressID = AddAddress(context, add);

            add.Street1 = model.WorksiteAddress.Street1;
            add.Street2 = model.WorksiteAddress.Street2;
            add.Municipality = model.WorksiteAddress.Municipality;
            add.PostalCode = model.WorksiteAddress.PostalCode;
            add.Region = model.WorksiteAddress.Region;
            add.AttentionTo = model.WorksiteAddress.AttentionTo;
            add.Country = model.WorksiteAddress.Country;

            result.WorksiteAddressID = AddAddress(context, add);


            result.BranchId = model.BranchId;
            result.BranchName = model.BranchName;




            if (addRec)
                context.Customer.Add(result);
            else
                context.Customer.Update(result);

            context.SaveChanges();


            return true;

        }


        public static void AddEmployeeProc(SqlConnection con, Employee model, bool replace = false)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@override", replace == false ? 0 : 1));
                cmd.Parameters.Add(new SqlParameter("@employeeId", model.employeeId));
                cmd.Parameters.Add(new SqlParameter("@employeeGuid", model.employeeGuid));
                cmd.Parameters.Add(new SqlParameter("@branchId", model.branchid));
                cmd.Parameters.Add(new SqlParameter("@branchName", model.branch));
                cmd.Parameters.Add(new SqlParameter("@firstName", model.firstName));
                cmd.Parameters.Add(new SqlParameter("@middleName", model.middleName));
                cmd.Parameters.Add(new SqlParameter("@lastName", model.lastName));
                cmd.Parameters.Add(new SqlParameter("@phoneNumber", string.IsNullOrEmpty(model.cellPhoneNumber) ? model.phoneNumber : model.cellPhoneNumber));
                cmd.Parameters.Add(new SqlParameter("@alias", model.alias));
                cmd.Parameters.Add(new SqlParameter("@namePrefix", model.namePrefix));
                cmd.Parameters.Add(new SqlParameter("@nameSuffix", model.nameSuffix));
                cmd.Parameters.Add(new SqlParameter("@governmentPersonalId", model.governmentPersonalId));
                cmd.Parameters.Add(new SqlParameter("@isActive", model.isActive));
                cmd.Parameters.Add(new SqlParameter("@isAssigned", model.isAssigned));
                cmd.Parameters.Add(new SqlParameter("@activationDate", DateTime.Parse(String.IsNullOrEmpty(model.activationDate) ? "01/01/1900" : model.activationDate)));
                cmd.Parameters.Add(new SqlParameter("@deactivationDate", DateTime.Parse(String.IsNullOrEmpty(model.deactivationDate) ? "01/01/1900" : model.deactivationDate)));
                cmd.Parameters.Add(new SqlParameter("@hasResumeOnFile", model.hasResumeOnFile));
                cmd.Parameters.Add(new SqlParameter("@resumeDocumentId", String.IsNullOrEmpty(model.resumeDocumentId) ? 0 : Convert.ToInt32(model.resumeDocumentId)));
                cmd.Parameters.Add(new SqlParameter("@resumeFileName", model.resumeFileName));
                cmd.Parameters.Add(new SqlParameter("@postalCode", model.postalCode));
                cmd.Parameters.Add(new SqlParameter("@i9ExpirationDate", DateTime.Parse(String.IsNullOrEmpty(model.i9ExpirationDate) ? "01/01/1900" : model.i9ExpirationDate)));
                cmd.Parameters.Add(new SqlParameter("@isI9OnFile", model.isI9OnFile));
                cmd.Parameters.Add(new SqlParameter("@jobTitle", model.jobTitle));
                cmd.Parameters.Add(new SqlParameter("@note", model.note));
                cmd.Parameters.Add(new SqlParameter("@numericRating", model.numericRating));
                cmd.Parameters.Add(new SqlParameter("@serviceRepId", model.serviceRepId));
                cmd.Parameters.Add(new SqlParameter("@serviceRep", model.serviceRep));
                cmd.Parameters.Add(new SqlParameter("@serviceRepChatName", model.serviceRepChatName));
                cmd.Parameters.Add(new SqlParameter("@createdByServiceRepId", model.createdByServiceRepId));
                cmd.Parameters.Add(new SqlParameter("@createdByServiceRep", model.createdByServiceRep));
                cmd.Parameters.Add(new SqlParameter("@companyId", model.companyId));
                cmd.Parameters.Add(new SqlParameter("@company", model.company));
                //cmd.Parameters.Add(new SqlParameter("@alternateEmployeeId", string.IsNullOrEmpty(model.alternateEmployeeId) ? 0 : Convert.ToInt32(model.alternateEmployeeId)));
                cmd.Parameters.Add(new SqlParameter("@employerId", model.employerId));
                cmd.Parameters.Add(new SqlParameter("@employer", model.employer));
                cmd.Parameters.Add(new SqlParameter("@emailAddress", model.emailAddress));
                cmd.Parameters.Add(new SqlParameter("@driverLicenseNumber", model.driverLicenseNumber));
                cmd.Parameters.Add(new SqlParameter("@driverLicenseClass", model.driverLicenseClass));
                //cmd.Parameters.Add(new SqlParameter("@driverLicenseExpire", DateTime.Parse(String.IsNullOrEmpty(model.driverLicenseExpire) ? "01/01/1900" : model.driverLicenseExpire)?<DateTime.MinValue? DateTime.MinValue:DateTime.Parse(model.driverLicenseExpire))));
                cmd.Parameters.Add(new SqlParameter("@lastMessage", model.lastMessage));

                int add = 0;

                if (model.address != null)
                {
                    Address addy = new Address
                    {
                        Street1 = model.address.Street1,
                        Street2 = model.address.Street2,
                        PostalCode = model.address.PostalCode,
                        AttentionTo = model.address.AttentionTo,
                        Country = model.address.Country,
                        Municipality = model.address.Municipality,
                        Region = model.address.Region
                    };

                    add = DataLayer.TempWorks.AddAddressProc(con, addy);
                }
                cmd.Parameters.Add(new SqlParameter("@addressId", add));



                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "prAddEmployee";
                cmd.ExecuteNonQuery();


            }

        }
        public static void AddEmployee(DataSurfContext context, Employee model)
        {
            Models.TempWorksDB.Employee result = context.Employee.FirstOrDefault(result => result.employeeId == model.employeeId);
            bool addRec = false;
            if (result == null)
            {
                addRec = true;
                result = new Models.TempWorksDB.Employee();
            }

            result.employeeId = model.employeeId;
            result.firstName = model.firstName;
            result.lastName = model.lastName;
            result.isActive = model.isActive;
            result.isAssigned = model.isAssigned;
            result.emailAddress = model.emailAddress;
            result.postalCode = model.postalCode;
            result.phoneNumber = string.IsNullOrEmpty(model.cellPhoneNumber) ? model.phoneNumber : model.cellPhoneNumber;
            result.branchName = model.branch;
            result.hasResumeOnFile = model.hasResumeOnFile;
            result.emailAddress = model.emailAddress;
            result.governmentPersonalId = model.governmentPersonalId;



            if (addRec)
                context.Employee.Add(result);
            else
                context.Employee.Update(result);



        }

        public static void AddAssignmentProc(SqlConnection con, Assignment model)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;

                cmd.Parameters.Add(new SqlParameter("@assignmentId", model.assignmentId));
                cmd.Parameters.Add(new SqlParameter("@activeStatus", model.activeStatus));
                cmd.Parameters.Add(new SqlParameter("@assignmentStatus", model.assignmentStatus));
                cmd.Parameters.Add(new SqlParameter("@assignmentStatusId", model.assignmentStatusId));
                cmd.Parameters.Add(new SqlParameter("@billRate", Convert.ToDecimal(model.billRate)));
                cmd.Parameters.Add(new SqlParameter("@branchId", model.branchId));
                cmd.Parameters.Add(new SqlParameter("@branchName", model.branchName));
                cmd.Parameters.Add(new SqlParameter("@customerId", model.customerId));
                cmd.Parameters.Add(new SqlParameter("@customerName", model.customerName));
                cmd.Parameters.Add(new SqlParameter("@departmentName", model.departmentName));
                cmd.Parameters.Add(new SqlParameter("@employeeId", model.employeeId));
                cmd.Parameters.Add(new SqlParameter("@endDate", DateTime.Parse(String.IsNullOrEmpty(model.endDate) ? "01 /01/1900" : model.endDate)));
                cmd.Parameters.Add(new SqlParameter("@expectedEndDate", DateTime.Parse(String.IsNullOrEmpty(model.expectedEndDate) ? "01 /01/1900" : model.expectedEndDate)));
                cmd.Parameters.Add(new SqlParameter("@firstName", model.firstName));
                cmd.Parameters.Add(new SqlParameter("@isActive", model.isActive));
                cmd.Parameters.Add(new SqlParameter("@isDeleted", model.isDeleted));
                cmd.Parameters.Add(new SqlParameter("@isTimeclockOrder", model.isTimeclockOrder));
                cmd.Parameters.Add(new SqlParameter("@jobOrderId", model.jobOrderId));
                cmd.Parameters.Add(new SqlParameter("@jobTitle", model.jobTitle));
                cmd.Parameters.Add(new SqlParameter("@lastName", model.lastName));
                cmd.Parameters.Add(new SqlParameter("@middleName", model.middleName));
                cmd.Parameters.Add(new SqlParameter("@originalStartDate", DateTime.Parse(String.IsNullOrEmpty(model.originalStartDate) ? "01 /01/1900" : model.originalStartDate)));
                cmd.Parameters.Add(new SqlParameter("@payRate", Convert.ToDecimal(model.payRate)));
                cmd.Parameters.Add(new SqlParameter("@performanceNote", model.performanceNote));
                cmd.Parameters.Add(new SqlParameter("@startDate", DateTime.Parse(String.IsNullOrEmpty(model.startDate) ? "01 /01/1900" : model.startDate)));
                cmd.Parameters.Add(new SqlParameter("@supervisor", model.supervisor));
                cmd.Parameters.Add(new SqlParameter("@supervisorContactInfo", model.supervisorContactInfo));
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.CommandText = "prAddAssignment";
                int count = cmd.ExecuteNonQuery();


            }

        }

        public static bool AddAssignment(DataSurfContext context, Models.TempWorks.Assignment model)
        {
            Models.TempWorksDB.Assignment result = context.Assignment.FirstOrDefault(result => result.assignmentId == model.assignmentId);
            bool addRec = false;
            if (result == null)
            {
                addRec = true;
                result = new Models.TempWorksDB.Assignment();
            }
            result.assignmentId = model.assignmentId;
            result.activeStatus = model.activeStatus;
            result.assignmentStatus = model.assignmentStatus;
            result.assignmentStatusId = model.assignmentStatusId;
            result.billRate = Convert.ToDecimal(model.billRate);
            result.branchId = model.branchId;
            result.branchName = model.branchName;
            result.customerId = model.customerId;
            result.customerName = model.customerName;
            result.departmentName = model.departmentName;
            result.employeeId = model.employeeId;
            result.endDate = DateTime.Parse(String.IsNullOrEmpty(model.endDate) ? "01/01/1900" : model.endDate);
            result.expectedEndDate = DateTime.Parse(String.IsNullOrEmpty(model.expectedEndDate) ? "01/01/1900" : model.expectedEndDate);
            result.firstName = model.firstName;
            result.isActive = model.isActive;
            result.isDeleted = model.isDeleted;
            result.isTimeclockOrder = model.isTimeclockOrder;
            result.jobOrderId = model.jobOrderId;
            result.jobTitle = model.jobTitle;
            result.lastName = model.lastName;
            result.middleName = model.middleName;
            result.originalStartDate = DateTime.Parse(String.IsNullOrEmpty(model.originalStartDate) ? "01/01/1900" : model.originalStartDate);
            result.payRate = Convert.ToDecimal(model.payRate);
            result.performanceNote = model.performanceNote;
            result.startDate = DateTime.Parse(String.IsNullOrEmpty(model.startDate) ? "01/01/1900" : model.startDate);
            result.supervisor = model.supervisor;
            result.supervisorContactInfo = model.supervisorContactInfo;

            if (addRec)
                context.Assignment.Add(result);
            else
                context.Assignment.Update(result);

            //      context.SaveChanges();

            return true;
        }

        public static bool AddBranch(DataSurfContext context, Models.TempWorks.Branch model)
        {
            Models.TempWorksDB.Branch result = context.Branch.FirstOrDefault(result => result.BranchId == model.BranchId);
            bool addRec = false;
            if (result == null)
            {
                addRec = true;
                result = new Models.TempWorksDB.Branch();
            }

            result.BranchId = model.BranchId;
            result.BranchFullName = model.BranchFullName;
            result.BranchName = model.BranchName;
            result.PhoneNumber = model.PhoneNumber;
            result.IsActive = model.IsActive;
            result.HierId = model.HierId;
            result.Employer = model.Employer;
            result.EmployerId = model.EmployerId;
            result.EmailAddress = model.EmailAddress;
            Models.TempWorksDB.Address add = new Models.TempWorksDB.Address()
            {
                Street1 = model.Address.Street1,
                Street2 = model.Address.Street2,
                PostalCode = model.Address.PostalCode,
                Country = model.Address.Country,
                Municipality = model.Address.Municipality,
                Region = model.Address.Region,
                AttentionTo = model.Address.AttentionTo
            };

            result.AddressId = AddAddress(context, add);

            if (addRec)
                context.Branch.Add(result);
            else
                context.Branch.Update(result);

            context.SaveChanges();


            return true;
        }

        public static bool AddDocumentProc(SqlConnection con, int EmployeeId, Document model)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;

                cmd.Parameters.Add(new SqlParameter("@EmployeeId", EmployeeId));
                cmd.Parameters.Add(new SqlParameter("@DocumentId", model.documentId));
                cmd.Parameters.Add(new SqlParameter("@Description", model.description));
                cmd.Parameters.Add(new SqlParameter("@FileType", model.fileType));
                cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", model.documentTypeId));
                cmd.Parameters.Add(new SqlParameter("@DocumentType", model.documentType));
                cmd.Parameters.Add(new SqlParameter("@DocumentName", model.documentName));
                cmd.Parameters.Add(new SqlParameter("@FileName", model.fileName));
                cmd.Parameters.Add(new SqlParameter("@CanBeDeleted", model.canBeDeleted));
                cmd.Parameters.Add(new SqlParameter("@CanBedEdited", model.canBeEdited));
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.CommandText = "prAddDocument";
                int count = cmd.ExecuteNonQuery();


            }

            return false;
        }


        public static bool AddJobOrderProc(SqlConnection con, DataLayer.Models.TempWorks.JobDetail model, bool replace = false)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@override", replace == false ? 0 : 1));
                cmd.Parameters.Add(new SqlParameter("@jobOrderId", model.jobOrderId));
                cmd.Parameters.Add(new SqlParameter("@branchId", model.branchId));
                cmd.Parameters.Add(new SqlParameter("@branch", model.branch));
                cmd.Parameters.Add(new SqlParameter("@jobOrderTypeId", model.jobOrderTypeId));
                cmd.Parameters.Add(new SqlParameter("@jobOrderType", model.jobOrderType));
                cmd.Parameters.Add(new SqlParameter("@jobTitleId", model.jobTitleId));
                cmd.Parameters.Add(new SqlParameter("@jobTitle", model.jobTitle));
                cmd.Parameters.Add(new SqlParameter("@jobDescription", model.jobDescription));
                cmd.Parameters.Add(new SqlParameter("@payRate", model.payRate));
                cmd.Parameters.Add(new SqlParameter("@billRate", model.billRate));
                cmd.Parameters.Add(new SqlParameter("@jobOrderStatusId", model.jobOrderStatusId));
                cmd.Parameters.Add(new SqlParameter("@jobOrderStatus", model.jobOrderStatus));
                cmd.Parameters.Add(new SqlParameter("@isActive", model.isActive));
                cmd.Parameters.Add(new SqlParameter("@positionsRequired", model.positionsRequired));
                cmd.Parameters.Add(new SqlParameter("@positionsFilled", model.positionsFilled));
                cmd.Parameters.Add(new SqlParameter("@customerId", model.customerId));
                cmd.Parameters.Add(new SqlParameter("@customerName", model.customerName));
                cmd.Parameters.Add(new SqlParameter("@departmentName", model.departmentName));
                cmd.Parameters.Add(new SqlParameter("@jobOrderDurationId", model.jobOrderDurationId));
                cmd.Parameters.Add(new SqlParameter("@jobOrderDuration", model.jobOrderDurationId));
                cmd.Parameters.Add(new SqlParameter("@dateOrderTaken", DateTime.Parse(String.IsNullOrEmpty(model.dateOrderTaken) ? "01/01/1900" : model.dateOrderTaken)));
                cmd.Parameters.Add(new SqlParameter("@startDate", DateTime.Parse(String.IsNullOrEmpty(model.startDate) ? "01/01/1900" : model.startDate)));
                cmd.Parameters.Add(new SqlParameter("@supervisorContactId", model.supervisorContactId));
                cmd.Parameters.Add(new SqlParameter("@supervisorFirstName", model.supervisorFirstName));
                cmd.Parameters.Add(new SqlParameter("@supervisorLastName", model.supervisorLastName));
                cmd.Parameters.Add(new SqlParameter("@supervisorOfficePhoneCountryCallingCode", model.supervisorOfficePhoneCountryCallingCode));
                cmd.Parameters.Add(new SqlParameter("@supervisorOfficePhone", model.supervisorOfficePhone));
                cmd.Parameters.Add(new SqlParameter("@doNotAutoClose", model.doNotAutoClose));
                cmd.Parameters.Add(new SqlParameter("@usesTimeClock", model.usesTimeClock));
                cmd.Parameters.Add(new SqlParameter("@usesPeopleNet", model.usesPeopleNet));
                cmd.Parameters.Add(new SqlParameter("@notes", model.notes));
                cmd.Parameters.Add(new SqlParameter("@alternateJobOrderId", model.alternateJobOrderId));
                cmd.Parameters.Add(new SqlParameter("@dressCode", model.dressCode));
                cmd.Parameters.Add(new SqlParameter("@safetyNotes", model.safetyNotes));
                cmd.Parameters.Add(new SqlParameter("@directions", model.directions));
                cmd.Parameters.Add(new SqlParameter("@serviceRepId", model.serviceRepId));
                cmd.Parameters.Add(new SqlParameter("@serviceRep", model.serviceRep));
                cmd.Parameters.Add(new SqlParameter("@salesTeamId", model.salesTeamId));
                cmd.Parameters.Add(new SqlParameter("@salesTeam", model.salesTeam));
                cmd.Parameters.Add(new SqlParameter("@publicJobTitle", model.publicJobTitle));
                cmd.Parameters.Add(new SqlParameter("@publicJobDescription", model.publicJobDescription));
                cmd.Parameters.Add(new SqlParameter("@publicPostingDate", DateTime.Parse(String.IsNullOrEmpty(model.publicPostingDate) ? "01/01/1900" : model.publicPostingDate)));
                cmd.Parameters.Add(new SqlParameter("@doNotPostPublicly", model.doNotPostPublicly));
                cmd.Parameters.Add(new SqlParameter("@publicJobDescriptionContentType", model.publicJobDescriptionContentType));
                cmd.Parameters.Add(new SqlParameter("@publicEducationSummary", model.publicEducationSummary));
                cmd.Parameters.Add(new SqlParameter("@publicExperienceSummary", model.publicExperienceSummary));
                cmd.Parameters.Add(new SqlParameter("@publicReplyEmail", model.publicReplyEmail));
                cmd.Parameters.Add(new SqlParameter("@applyToCustomUrl", model.applyToCustomUrl));
                cmd.Parameters.Add(new SqlParameter("@applyToHrCenterProductInstanceId", model.applyToHrCenterProductInstanceId));
                cmd.Parameters.Add(new SqlParameter("@applyToHrCenterProductInstanceName", model.applyToHrCenterProductInstanceName));
                cmd.Parameters.Add(new SqlParameter("@applyToHrCenterWorkflowId", model.applyToHrCenterWorkflowId));
                cmd.Parameters.Add(new SqlParameter("@applyToHrCenterWorkflowName", model.applyToHrCenterWorkflowName));
                cmd.Parameters.Add(new SqlParameter("@howToApply", model.howToApply));
                cmd.Parameters.Add(new SqlParameter("@utmCampaign", model.utmCampaign));
                cmd.Parameters.Add(new SqlParameter("@payDescription", model.payDescription));
                cmd.Parameters.Add(new SqlParameter("@expireDateTime", DateTime.Parse(String.IsNullOrEmpty(model.expireDateTime) ? "01/01/1900" : model.expireDateTime)));

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "prAddJobOrder";
                cmd.ExecuteNonQuery();

            }
            return true;
        }
        public static async System.Threading.Tasks.Task<bool> AddDepartmentCustomerProc(SqlConnection con, DataLayer.Models.TempWorks.Department model, Int64 CustomerId = 0, bool replace = false)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                // get the customer details and save
                if (model == null)
                {
                    try
                    {
                        model = await TempWorksAPI.GetCustomerDepartmentFromTempworksAsync(CustomerId);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
                if (model.departments != null)
                {
                    foreach (Department dep in model.departments)
                    {
                        Customer cust = await TempWorksAPI.GetCustomerFromTempworksAsync(dep.customerId);
                        AddCustomerProc(con, cust, model.customerId, true);
                        await AddDepartmentCustomerProc(con, dep, dep.customerId, true);
                    }
                }
                return true;
            }

            return true;
        }
    }

}
