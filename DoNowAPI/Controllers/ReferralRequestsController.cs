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
                stringSQL = " SELECT r.ID,IFNULL(SellerName,'') AS SellerName,r.Status,IFNULL(CompanyInfo,'') AS CompanyInfo, IFNULL(CompanyName,'') AS CompanyName, "
                                + " IFNULL(Prospect,'') AS Prospect,IFNULL(LeadEmailID,'') AS LeadEmailID,IFNULL(CreatedOn,'') AS CreatedOn, BrokerUserID,BrokerID, "
                                + " LeadID,SellerUserID,IFNULL(r.City,'') AS City,IFNULL(r.State,'') AS State,IFNULL(BusinessNeeds,'') AS BusinessNeeds,"
                                + " IFNULL(r.Industry,'') AS Industry, IFNULL(B.Title,'') as SellerTitle,IFNULL(B.City,'') AS SellerCity,"
                                + " IFNULL(B.State,'') AS SellerState, IFNULL(B.Company,'') as SellerCompany,IFNULL(B.Industry,'') as SellerIndustry,"
                                + " IFNULL(B.Office_Address,'') as SellerOfficeAddress, IFNULL(B.Zip,'') as SellerZipCode,IFNULL(B.Phone,'') as SellerPhone,"
                                + " IFNULL(B.LINE_OF_BUSINESS,'') as SellerLOB, A.LEAD_COMP_NAME AS COMPANY_NAME, IFNULL(A.LEAD_TITLE,'') as LEAD_TITLE,"
                                + " A.LEAD_COMP_STATE AS LEAD_STATE, A.LEAD_COMP_CITY AS LEAD_CITY, A.LEAD_COMP_INDUSTRY AS LEAD_INDUSTRY, A.LINE_OF_BUSINESS AS LEAD_LOB,"
                                + " A.LEAD_COMP_PHONE_NO_1 AS LEAD_PHONE, IFNULL(A.LEAD_COMP_ADDRESS,'') AS LEAD_COMP_ADDRESS , IFNULL(A.LEAD_COMP_ZIPCODE,'') AS LEAD_COMP_ZIPCODE,"
                                + " IFNULL(A.LEAD_COMP_COUNTRY,'') AS LEAD_COMP_COUNTRY, IFNULL(A.Fiscal_Year_End,'') AS FiscalYE, IFNULL(A.Annual_Revenue,'') AS Revenue,"
                                + " IFNULL(A.Net_income,'') AS NetIncome, IFNULL(A.Number_of_Employee,'') AS Employees, IFNULL(A.Market_Value,'') AS MarketValue,"
                                + " IFNULL(A.Year_Of_Founding,'') AS YearFounded, IFNULL(A.DBPreScreen_Score,'') AS IndustryRiskScore, IFNULL(A.Lead_Comp_County,'') AS County,"
                                + " IFNULL(A.Web_Address,'') AS WebAddress FROM referral_requests r inner join user_details B on r.SellerUserID = B.ID"
                                + " Inner join dn_lead_det_e A on A.ID = r.LeadID where BrokerUserID=" + BrokerUserID;
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
                            SellerUserID = long.Parse(reader["SellerUserID"].ToString()),
                            SellerTitle = reader["SellerTitle"].ToString(),
                            SellerCity = reader["SellerCity"].ToString(),
                            SellerState = reader["SellerState"].ToString(),
                            SellerCompany = reader["SellerCompany"].ToString(),
                            SellerIndustry = reader["SellerIndustry"].ToString(),
                            SellerOfficeAddress = reader["SellerOfficeAddress"].ToString(),
                            SellerZipCode = reader["SellerZipCode"].ToString(),
                            SellerPhone = reader["SellerPhone"].ToString(),
                            SellerLOB = reader["SellerLOB"].ToString(),
                            COMPANY_NAME = reader["COMPANY_NAME"].ToString(),
                            LEAD_TITLE = reader["LEAD_TITLE"].ToString(),
                            LEAD_STATE = reader["LEAD_STATE"].ToString(),
                            LEAD_CITY = reader["LEAD_CITY"].ToString(),
                            LEAD_INDUSTRY = reader["LEAD_INDUSTRY"].ToString(),
                            LEAD_LOB = reader["LEAD_LOB"].ToString(),
                            LEAD_PHONE = reader["LEAD_PHONE"].ToString(),
                            LEAD_COMP_ADDRESS = reader["LEAD_COMP_ADDRESS"].ToString(),
                            LEAD_COMP_ZIPCODE = reader["LEAD_COMP_ZIPCODE"].ToString(),
                            LEAD_COMP_COUNTRY = reader["LEAD_COMP_COUNTRY"].ToString(),
                            FiscalYE = reader["FiscalYE"].ToString(),
                            Revenue = reader["Revenue"].ToString(),
                            NetIncome = reader["NetIncome"].ToString(),
                            Employees = reader["Employees"].ToString(),
                            MarketValue = reader["MarketValue"].ToString(),
                            YearFounded = reader["YearFounded"].ToString(),
                            IndustryRiskScore = reader["IndustryRiskScore"].ToString(),
                            County = reader["County"].ToString(),
                            WebAddress = reader["WebAddress"].ToString()


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
                    cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Parse(value.CreatedOn));
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
