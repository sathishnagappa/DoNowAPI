using DoNowAPI.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace DoNowAPI.Controllers
{
    public class CustomerInteractionController : ApiController
    {
        private string MyConnnectionString = ConfigurationManager.AppSettings["DoNowConnectionString"];
        [HttpGet]
        public IEnumerable<CustomerInteractions> Get(string CustomerName, long UserID)
        {
            List<CustomerInteractions> customerInteractions = new List<CustomerInteractions>();
            using (MySqlConnection connection = new MySqlConnection(MyConnnectionString))
            {
                connection.Open();

                string stringSQL;
                using (MySqlCommand cmd = connection.CreateCommand())
                { 
                    stringSQL = "SELECT USERID,IFNULL(CustomerName,'') AS CustomerName, IFNULL(Type,'') AS Type, DateNTime "
                       + " FROM donow.Customer_Interactions where CustomerName ='" + CustomerName + "' and UserID=" + UserID;
                    cmd.CommandText = stringSQL;
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            customerInteractions.Add(new CustomerInteractions
                            {
                                UserId = long.Parse(reader["USERID"].ToString()),
                                CustomerName = reader["CustomerName"].ToString(),
                                Type = reader["Type"].ToString(),
                                DateNTime = reader["DateNTime"].ToString()

                            });
                        }
                    }
                }
                connection.Close();
            }
               
            return customerInteractions.ToArray();
        }
        //}

        // POST api/values
        [HttpPost]
        public int Post([FromBody]CustomerInteractions value)
        {
            int LeadID = -1;

            using(MySqlConnection connection = new MySqlConnection(MyConnnectionString)) 
            {
                connection.Open();
                using (MySqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO donow.Customer_Interactions values(@USERID,@CustomerName,@Type,@DateNTime,@leadID)";
                    cmd.Parameters.AddWithValue("@UserID", value.UserId);
                    cmd.Parameters.AddWithValue("@CustomerName", value.CustomerName);
                    cmd.Parameters.AddWithValue("@Type", value.Type);
                    cmd.Parameters.AddWithValue("@DateNTime", DateTime.Parse(value.DateNTime));
                    cmd.Parameters.AddWithValue("@leadID", value.LeadId);
                    LeadID = cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
            return LeadID;
        }
    }
}
