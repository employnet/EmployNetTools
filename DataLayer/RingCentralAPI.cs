using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using RingCentral.Paths.Restapi;
using RingCentral;
using System.Threading.Tasks;

namespace EmployNetTools.DataLayer
{


    public class RingCentralAPI
    {
        private string clientId = "powrtn1KT-SI4e_l5u1y3g";
        private string clientSecret = "Y1OVPRlxRWympswEd7QJxw48nBzHEsTAiLfMbmCPfKuQ";
        private string userName = "+14242994716"; 
        private string password = "PA-38Tomahawk";
        private string accountId = "~";


        private async Task<RestClient> GetClient()
        {
            RestClient rc = new RestClient(clientId, clientSecret, false);
            await rc.Authorize(userName, "", password);
            return rc;
        }

        public async Task<string> GetAccount()
        {

            RestClient client = await GetClient();

            var r = await client.Restapi().Account(accountId).Get();
            

            return r.ToString();
        }

        public async Task<RingCentral.UserCallLogRecord> GetCallLogs()
        {
            try
            {
                RestClient client = await GetClient();
                var parameters = new ReadUserCallLogParameters();
                parameters.view = "Detailed";

                var resp = await client.Restapi().Account().Extension().CallLog().List(parameters);
                foreach (RingCentral.UserCallLogRecord record in resp.records)
                {
                    Console.WriteLine("Call type: " + record.type);
                }
                var resp2 = await client.Restapi().Account().Extension().MessageStore().List();
                //var resp3 = await client.Restapi().Account().Presence();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<string> GetContacts()
        {

            SearchDirectoryEntriesRequest searchDirectoryEntriesRequest = new SearchDirectoryEntriesRequest
            {
                searchString = "joe",
                showFederated = true,
                extensionType = "User",
                orderBy = new[] {
                new OrderBy {
                    index = 1,
                    fieldName = "firstName",
                    direction = "Asc"
                },
            },
                page = 1,
                perPage = 10
            };
            RestClient rc = await GetClient();
            var r = await rc.Restapi().Account(accountId).Directory().Entries().Search().Post(searchDirectoryEntriesRequest);

            return null;
        }

        public async Task<string> ReadMessages()
        {
            RestClient rc = await GetClient();

            string extensionId = "0";

            // OPTIONAL QUERY PARAMETERS
            ListMessagesParameters listMessagesParameters = new ListMessagesParameters
            {
                //availability = new[] { "Alive", "Deleted", "Purged" },
                //conversationId = 000,
                //dateFrom = "<ENTER VALUE>",
                //dateTo = "<ENTER VALUE>",
                //direction = new[] { "Inbound", "Outbound" },
                //distinctConversations = true,
                //messageType = new[] { "Fax", "SMS", "VoiceMail", "Pager", "Text" },
                //readStatus = new[] { "Read", "Unread" },
                //page = 1,
                //perPage = 100,
                //phoneNumber = "<ENTER VALUE>"
            };

            var r = await rc.Restapi().Account(accountId).Extension(extensionId).MessageStore().List(listMessagesParameters);
            // PROCESS RESPONSE


            return null;
        }

        public async Task<string> GetGlipPerson(string id)
        {
            string personId = id;

            RestClient rc = await GetClient();
            var r = await rc.Restapi().Glip().Persons(personId).Get();
            return null;
        }
        
    }
}
