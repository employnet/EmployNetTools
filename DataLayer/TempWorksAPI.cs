using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using EmployNetTools.DataLayer.Models.TempWorks;



namespace EmployNetTools.DataLayer
{
    public class TempWorksAPI
    {
        const string APIKey = "ZWU2Yzk2ODU0ZmYyNGRlMDgwMWMxOTFjNjM0Njg5YjI6YzU4N2ZhM2NhNDdmNGEyY2IxOGEyNDdjNTAwMzI5ZDI=";

        public static async System.Threading.Tasks.Task<EEO> GetEmployeeEEOFromTempworksAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                // Call asynchronous network methods in a try/catch block to handle exceptions
                client.DefaultRequestHeaders.Add("x-tw-token", APIKey);
                var result = await client.GetAsync("https://api.ontempworks.com/Employees/"+ id.ToString() + "/eeo");

                EEO custs = await result.Content.ReadFromJsonAsync<EEO>();

                return custs;
            }

        }

        public static async System.Threading.Tasks.Task<Customers> SearchCustomersFromTempworksAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                // Call asynchronous network methods in a try/catch block to handle exceptions
                client.DefaultRequestHeaders.Add("x-tw-token", APIKey);
                var result = await client.GetAsync("https://api.ontempworks.com/search/customers/?skip=0&take=100000&includeDepartments=true");

                Customers custs = await result.Content.ReadFromJsonAsync<Customers>();


                return custs;
            }

        }

        public static async System.Threading.Tasks.Task<Assignments> SearchAssignmentFromTempworksAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                // Call asynchronous network methods in a try/catch block to handle exceptions
                client.DefaultRequestHeaders.Add("x-tw-token", APIKey);
                var result = await client.GetAsync("https://api.ontempworks.com/search/assignments/?skip=0&take=100000&IsActive=true");

                //if(result.StatusCode!=System.Net.HttpStatusCode.OK)
                //throw new Exception(result.Content.)
                //string contnet = await result.Content.ReadAsStringAsync();
                Assignments ret = await result.Content.ReadFromJsonAsync<Assignments>();


                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);

                return ret;
            }

        }
        public static async System.Threading.Tasks.Task<JobOrders> SearchJobOrdersFromTempworksAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                // Call asynchronous network methods in a try/catch block to handle exceptions
                client.DefaultRequestHeaders.Add("x-tw-token", APIKey);
                var result = await client.GetAsync("https://api.ontempworks.com/search/joborders/?skip=0&take=100000");

                //if(result.StatusCode!=System.Net.HttpStatusCode.OK)
                //throw new Exception(result.Content.)
                //string contnet = await result.Content.ReadAsStringAsync();
                JobOrders jobs = await result.Content.ReadFromJsonAsync<JobOrders>();


                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);

                return jobs;
            }

        }

        public static async System.Threading.Tasks.Task<JobDetail> GetJobOrderDetailFromTempworksAsync(Int64 id)
        {
            using (HttpClient client = new HttpClient())
            {
                // Call asynchronous network methods in a try/catch block to handle exceptions
                client.DefaultRequestHeaders.Add("x-tw-token", APIKey);
                HttpResponseMessage result;
                try
                {
                    result = await client.GetAsync("https://api.ontempworks.com/JobOrders/" + id.ToString());

                    // we are testing for their API limits error report
                    if (result.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                    {
                        Thread.Sleep(6000);
                        result = await client.GetAsync("https://api.ontempworks.com/JobOrders/" + id.ToString());
                    }
                }
                catch(Exception ex)
                {
                    if (ex.Message.Contains("429"))
                    {
                        Thread.Sleep(6000);
                        result = await client.GetAsync("https://api.ontempworks.com/JobOrders/" + id.ToString());

                    }
                    else throw ex;
                }
                //if(result.StatusCode!=System.Net.HttpStatusCode.OK)
                //throw new Exception(result.Content.)
                //string contnet = await result.Content.ReadAsStringAsync();
                JobDetail job = await result.Content.ReadFromJsonAsync<JobDetail>();


                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);

                return job;
            }

        }

        public static async System.Threading.Tasks.Task<Department> GetCustomerDepartmentFromTempworksAsync(Int64 id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("x-tw-token", APIKey);
                var result = await client.GetAsync("https://api.ontempworks.com/customers/" + id.ToString() + "/departments?limittodirectdepartments=false&includeinactivedepartments=true");


                Department dep = await result.Content.ReadFromJsonAsync<Department>();


                return dep;
            }


        }


        public static async System.Threading.Tasks.Task<Customer> GetCustomerFromTempworksAsync(Int64 id)
        {
            using (HttpClient client = new HttpClient())
            {
                // Call asynchronous network methods in a try/catch block to handle exceptions
                client.DefaultRequestHeaders.Add("x-tw-token", APIKey);
                var result = await client.GetAsync("https://api.ontempworks.com/customers/" + id.ToString());

                //if(result.StatusCode!=System.Net.HttpStatusCode.OK)
                //throw new Exception(result.Content.)
                //string contnet = await result.Content.ReadAsStringAsync();
                Customer cust = await result.Content.ReadFromJsonAsync<Customer>();


                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);

                return cust;
            }

        }



        public static async System.Threading.Tasks.Task<Employee> GetEmployeeFromTempworksAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                // Call asynchronous network methods in a try/catch block to handle exceptions
                    client.DefaultRequestHeaders.Add("x-tw-token", APIKey);
                    var result = await client.GetAsync("https://api.ontempworks.com/employees/" + id.ToString());

                //if(result.StatusCode!=System.Net.HttpStatusCode.OK)
                    //throw new Exception(result.Content.)
                    //string contnet = await result.Content.ReadAsStringAsync();
                    Employee emp = await result.Content.ReadFromJsonAsync<Employee>() ;

                    
                    // Above three lines can be replaced with new helper method below
                    // string responseBody = await client.GetStringAsync(uri);

                    return emp;
            }

        }

        public static async System.Threading.Tasks.Task<Documents> GetEmployeeDocsFromTempworksAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                // Call asynchronous network methods in a try/catch block to handle exceptions
                client.DefaultRequestHeaders.Add("x-tw-token", APIKey);
                var result = await client.GetAsync("https://api.ontempworks.com/employees/" + id.ToString()+"/documents?take=1000&skip=0");

                if (result.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception("Employee not found");
                    //string contnet = await result.Content.ReadAsStringAsync();
                }
                Documents docs = await result.Content.ReadFromJsonAsync<Documents>();


                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);

                return docs;
            }

        }

        public static async System.Threading.Tasks.Task<User> GetUserFromTempworksAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                // Call asynchronous network methods in a try/catch block to handle exceptions
                client.DefaultRequestHeaders.Add("x-tw-token", APIKey);
                var result = await client.GetAsync("https://api.ontempworks.com/users/" + id.ToString());

                //if(result.StatusCode!=System.Net.HttpStatusCode.OK)
                //throw new Exception(result.Content.)
                //string contnet = await result.Content.ReadAsStringAsync();
                User user = await result.Content.ReadFromJsonAsync<User>();


                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);

                return user;
            }

        }

        public static async System.Threading.Tasks.Task<WorkSite> GetWorksiteFromTempworksAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                // Call asynchronous network methods in a try/catch block to handle exceptions
                client.DefaultRequestHeaders.Add("x-tw-token", APIKey);
                var result = await client.GetAsync("https://api.ontempworks.com/JobOrders/" + id.ToString() + "/Worksite");

                //if(result.StatusCode!=System.Net.HttpStatusCode.OK)
                //throw new Exception(result.Content.)
                //string contnet = await result.Content.ReadAsStringAsync();
                WorkSite ret = await result.Content.ReadFromJsonAsync<WorkSite>();


                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);

                return ret;
            }

        }

    }
}
