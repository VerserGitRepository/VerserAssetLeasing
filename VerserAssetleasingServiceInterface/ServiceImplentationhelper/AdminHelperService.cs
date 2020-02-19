﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using VerserAssetleasingServiceInterface.Models;

namespace VerserAssetleasingServiceInterface.ServiceImplentationhelper
{
    public class AdminHelperService
    {
        public static string BaseUri = ConfigurationManager.AppSettings["AssetleasingAPIBaseURL"];

        public static async Task<List<string>> Users()
        {
            var users = new List<string>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                HttpResponseMessage response = client.GetAsync(string.Format("inventorycontrol/JMSUsers")).Result;
                if (response.IsSuccessStatusCode)
                {
                    users = await response.Content.ReadAsAsync<List<string>>();
                    
                }
            }
            return users;
        }
        //public static async Task<List<RolesModel>> Roles()
        //{
        //    var roles = new List<RolesModel>();
        //    using (HttpClient client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri(BaseUri);
        //        HttpResponseMessage response = client.GetAsync(string.Format("MasteData/Users")).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var role = await response.Content.ReadAsAsync<List<RolesModel>>();
        //            foreach (var p in role)
        //            {
        //                roles.Add(p);
        //            }
        //        }
        //    }
        //    return roles;
        //}
        public static async Task<ReturnModel> ChangeUserPermissions(UserRoleModel theModel)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                foreach (string val in theModel.ResourceIDs)
                {
                    foreach (int companyId in theModel.CompanyIDs)
                    {
                        var liteModel = new UserRoleModelLite();
                        liteModel.CompanyID = companyId;
                        liteModel.UserId = Convert.ToString(val);
                        liteModel.CanWrite = theModel.CanEdit;
                        liteModel.ISAdmin = theModel.IsAdmin;
                        HttpResponseMessage response = client.PostAsJsonAsync(string.Format("Admin/AddPermissionsCompanies"), liteModel).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            var user = await response.Content.ReadAsAsync<ReturnModel>();
                        }
                    }
                }
            }
            return new ReturnModel();
        }
    }
}