using DoNowAPI.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;

namespace DoNowAPI.Controllers
{
    public class ReferralRequestsController : ApiController
    {
        private string MyConnnectionString = ConfigurationManager.AppSettings["DoNowConnectionString"];

        [HttpGet]
        public IEnumerable<ReferralRequests> Get()
        {   List<ReferralRequests> referralRequestsList = new List<ReferralRequests>();
            using(MySqlConnection connection = new MySqlConnection(MyConnnectionString))
            {
                connection.Open();

                using (MySqlCommand cmd = connection.CreateCommand())
                { 
                    cmd.CommandText = "SELECT ID,IFNULL(SellerName,'') AS SellerName,Status,IFNULL(CompanyInfo,'') AS CompanyInfo, IFNULL(CompanyName,'') AS CompanyName, IFNULL(Prospect,'') AS Prospect, "
                                       + " IFNULL(LeadEmailID,'') AS LeadEmailID,IFNULL(CreatedOn,'') AS CreatedOn, BrokerUserID,BrokerID,LeadID,SellerUserID,IFNULL(City,'') AS City,IFNULL(State,'') AS State, "
                                       + " IFNULL(BusinessNeeds,'') AS BusinessNeeds,IFNULL(Industry,'') AS Industry  FROM referral_requests r ";


                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        referralRequestsList.Add(new ReferralRequests
                        {
                            ID = long.Parse(reader["ID"].ToString()),
                            SellerName = reader["SellerName"].ToString(),
                            LeadID = long.Parse(reader["LeadID"].ToString()),
                            City = reader["City"].ToString(),
                            State = reader["State"].ToString(),
                            Industry = reader["Industry"].ToString(),
                            BusinessNeeds = reader["BusinessNeeds"].ToString(),
                            Prospect = reader["Prospect"].ToString(),
                            CreatedOn = reader["CreatedOn"].ToString(),
                            CompanyInfo = reader["CompanyInfo"].ToString(),
                            CompanyName = reader["CompanyName"].ToString(),
                            LeadEmailID = reader["LeadEmailID"].ToString(),
                            BrokerID = long.Parse(reader["BrokerID"].ToString()),
                            BrokerUserID = long.Parse(reader["BrokerUserID"].ToString()),
                            Status = int.Parse(reader["Status"].ToString()),
                            SellerUserID = long.Parse(reader["SellerUserID"].ToString())

                        });
                    }
                }
                }
                connection.Close();
            }
          
