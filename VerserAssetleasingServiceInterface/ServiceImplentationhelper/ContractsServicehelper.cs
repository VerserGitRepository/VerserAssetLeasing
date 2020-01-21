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
    public class ContractsServicehelper
    {
        public static string BaseUri = ConfigurationManager.AppSettings["AssetleasingAPIBaseURL"];
        public static async Task<List<ContractsListViewModel>> Projects()
        {
            List<ContractsListViewModel> projectsList = new List<ContractsListViewModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                HttpResponseMessage response = client.GetAsync(string.Format("Contracts/ContractsList")).Result;
                if (response.IsSuccessStatusCode)
                {
                    var projects = await response.Content.ReadAsAsync<List<ContractsListViewModel>>();

                    foreach (var p in projects)
                    {
                        projectsList.Add(p);
                    }
                }
            }
            return projectsList;
        }
        public static async Task<ReturnModel> AddContract(ContractsListViewModel theModel)
        {
            List<ContractsListViewModel> contractList = new List<ContractsListViewModel>();
            HttpResponseMessage response = new HttpResponseMessage();
            ReturnModel ReturnResult = new ReturnModel();
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(BaseUri);
                    response = client.PostAsJsonAsync(string.Format("Contracts/CreateNewContract"), theModel).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        ReturnResult = await response.Content.ReadAsAsync<ReturnModel>();
                        if (ReturnResult.Message == null || ReturnResult.Message == string.Empty)
                        {
                            ReturnResult.Message = "User has been registered successfully.";
                        }
                    }
                    else
                    {
                        ReturnResult.Message = "There is an issue with registration. The detail are " + response.ReasonPhrase;
                    }
                }
                catch (Exception ex)
                {
                    ReturnResult.Message = "There is an issue with registration. The detail are " + ex.Message;
                }
            }
            return ReturnResult;
        }
        
        public static async Task<ReturnModel> UpdateContract(ContractsListViewModel theModel)
        {
            List<ContractsListViewModel> contractList = new List<ContractsListViewModel>();
            HttpResponseMessage response = new HttpResponseMessage();
            ReturnModel ReturnResult = new ReturnModel();
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(BaseUri);
                    response = client.PostAsJsonAsync(string.Format("Contracts/UpdateContract"), theModel).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        ReturnResult = await response.Content.ReadAsAsync<ReturnModel>();
                        if (ReturnResult.Message == null || ReturnResult.Message == string.Empty)
                        {
                            ReturnResult.Message = "User has been registered successfully.";

                        }

                    }
                    else
                    {
                        ReturnResult.Message = "There is an issue with registration. The detail are " + response.ReasonPhrase;

                    }
                }
                catch (Exception ex)
                {
                    ReturnResult.Message = "There is an issue with registration. The detail are " + ex.Message;
                }
            }
            return ReturnResult;
        }
    }
}