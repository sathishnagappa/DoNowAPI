using DoNowAPI.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace DoNowAPI.Controllers 
{
    public class StringTable
    {
        public string[] ColumnNames { get; set; }
        public string[,] Values { get; set; }
    }

    public class DealHistoryController : ApiController
    {

        private string MyConnnectionString = ConfigurationManager.AppSettings["DoNowConnectionString"];
        [HttpGet]
        public IEnumerable<DealHistory> Get(long LeadID, long UserID)
        {
            List<DealHistory> dealHistory = new List<DealHistory>();
            using (MySqlConnection connection = new MySqlConnection(MyConnnectionString)) 
            {
                connection.Open();

                using(MySqlCommand cmd = connection.CreateCommand())
                {   string stringSQL;
                    stringSQL = "SELECT LeadID,Date, UserID,BrokerID, IFNULL(Lead_City, '') as City,IFNULL(Lead_State, '') as State,IFNULL(Lead_Industry, '') as Lead_Industry,IFNULL(CustomerName, '') as CustomerName FROM Deal_History where LeadID =" + LeadID + " and UserID =" + UserID;

                    cmd.CommandText = stringSQL;
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dealHistory.Add(new DealHistory
                            {
                                LeadId = long.Parse(reader["LeadID"].ToString()),
                                UserId = long.Parse(reader["UserID"].ToString()),
                                Date = reader["Date"].ToString(),
                                City = reader["City"].ToString(),
                                State = reader["State"].ToString(),
                                CustomerName = reader["CustomerName"].ToString(),
                                LeadIndustry = reader["Lead_Industry"].ToString(),
                                BrokerId = long.Parse(reader["BrokerID"].ToString())
                            });
                        }
                    }
                }
                connection.Close();
            }
           
            return dealHistory.ToArray();
        }
        // POST api/values
        public int Post([FromBody]DealHistory value)
        {
            int result = -1; 
            using (MySqlConnection connection = new MySqlConnection(MyConnnectionString))
            {
                connection.Open();
                using (MySqlCommand cmd = connection.CreateCommand())
                {

                    string stringSQL = "Select CASE when Count(BROKER_ID) = 0 THEN 0 ELSE BROKER_ID END from dn_broker_scoring_e where LEAD_ID =" + value.LeadId + " and Broker_Status =4 limit 1";
                    cmd.CommandText = stringSQL;

                    long BrokerID = long.Parse(cmd.ExecuteScalar().ToString());

                    cmd.CommandText = "INSERT INTO Deal_History values(@LeadID, @UserID, @BrokerID, @Date,@Lead_City, @Lead_State,@Lead_Industry,@CustomerName)";
                    cmd.Parameters.AddWithValue("@LeadID", value.LeadId);
                    cmd.Parameters.AddWithValue("@UserID", value.UserId);
                    cmd.Parameters.AddWithValue("@BrokerID", BrokerID);
                    cmd.Parameters.AddWithValue("@Date", DateTime.Parse(value.Date));
                    cmd.Parameters.AddWithValue("@Lead_City", value.City);
                    cmd.Parameters.AddWithValue("@Lead_State", value.State);
                    cmd.Parameters.AddWithValue("@Lead_Industry", value.LeadIndustry);
                    cmd.Parameters.AddWithValue("@CustomerName", value.CustomerName);
                    result =cmd.ExecuteNonQuery();

                }
                connection.Close();
            }
            return result;
        }
        
    }
}
