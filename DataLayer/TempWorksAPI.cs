using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using EmployNetTools.DataLayer.Models.TempWorks;



namespace DataLayer
{
    public class TempWorksAPI
    {

        public static async System.Threading.Tasks.Task<Customer> GetCustomerFromTempworksAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                // Call asynchronous network methods in a try/catch block to handle exceptions
                client.DefaultRequestHeaders.Add("x-tw-token", "OTZmZGNkOWNmZWJlNGI4MDhlYzAxOGNmMzYyYmZiNWE6M2Q2MWUyNDJlYzUwNDI1Njk1OGZlZTNhOTQyNzE5Mjg=");
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
                    client.DefaultRequestHeaders.Add("x-tw-token", "OTZmZGNkOWNmZWJlNGI4MDhlYzAxOGNmMzYyYmZiNWE6M2Q2MWUyNDJlYzUwNDI1Njk1OGZlZTNhOTQyNzE5Mjg=");
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
                client.DefaultRequestHeaders.Add("x-tw-token", "OTZmZGNkOWNmZWJlNGI4MDhlYzAxOGNmMzYyYmZiNWE6M2Q2MWUyNDJlYzUwNDI1Njk1OGZlZTNhOTQyNzE5Mjg=");
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


    }
}
