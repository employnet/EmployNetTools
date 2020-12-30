﻿using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using EmployNetTools.DataLayer.Models;
using EmployNetTools.DataLayer.Models.TempWorks;

namespace EmployNetTools.DataLayer
{
    public class TempWorks
    {
 
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
            result.DateActivated = model.DateActivated.LocalDateTime;
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
            



            if(addRec)
                context.Customer.Add(result);
            else
                context.Customer.Update(result);

            context.SaveChanges();


            return true;

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
            result.expectedEndDate = DateTime.Parse(String.IsNullOrEmpty(model.expectedEndDate)?"01/01/1900":model.expectedEndDate);
            result.firstName = model.firstName;
            result.isActive = model.isActive;
            result.isDeleted = model.isDeleted;
            result.isTimeclockOrder = model.isTimeclockOrder;
            result.jobOrderId = model.jobOrderId;
            result.jobTitle = model.jobTitle;
            result.lastName = model.lastName;
            result.middleName = model.middleName;
            result.originalStartDate = DateTime.Parse(String.IsNullOrEmpty(model.originalStartDate)?"01/01/1900":model.originalStartDate);
            result.payRate = Convert.ToDecimal(model.payRate);
            result.performanceNote = model.performanceNote;
            result.startDate = DateTime.Parse(String.IsNullOrEmpty(model.startDate)?"01/01/1900":model.startDate);
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
            if(result==null)
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

            result.AddressId = AddAddress(context,add);

            if (addRec)
                context.Branch.Add(result);
            else
                context.Branch.Update(result);

            context.SaveChanges();


            return true;
        }
    }
}