using DoNowAPI.Models;
using MySql.Data.MySqlClient;
using System;
using System.Web.Http;
using System.Configuration;

namespace DoNowAPI.Controllers
{
    public class ReferralRequestsUpdateController : ApiController
    {
        private string MyConnnectionString = ConfigurationManager.AppSettings["DoNowConnectionString"];
        public int Post([FromBody]ReferralRequests value)
        {
            string stringSQL;
            int result = -1;
            using (MySqlConnection connection = new MySqlConnection(MyConnnectionString))
            {
                connection.Open();

                using (MySqlCommand cmd = connection.CreateCommand())
                { 
                    stringSQL = "Update referral_requests Set status = " + value.Status + " where ID = " + value.ID;
                cmd.CommandText = stringSQL;
                result = cmd.ExecuteNonQuery();
                }
                connection.Close();

            }
            return result;
        }
    }
}
