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
    public class EndUsersServicehelper
    {
        public static string BaseUri = ConfigurationManager.AppSettings["AssetleasingAPIBaseURL"];

        public static async Task<List<EndUsersListViewModel>> EndUsersList()
        {
            var EndUserList = new List<EndUsersListViewModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                HttpResponseMessage response = client.GetAsync(string.Format("AssetLeasing/EndUsersList")).Result;
                if (response.IsSuccessStatusCode)
                {
                    var projects = await response.Content.ReadAsAsync<List<EndUsersListViewModel>>();
                    foreach (var p in projects)
                    {
                        EndUserList.Add(p);
                    }
                }
            }
            return EndUserList;
        }
        public static async Task<ReturnModel> AddEndUser(EndUsersListViewModel theModel)
        {
            List<EndUsersListViewModel> projectsList = new List<EndUsersListViewModel>();
            HttpResponseMessage response = new HttpResponseMessage();
            ReturnModel ReturnResult = new ReturnModel();
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(BaseUri);
                    response = client.PostAsJsonAsync(string.Format("AssetLeasing/AddNewEndUser"), theModel).Result;
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
        public static async Task<ReturnModel> UpdateEnduser(EndUsersListViewModel theModel)
        {
            List<EndUsersListViewModel> contractList = new List<EndUsersListViewModel>();
            HttpResponseMessage response = new HttpResponseMessage();
            ReturnModel ReturnResult = new ReturnModel();
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(BaseUri);
                    response = client.PostAsJsonAsync(string.Format("AssetLeasing/UpdateEndUser"), theModel).Result;
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