using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using VerserAssetleasingServiceInterface.Models;
using System.Collections.Generic;

namespace VerserAssetleasingServiceInterface.ServiceHelper
{
    public class LoginService
    {
        public static string AssetLeaseUri = ConfigurationManager.AppSettings["AssetleasingAPIBaseURL"];
        public static readonly string BaseUri = ConfigurationManager.AppSettings["LoginBase"] + ConfigurationManager.AppSettings["LoginRoot"];
        public async static Task<LoginModel> Login(LoginModel login)
        {
            LoginModel returnmessage = new LoginModel();
           

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                HttpResponseMessage response = client.PostAsJsonAsync("Login/AuthenticateUser", login).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<LoginModel>();
                    returnmessage = result;
                }
            }
            return returnmessage;
        }
        public async static Task<int> UserCompanyId(string userId)
        {
            int ReturnID = 0;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(AssetLeaseUri);
                HttpResponseMessage response = client.GetAsync(string.Format($"MasteData/{userId}/UserCompanyID")).Result;
                if (response.IsSuccessStatusCode)
                {
                     ReturnID = await response.Content.ReadAsAsync<int>();
                }
            }
            return 100000;
        }

        public async static Task<List<ListItemViewModel>> UserRoleList(string UserName)
        {
            var returnmessage = new List<ListItemViewModel>();          

            using (HttpClient client = new System.Net.Http.HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                HttpResponseMessage response = client.GetAsync(string.Format("Login/{0}/UserRole", UserName)).Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<ListItemViewModel>>();
                    foreach (var p in result)
                    {
                        returnmessage.Add(new ListItemViewModel() { Id = p.Id, Value = p.Value });
                    }
                }
            }
            return returnmessage;
        }
    }
}
            
       