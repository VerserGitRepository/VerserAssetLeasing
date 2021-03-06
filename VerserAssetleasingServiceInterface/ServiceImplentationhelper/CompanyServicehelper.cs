﻿using System;
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

        public static async Task<List<CompanyListViewModel>> Projects(string _user)
        {            List<CompanyListViewModel> projectsList = new List<CompanyListViewModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                HttpResponseMessage response = client.GetAsync(string.Format($"AssetLeasing/{_user}/CompanyList")).Result;
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

        public static async Task<List<ListItemViewModel>> CompanyList()
        {
            var companies = new List<ListItemViewModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                HttpResponseMessage response = client.GetAsync(string.Format("inventorycontrol/projects/listitems")).Result;
                if (response.IsSuccessStatusCode)
                {
                    var projects = await response.Content.ReadAsAsync<List<ListItemViewModel>>();

                    foreach (var p in projects)
                    {
                        companies.Add(new ListItemViewModel() { Id = p.Id, Value = p.Value });
                    }
                }
            }            
            return companies;
        }
        public static async Task<List<AssetsListViewModel>> Assets()
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

        public static async Task<List<EndUsersListViewModel>> EndUsers()
        {
            List<EndUsersListViewModel> projectsList = new List<EndUsersListViewModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                HttpResponseMessage response = client.GetAsync(string.Format("EndUsers/EndUsersList")).Result;
                if (response.IsSuccessStatusCode)
                {
                    var projects = await response.Content.ReadAsAsync<List<EndUsersListViewModel>>();

                    foreach (var p in projects)
                    {
                        projectsList.Add(p);
                    }
                }
            }
            return projectsList;
        }
        public static async Task<List<ContractsListViewModel>> Contracts()
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

        public static async Task<List<EndUsersListViewModel>> EndUsersByCompany(string userId)
        {
           var EndUsers = new List<EndUsersListViewModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                HttpResponseMessage response = client.GetAsync(string.Format($"EndUsers/{userId}/CompanyEndUsers")).Result;
                if (response.IsSuccessStatusCode)
                {
                    var projects = await response.Content.ReadAsAsync<List<EndUsersListViewModel>>();

                    foreach (var p in projects)
                    {
                        EndUsers.Add(p);
                    }
                }
            }
            return EndUsers;
        }
        public static async Task<List<ContractsListViewModel>> ContractsByCompany(string userId)
        {
            var Contracts = new List<ContractsListViewModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                HttpResponseMessage response = client.GetAsync(string.Format($"Contracts/{userId}/Contracts")).Result;
                if (response.IsSuccessStatusCode)
                {
                    var projects = await response.Content.ReadAsAsync<List<ContractsListViewModel>>();

                    foreach (var p in projects)
                    {
                        Contracts.Add(p);
                    }
                }
            }
            return Contracts;
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
        public static async Task<List<CompanySitesListViewModel>> CompanySites(string user)
        {
            List<CompanySitesListViewModel> companyList = new List<CompanySitesListViewModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                HttpResponseMessage response = client.GetAsync(string.Format($"MasteData/{user}/CompanySites")).Result;
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
                HttpResponseMessage response = client.GetAsync(string.Format("inventorycontrol/projectSitesList/{0}/listitems", id)).Result;
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