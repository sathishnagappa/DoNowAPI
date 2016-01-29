using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Configuration;

namespace DoNowAPI.Controllers
{
    public class BrokerScoringController : ApiController  
    {
        private string apiKey = ConfigurationManager.AppSettings["AzureMLApiKey"];
        private string BaseAddress = ConfigurationManager.AppSettings["AzureMLBaseAddress"]; 
       
        public async Task<string> Post()
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
                  // const string apiKey = "Nj1o3waMvlhaRbbx5PvQLFHSXzDb7OPFptGcoL+8DOFOpWSz4V51pPtUs065GWBJ1q9lyDxIBgMr5kMM5jDmsA=="; // Replace this with the API key from web config file
                   client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                   // client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/02278d1d18e244feb377937c5e508f14/services/ff3f5e1a84154825bfee72381c3e2956/execute?api-version=2.0&details=true");
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
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

    }

}
