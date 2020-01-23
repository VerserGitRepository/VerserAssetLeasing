using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using VerserAssetleasingServiceInterface.Models;

namespace VerserAssetleasingServiceInterface.ServiceHelper
{
    public class LoginService
    {
        public static string AssetLeaseUri = ConfigurationManager.AppSettings["AssetleasingAPIBaseURL"];
        public async static Task<LoginModel> Login(LoginModel login)
        {
            LoginModel returnmessage = new LoginModel();
            string BaseUri = ConfigurationManager.AppSettings["LoginBase"] + ConfigurationManager.AppSettings["LoginRoot"];

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
    }
}
            
       