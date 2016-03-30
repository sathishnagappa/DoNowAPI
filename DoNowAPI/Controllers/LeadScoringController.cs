using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Configuration;

namespace DoNowAPI.Controllers
{
   
    public class LeadScoringController : ApiController
    {
        
        private string apiKey = ConfigurationManager.AppSettings["AzureMLApiKey_Lead"];
        private string BaseAddress = ConfigurationManager.AppSettings["AzureMLBaseAddress_Lead"];
        public class StringTable
          {
              public string[] ColumnNames { get; set; }
              public string[,] Values { get; set; }
          }
                 

       // public  static async Task InvokeRequestResponseService()
        public  async Task<string> Post()
        {

            string Temp;
            Temp = "";
            try
             {
                 using (var client = new HttpClient())
                 {
                     var scoreRequest = new
                     {
                         GlobalParameters = new Dictionary<string, string>()
                         {
                         }
                     };
                    // const string apiKey = "DCHvG0809f++ETfNy/I3ODexG3ZC2hT8RM2u1Fq4Gx9tnbBKBpx4ys+LR/AZuMEKRyDhcZo+mHMzN5yLfqL6YQ=="; // Replace this with the API key from web config file
                     client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                    // client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/02278d1d18e244feb377937c5e508f14/services/5a819a0e847a49b18f0293d3ae1b63b2/execute?api-version=2.0&details=true");
                    client.BaseAddress = new Uri(BaseAddress);

                    HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest);
                    

                     if (response.IsSuccessStatusCode)
                     {
                         string result = await response.Content.ReadAsStringAsync();
                         Console.WriteLine("Result: {0}", result);
                        Temp = "Result " + result.ToString();
                    }
                     else
                     {
                         Console.WriteLine(string.Format("The request failed with status code: {0}", response.StatusCode));

                         // Print the headers - they include the requert ID and the timestamp, which are useful for debugging the failure
                         Console.WriteLine(response.Headers.ToString());

                         string responseContent = await response.Content.ReadAsStringAsync();
                         Console.WriteLine(responseContent);
                        Temp = responseContent;
                    }
                   
                 }
                return Temp;
                }
                 catch(Exception ex)
             {
                return ex.Message;
             } 
          
            }
    }
}
