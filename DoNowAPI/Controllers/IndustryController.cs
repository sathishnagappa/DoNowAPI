using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Configuration;

namespace DoNowAPI.Controllers
{
    public class IndustryController : ApiController
    {
        private string MyConnnectionString = ConfigurationManager.AppSettings["DoNowConnectionString"];

        [HttpGet]
        public IEnumerable<string> Get()
         {
             List<string> industryList = new List<string>();
            using (MySqlConnection connection = new MySqlConnection(MyConnnectionString)) 
            {
                connection.Open();

                using (MySqlCommand cmd = connection.CreateCommand())
                { 
                    cmd.CommandText = "SELECT IFNULL(IndustryName,'') as IndustryName FROM ui_industry";

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            industryList.Add(reader["IndustryName"].ToString());
                        }
                    }
                }
                connection.Close();
            }
               
             return industryList.ToArray();

         }
    }
}