            return referralRequestsList.ToArray();

        }

        public long Get(bool temp1)
        {
            long ReferralId = 0;
            using (MySqlConnection connection = new MySqlConnection(MyConnnectionString))
            { 
                connection.Open();
                using (MySqlCommand cmd = connection.CreateCommand())
                { 

                    cmd.CommandText = "select IFNULL(max(ID),1) from donow.referral_requests";
                    var temp = cmd.ExecuteScalar();

                    if (temp == null)
                    {
                        ReferralId = 1;
                    }
                }
                connection.Close();
            }
            return ReferralId;
        }

        [HttpGet]
        public IEnumerable<ReferralRequests> Get(long BrokerUserID)
        {
            List<ReferralRequests> referralRequestsList = new List<ReferralRequests>();
            using (MySqlConnection connection = new MySqlConnection(MyConnnectionString))
            {
                connection.Open(); 

                string stringSQL;
                stringSQL = "SELECT ID,IFNULL(SellerName,'') AS SellerName,Status,IFNULL(CompanyInfo,'') AS CompanyInfo, IFNULL(CompanyName,'') AS CompanyName, IFNULL(Prospect,'') AS Prospect, "
                                   + " IFNULL(LeadEmailID,'') AS LeadEmailID,IFNULL(CreatedOn,'') AS CreatedOn, BrokerUserID,BrokerID,LeadID,SellerUserID,IFNULL(City,'') AS City,IFNULL(State,'') AS State, "
                                   + " IFNULL(BusinessNeeds,'') AS BusinessNeeds,IFNULL(Industry,'') AS Industry  FROM referral_requests r where BrokerUserID=" + BrokerUserID;
                using (MySqlCommand cmd = connection.CreateCommand())
                { 
                    cmd.CommandText = stringSQL;
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        referralRequestsList.Add(new ReferralRequests
                        {
                            ID = long.Parse(reader["ID"].ToString()),
                            SellerName = reader["SellerName"].ToString(),
                            LeadID = long.Parse(reader["LeadID"].ToString()),
                            City = reader["City"].ToString(),
                            State = reader["State"].ToString(),
                            Industry = reader["Industry"].ToString(),
                            BusinessNeeds = reader["BusinessNeeds"].ToString(),
                            Prospect = reader["Prospect"].ToString(),
                            CreatedOn = reader["CreatedOn"].ToString(),
                            CompanyInfo = reader["CompanyInfo"].ToString(),
                            CompanyName = reader["CompanyName"].ToString(),
                            LeadEmailID = reader["LeadEmailID"].ToString(),
                            BrokerID = long.Parse(reader["BrokerID"].ToString()),
                            BrokerUserID = long.Parse(reader["BrokerUserID"].ToString()),
                            Status = int.Parse(reader["Status"].ToString()),
                            SellerUserID = long.Parse(reader["SellerUserID"].ToString())

                        });
                    }
                }
                }
                connection.Close();
            }
           
            return referralRequestsList.ToArray();

        }
        [HttpPut]
        // public int Put([FromBody]ReferralRequests value)
        public int Put(long ID, [FromBody]int status)
        {
            int result = -1;
            using (MySqlConnection connection = new MySqlConnection(MyConnnectionString))
            {
                connection.Open();

                using (MySqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "Update referral_requests Set status = " + status + " where ID = " + ID;
                   
                    result = cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
           
            return result;
        }
        // POST api/values
        public long Post([FromBody]ReferralRequests value)
        {  
            long ReferralId = -1;
            using(MySqlConnection connection = new MySqlConnection(MyConnnectionString))
            {
                connection.Open();
                using (MySqlCommand cmd = connection.CreateCommand())
                { 
                    cmd.CommandText = "select IFNULL(max(ID),0) from donow.referral_requests";
                    
                    ReferralId = long.Parse(cmd.ExecuteScalar().ToString())+1;
              
                    cmd.CommandText = "INSERT INTO donow.referral_requests values(@ID, @SellerName, @Status, @CompanyInfo, @LeadEmailID, @CreatedOn, @BrokerUserID, @BrokerID, @LeadID, @SellerUserID, @City, @BusinessNeeds, @Industry, @Prospect, @CompanyName,@State)";
                    cmd.Parameters.AddWithValue("@ID", ReferralId);
                    cmd.Parameters.AddWithValue("@SellerName", value.SellerName);
                    cmd.Parameters.AddWithValue("@Status", value.Status);
                    cmd.Parameters.AddWithValue("@CompanyInfo", value.CompanyInfo);
                    cmd.Parameters.AddWithValue("@LeadEmailID", value.LeadEmailID);
                    cmd.Parameters.AddWithValue("@CreatedOn", value.CreatedOn);
                    cmd.Parameters.AddWithValue("@BrokerUserID", value.BrokerUserID);
                    cmd.Parameters.AddWithValue("@BrokerID", value.BrokerID);
                    cmd.Parameters.AddWithValue("@LeadID", value.LeadID);
                    cmd.Parameters.AddWithValue("@SellerUserID", value.SellerUserID);
                    cmd.Parameters.AddWithValue("@City", value.City);
                    cmd.Parameters.AddWithValue("@BusinessNeeds", value.BusinessNeeds);
                    cmd.Parameters.AddWithValue("@Industry", value.Industry);
                    cmd.Parameters.AddWithValue("@Prospect", value.Prospect);
                    cmd.Parameters.AddWithValue("@CompanyName", value.CompanyName);
                    cmd.Parameters.AddWithValue("@State", value.State);

                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
           
            return ReferralId;
        }
    }
}
