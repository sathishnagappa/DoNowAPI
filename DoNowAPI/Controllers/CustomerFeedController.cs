using DoNowAPI.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace DoNowAPI.Controllers
{
    public class CustomerFeedController : ApiController
    {
        
        private string MyConnnectionString = ConfigurationManager.AppSettings["DoNowConnectionString"];
        [HttpGet]
        public IEnumerable<CustomerFeed> Get()
        {
            List<CustomerFeed> customerFeed = new List<CustomerFeed>();
            using (MySqlConnection connection = new MySqlConnection(MyConnnectionString) )
            {
                connection.Open();
                using (MySqlCommand cmd = connection.CreateCommand())
                { 
                    cmd.CommandText = "SELECT Customer_ID,IFNULL(Title,'') AS Title,IFNULL(Details,'') AS Details FROM donow.Customer_Feed";
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            customerFeed.Add(new CustomerFeed
                            {
                                CustomerId = long.Parse(reader["Customer_ID"].ToString()),
                                Title = reader["Title"].ToString(),
                                Details = reader["Details"].ToString()
                            });
                        }
                    }
                }
                connection.Close();
            }
            return customerFeed.ToArray();
        }
                
    }
}
