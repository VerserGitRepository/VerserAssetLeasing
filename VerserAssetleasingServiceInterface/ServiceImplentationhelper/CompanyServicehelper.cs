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
    public class CompanyServicehelper
    {
    public static string BaseUri = ConfigurationManager.AppSettings["AssetleasingAPIBaseURL"];

        public static async Task<List<CompanyListViewModel>> Projects(int CompanyID)
        {            List<CompanyListViewModel> projectsList = new List<CompanyListViewModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                HttpResponseMessage response = client.GetAsync(string.Format($"Company/{CompanyID}/CompanyList")).Result;
                if (response.IsSuccessStatusCode)
                {
                    var projects = await response.Content.ReadAsAsync<List<CompanyListViewModel>>();                  

                    foreach (var p in projects)
                    {
                        projectsList.Add(p);
                    }
                }
            }
            return projectsList;
        }
        public static async Task<ReturnModel> CompanyAdd(CompanyEntryModel CompanyEntryRecord)
        {
            ReturnModel ReturnResult = new ReturnModel();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                HttpResponseMessage response = client.PostAsJsonAsync(string.Format("Company/Create"), CompanyEntryRecord).Result;
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
        public static async Task<List<CompanySitesListViewModel>> CompanySites(int CompanyID)
        {
            List<CompanySitesListViewModel> companyList = new List<CompanySitesListViewModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                HttpResponseMessage response = client.GetAsync(string.Format($"MasteData/{CompanyID}/CompanySites")).Result;
                if (response.IsSuccessStatusCode)
                {
                    companyList = await response.Content.ReadAsAsync<List<CompanySitesListViewModel>>();
                }
            }
            return companyList;
        }
        public static async Task<List<CompanySitesListViewModel>> GetCompanyData(int id)
        {
            List<CompanySitesListViewModel> companyList = new List<CompanySitesListViewModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                HttpResponseMessage response = client.GetAsync(string.Format("MasteData/{0}/CompanySitesById",id)).Result;
                if (response.IsSuccessStatusCode)
                {
                    companyList = await response.Content.ReadAsAsync<List<CompanySitesListViewModel>>();

                    
                }
            }
            return companyList;
        }
        public static async Task<ReturnModel> UpdateCompany(CompanyListViewModel theModel)
        {
            ReturnModel ReturnResult = new ReturnModel();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                HttpResponseMessage response = client.PostAsJsonAsync(string.Format("Company/UpdateCompany"), theModel).Result;
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