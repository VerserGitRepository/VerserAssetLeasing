using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Net.Http;
using VerserAssetleasingServiceInterface.Models;
using System.Linq;

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
            theModel.Asset_Contract =1;
            theModel.Asset_EndUser = 1;
            theModel.Asset_InventoryItem = 1;
            theModel.Asset_LifecycleStatus = 1;
            theModel.Asset_OSVersion = 1;

            HttpResponseMessage response = new HttpResponseMessage();
            var _response = false;
            ReturnModel ReturnResult = new ReturnModel();
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(BaseUri);
                    response = client.PostAsJsonAsync(string.Format("Assets/CreateNewAsset"), theModel).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        _response = await response.Content.ReadAsAsync<bool>();                        
                    }
                    if (_response != false)
                    {
                        ReturnResult.Message = "User has been registered successfully.";
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
        public static async Task<List<JBHiFiAssetsModel>> GetAssetsData(string ProjectID)
        {
            var assetsList = new List<JBHiFiAssetsModel>();

            if (ProjectID.All(char.IsDigit))
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    HttpResponseMessage response = client.GetAsync(string.Format($"AssetLeasing/{ProjectID}/ProjectAssets")).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        assetsList = await response.Content.ReadAsAsync<List<JBHiFiAssetsModel>>();
                    }
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
                    response = client.PostAsJsonAsync(string.Format("AssetLeasing/UpdateAsset"), theModel).Result;
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