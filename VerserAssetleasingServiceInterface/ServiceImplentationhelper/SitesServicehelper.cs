using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Net.Http;
using Microsoft.AspNet; 
using VerserAssetleasingServiceInterface.Models;
using Newtonsoft.Json;

namespace VerserAssetleasingServiceInterface.ServiceImplentationhelper
{
    public class SitesServicehelper
    {
    public static string BaseUri = ConfigurationManager.AppSettings["AssetleasingAPIBaseURL"];

       
        public static async Task<ReturnModel> AddSiteInformation(CompanySitesListViewModel theModel)
        {
            ReturnModel ReturnResult = new ReturnModel();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                HttpResponseMessage response = client.PostAsJsonAsync(string.Format("Sites/AddSite"), theModel).Result;
                if (response.IsSuccessStatusCode)
                {
                    ReturnResult = await response.Content.ReadAsAsync<ReturnModel>();
                    HttpContext.Current.Session["ResultMessage"] = ReturnResult.Message;
                }
                else
                {
                    HttpContext.Current.Session["ErrorMessage"] = ReturnResult.Message;
                }
            }
            return ReturnResult;
        }
        
        public static async Task<ReturnModel> UpdateSiteInformation(CompanySitesListViewModel theModel)
        {
            ReturnModel ReturnResult = new ReturnModel();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                HttpResponseMessage response = client.PostAsJsonAsync(string.Format("Sites/UpdateSite"), theModel).Result;
                if (response.IsSuccessStatusCode)
                {
                    ReturnResult = await response.Content.ReadAsAsync<ReturnModel>();
                    HttpContext.Current.Session["ResultMessage"] = ReturnResult.Message;
}
                else
                {
                    HttpContext.Current.Session["ErrorMessage"] = ReturnResult.Message;
                }
            }
            return ReturnResult;
        }
    }
}