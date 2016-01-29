using ContactList.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Configuration;

namespace DoNowAPI.Controllers
{
    public class LeadTalkingPointsController : ApiController
    {
        private string MyConnnectionString = ConfigurationManager.AppSettings["DoNowConnectionString"];
        
        [HttpGet]
        public IEnumerable<LeadTalkingPoints> Get()
        {
            List<LeadTalkingPoints> talkingPointsList = new List<LeadTalkingPoints>();
           using (MySqlConnection connection = new MySqlConnection(MyConnnectionString))
            {
                connection.Open();

                using (MySqlCommand cmd = connection.CreateCommand())
                { 
                    cmd.CommandText = "SELECT Lead_ID, IFNULL(Points,'') as Points FROM donow.lead_talking_points";

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            talkingPointsList.Add(new LeadTalkingPoints
                            {
                                LeadId = long.Parse(reader["Lead_ID"].ToString()),
                                Points = reader["Points"].ToString()

                            });
                        }
                    }
                }
                connection.Close();
            }
           
            return talkingPointsList.ToArray();
        }

        // POST api/values
        public void Post([FromBody]LeadTalkingPoints value)
        {
            using(MySqlConnection connection = new MySqlConnection(MyConnnectionString))
            {
                connection.Open();

                using (MySqlCommand cmd = connection.CreateCommand())
                { 
                    cmd.CommandText = "INSERT INTO donow.lead_talking_points values(@Lead_ID,@Points)";
                    cmd.Parameters.AddWithValue("@Lead_ID", value.LeadId);
                    cmd.Parameters.AddWithValue("@Points", value.Points);
                    cmd.ExecuteNonQuery();

                }
                connection.Close();
            }
          
        }
    }
}
