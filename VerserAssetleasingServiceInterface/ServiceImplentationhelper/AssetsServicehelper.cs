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
    public class AssetsServicehelper
    {
        public static string BaseUri = ConfigurationManager.AppSettings["AssetleasingAPIBaseURL"];

        public static async Task<List<AssetsListViewModel>> AssetsList()
        {
            List<AssetsListViewModel> projectsList = new List<AssetsListViewModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                HttpResponseMessage response = client.GetAsync(string.Format("assets/AssetsList")).Result;
                if (response.IsSuccessStatusCode)
                {
                    var projects = await response.Content.ReadAsAsync<List<AssetsListViewModel>>();
                    foreach (var p in projects)
                    {
                        projectsList.Add(p);
                    }
                }
            }
            return projectsList;
        }
        public static async Task<ReturnModel> AddAsset(AssetsListViewModel theModel)
        {
            List<AssetsListViewModel> assetssList = new List<AssetsListViewModel>();
            HttpResponseMessage response = new HttpResponseMessage();
            ReturnModel ReturnResult = new ReturnModel();
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(BaseUri);
                    response = client.PostAsJsonAsync(string.Format("Assets/CreateNewAsset"), theModel).Result;
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
        public static async Task<List<AssetsListViewModel>> GetAssetsData(int id)
        {
            List<AssetsListViewModel> assetsList = new List<AssetsListViewModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                HttpResponseMessage response = client.GetAsync(string.Format("Assets/{0}/CompanyAssets", id)).Result;
                if (response.IsSuccessStatusCode)
                {
                    assetsList = await response.Content.ReadAsAsync<List<AssetsListViewModel>>();
                }
            }
            return assetsList;
        }
        public static async Task<ReturnModel> UpdateAsset(AssetsListViewModel theModel)
        {
            List<AssetsListViewModel> assetssList = new List<AssetsListViewModel>();
            HttpResponseMessage response = new HttpResponseMessage();
            ReturnModel ReturnResult = new ReturnModel();
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(BaseUri);
                    response = client.PostAsJsonAsync(string.Format("Assets/UpdateAsset"), theModel).Result;
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