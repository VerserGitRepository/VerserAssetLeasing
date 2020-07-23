using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using VerserAssetleasingServiceInterface.Models;

namespace VerserAssetleasingServiceInterface.ServiceImplentationhelper
{
    public class QuoteRequestHelperService
    {
        private static readonly string CostModelAPIBase = ConfigurationManager.AppSettings["CostModelAPIBase"] + ConfigurationManager.AppSettings["CostModelAPIRoot"];
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
        public static async Task<ReturnModel> AddQuoteServiceItems(List<JBHIFiCostModelServiceItems> RequestQuoteModel)
        {
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


        public static async Task<JBHiFiCostmodelServiceRequestDetailsModel> JBHiFiCostmodelServiceRequestDetails(int id)
        {
            var ResponseListModel = new JBHiFiCostmodelServiceRequestDetailsModel();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(CostModelAPIBase);
                HttpResponseMessage response = client.GetAsync(string.Format("JBHiFICostModelServices/JBHIFiCostModelQuoteRequestDetails/{0}", id)).Result;
                if (response.IsSuccessStatusCode)
                {
                    //var opp = await response.Content.ReadAsAsync<List<JBHiFiCostmodelServiceRequestDetailsModel>>();
                    //foreach (var a in opp)
                    //{
                    //    ResponseListModel.Add(a);
                    //}
                    try
                    {
                        ResponseListModel = await response.Content.ReadAsAsync<JBHiFiCostmodelServiceRequestDetailsModel>();
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex);
                    }

                }
            }
            return ResponseListModel;
        }
    }
}
