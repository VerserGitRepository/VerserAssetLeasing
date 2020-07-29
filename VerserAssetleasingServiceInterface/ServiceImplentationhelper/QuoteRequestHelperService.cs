using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using VerserAssetleasingServiceInterface.Models;
using VerserAssetleasingServiceInterface.VerserSalesForce;

namespace VerserAssetleasingServiceInterface.ServiceImplentationhelper
{
    public class QuoteRequestHelperService
    {
        private static readonly string CostModelAPIBase = ConfigurationManager.AppSettings["CostModelAPIBase"] + ConfigurationManager.AppSettings["CostModelAPIRoot"];
        private static readonly string salesForceUser = ConfigurationManager.AppSettings["salesForceUser"];
        private static readonly string salesForcePWD = ConfigurationManager.AppSettings["salesForcePWD"];
        public static async Task<PostQuoteReturnModel> PostQuoteRequest(PostQuoteRequestModel model)
        {
            string SerialisedPostModel = JsonConvert.SerializeObject(model);

            var returnmodel = new PostQuoteReturnModel();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(CostModelAPIBase);
                HttpResponseMessage response = client.PostAsJsonAsync(string.Format("JBHiFICostModelServices/RequestCostModelServiceQuote"), model).Result;
                if (response.IsSuccessStatusCode)
                {
                    returnmodel = await response.Content.ReadAsAsync<PostQuoteReturnModel>();
                }
            }
            return returnmodel;
        }

        public static async Task<List<PostQuoteRequestModel>> GetTQuotes()
        {
            var ResponseListModel = new List<PostQuoteRequestModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(CostModelAPIBase);
                HttpResponseMessage response = client.GetAsync(string.Format("JBHiFICostModelServices/JBHIFiCostModelQuoteRequestList")).Result;
                if (response.IsSuccessStatusCode)
                {
                    ResponseListModel = await response.Content.ReadAsAsync<List<PostQuoteRequestModel>>();
                }
            }
            return ResponseListModel;
        }
        public static async Task<ReturnModel> AddQuoteServiceItems(JBHIFiCostModelServiceItemsSummary RequestQuoteModel)
        {
            UpdateSalesForceData(RequestQuoteModel);
            var ResponseListModel = new ReturnModel();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(CostModelAPIBase);
                HttpResponseMessage response = client.PostAsJsonAsync(string.Format("JBHiFICostModelServices/AddQuoteServiceItems"), RequestQuoteModel).Result;
                if (response.IsSuccessStatusCode)
                {
                    ResponseListModel = await response.Content.ReadAsAsync<ReturnModel>();
                }
            }
            return ResponseListModel;
        }
        public static async Task<ReturnModel> SubmitChangedQuote(JBHIFiCostModelServiceItemsSummary RequestQuoteModel)
        {          
            var ResponseListModel = new ReturnModel();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(CostModelAPIBase);
                HttpResponseMessage response = client.PostAsJsonAsync(string.Format("JBHiFICostModelServices/UpdateQuoteServiceItems"), RequestQuoteModel).Result;
                if (response.IsSuccessStatusCode)
                {
                    ResponseListModel = await response.Content.ReadAsAsync<ReturnModel>();
                }
            }
            return ResponseListModel;
        }

        public static async Task<JBHiFiCostmodelServiceRequestDetailsModel> JBHiFiCostmodelServiceRequestDetails(int id)
        {
            var ResponseListModel = new JBHiFiCostmodelServiceRequestDetailsModel();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(CostModelAPIBase);
                HttpResponseMessage response = client.GetAsync(string.Format("JBHiFICostModelServices/JBHIFiCostModelQuoteRequestDetails/{0}", id)).Result;
                if (response.IsSuccessStatusCode)
                {

                     ResponseListModel = await response.Content.ReadAsAsync<JBHiFiCostmodelServiceRequestDetailsModel>();

                }
            }
            return ResponseListModel;
        }
        private static bool UpdateSalesForceData(JBHIFiCostModelServiceItemsSummary RequestQuoteModel)
        {
            Opportunity opn = new Opportunity
            {
                Opportunity_Number__c = RequestQuoteModel.OpportunityId,
                Service_Description__c = RequestQuoteModel.Summary,
                Id = RequestQuoteModel.SalesForceUniqueId,
                Amount = (double?) RequestQuoteModel.TOTAL_Incl_GST,
                //Account_Manager__c = opportunity.SalesManager,

            };

            var SfdcBinding = new SforceService();
            LoginResult CurrentLoginResult = null;
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            CurrentLoginResult = SfdcBinding.login(salesForceUser, salesForcePWD);
            SfdcBinding.Url = CurrentLoginResult.serverUrl;
            SfdcBinding.SessionHeaderValue = new SessionHeader();
            SfdcBinding.SessionHeaderValue.sessionId = CurrentLoginResult.sessionId;            
            SaveResult[] createResults = SfdcBinding.update(new sObject[] { opn });
            if (createResults[0].success)
            {
                //salesForceUniqueId = createResults[0].id;

                return true;
            }
            else
            {

                return false;
            }
        }
    }
}
