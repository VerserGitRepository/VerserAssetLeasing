using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using VerserAssetleasingServiceInterface.Models;

namespace VerserAssetleasingServiceInterface.ServiceImplentationhelper
{
    public class CostModelServicesHelpers
    {
        public static bool CreateSalesForceOpportunity(SalesForceOpportunity opportunity, out string opportunityNumber)
        {
            opportunityNumber = "";
            Opportunity opn = new Opportunity
            {
                Opportunity_Number__c = opportunity.OpportunityNumber,
                Name = opportunity.OpportunityName,
                Project_Start_Date__c = opportunity.StartDate,
                CloseDate = opportunity.StartDate.Date,
                CloseDateSpecified = true,
                //Customer_Type__c = opportunity.CustomerContactName,
                Approver__c = opportunity.Approver,
                StageName = "Building Solution",
                Referred_By__c = opportunity.CustomerContactName,
                //Account_Manager__c = opportunity.SalesManager,
                Branch__c = opportunity.VerserBranch
            };

            var SfdcBinding = new SforceService();
            LoginResult CurrentLoginResult = null;
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            CurrentLoginResult = SfdcBinding.login(salesForceUser, salesForcePWD);
            SfdcBinding.Url = CurrentLoginResult.serverUrl;
            SfdcBinding.SessionHeaderValue = new SessionHeader();
            SfdcBinding.SessionHeaderValue.sessionId = CurrentLoginResult.sessionId;
            QueryResult queryResult = null;
            //String SOQL = "";
            //SOQL = salesForceQuery;
            // "select OwnerId,Name,Opportunity_Number__c from Opportunity where closedate > 2019-01-01 and stageName in('Approved - Pending to be sent to customer', 'Pending Customer Decision', 'Pending PM Allocation', 'Closed', 'Closed Won')";

            //queryResult = SfdcBinding.query(SOQL);
            // StreamWriter info = new StreamWriter("C:\\temp\\salesforce.txt");

            // Create Query
            //var addPhoneData = new SalesForceOpportunity { Email__c = "john@gmail.com", FirstName__c = "John", LastName__c = "Doe", Phone__c = 9876543210 };
            //SfdcBinding.update();
            SaveResult[] createResults = SfdcBinding.create(new sObject[] { opn });
            if (createResults[0].success)
            {
                string query = "select Opportunity_Number__c from Opportunity where id = '" + createResults[0].id + "'";
                queryResult = SfdcBinding.query(query);
                // StreamWriter info = new StreamWriter("C:\\temp\\salesforce.txt");
                if (queryResult.size > 0)
                {

                    Opportunity lead = (Opportunity)queryResult.records[0];
                    opportunityNumber = lead.Opportunity_Number__c;


                }
                return true;
            }
            else
            {
                string result = createResults[0].errors[0].message;
                opportunityNumber = "0";
                return false;
            }
            //SfdcBinding.createCompleted += new createCompletedEventHandler(CreationDone);// (new sObject[] { opn });           
            //Console.WriteLine("Successfully added the record.");
        }

        public static async Task<List<ListItems>> GetCostModelServices()
        {
            List<ListItems> returnmodel = new List<ListItems>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://versergateway.com.au/TimesheetCostModelServicesDev/");
                HttpResponseMessage response = client.GetAsync(string.Format("JBHiFICostModelServices/JBHIFIServiceList")).Result;
                if (response.IsSuccessStatusCode)
                {
                    returnmodel = await response.Content.ReadAsAsync<List<ListItems>>();
                }
            }
            return returnmodel;
        }
        public static async Task<List<ListItems>> GetCostModelServiceCategories()
        {
            List<ListItems> returnmodel = new List<ListItems>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://versergateway.com.au/TimesheetCostModelServicesDev/");
                HttpResponseMessage response = client.GetAsync(string.Format("JBHiFICostModelServices/CostModelServiceCategories")).Result;
                if (response.IsSuccessStatusCode)
                {
                    returnmodel = await response.Content.ReadAsAsync<List<ListItems>>();
                }
            }
            return returnmodel;
        }
        public static async Task<CostModelRateCard> GetPrice(int value1, int value2)
        {
            CostModelRateCard returnmodel = new CostModelRateCard();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://versergateway.com.au/TimesheetCostModelServicesDev/");
                HttpResponseMessage response = client.GetAsync(string.Format("JBHiFICostModelServices/ServiceCostPerUnitTotal/{0}/{1}", value1, value2)).Result;
                if (response.IsSuccessStatusCode)
                {
                    returnmodel = await response.Content.ReadAsAsync<CostModelRateCard>();
                }
            }
            return returnmodel;
        }
    }
}