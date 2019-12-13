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
    }
}