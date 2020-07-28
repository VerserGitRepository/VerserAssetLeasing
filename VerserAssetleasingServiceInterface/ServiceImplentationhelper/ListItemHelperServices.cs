using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace VerserAssetleasingServiceInterface.ServiceImplentationhelper
{
    public class ListItemHelperServices
    {

        private static readonly string CostModelAPIURL = ConfigurationManager.AppSettings["CostModelAPIBase"] + ConfigurationManager.AppSettings["CostModelAPIRoot"];
        public static async Task<List<ListItems>> ProjectManagerList()
        {
            List<ListItems> returnmodel = new List<ListItems>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(CostModelAPIURL);
                HttpResponseMessage response = client.GetAsync(string.Format("ListItems/ProjectManagerList")).Result;
                if (response.IsSuccessStatusCode)
                {
                    returnmodel = await response.Content.ReadAsAsync<List<ListItems>>();
                }
            }
            return returnmodel;
        }
        public static async Task<List<ListItems>> SalesManagerList()
        {
            List<ListItems> returnmodel = new List<ListItems>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(CostModelAPIURL);
                HttpResponseMessage response = client.GetAsync(string.Format("ListItems/SalesManagerList")).Result;
                if (response.IsSuccessStatusCode)
                {
                    returnmodel = await response.Content.ReadAsAsync<List<ListItems>>();
                }
            }
            return returnmodel;
        }
    }
}