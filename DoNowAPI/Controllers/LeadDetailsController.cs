using DoNowAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;
using MySql.Data.MySqlClient;


namespace DoNowAPI.Controllers
{
    public class LeadDetailsController : ApiController
    {
        private string MyConnnectionString = ConfigurationManager.AppSettings["DoNowConnectionString"];

      
        [HttpGet]
        public List<LeadMaster> Get(long id)
        {
          
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            long secondsSinceEpoch = (long)t.TotalMilliseconds;
            //string startTime = DateTime.Now.ToString() + " Milli" + DateTime.Now.Millisecond.ToString();
            string startTime = secondsSinceEpoch.ToString();
            string startTime2;
            string startTime3;

            List<LeadMaster> leadMasterDetails = new List<LeadMaster>();
            using (MySqlConnection connection = new MySqlConnection(MyConnnectionString))
            {
                connection.Open();

                TimeSpan t2 = DateTime.UtcNow - new DateTime(1970, 1, 1);
                long secondsSinceEpoch2 = (long)t2.TotalMilliseconds;
                //string startTime = DateTime.Now.ToString() + " Milli" + DateTime.Now.Millisecond.ToString();
                startTime2 = secondsSinceEpoch2.ToString();

                using (MySqlCommand cmd = connection.CreateCommand())
                {
                    
                    string stringSQL = "SELECT A.ID AS LEAD_ID, A.LEAD_NAME, IFNULL(A.LEAD_TITLE,'') as LEAD_TITLE, A.LEAD_COMP_NAME AS COMPANY_NAME, A.LEAD_COMP_STATE AS STATE, A.LEAD_COMP_CITY AS CITY, A.LEAD_SRC_SYS_ID AS LEAD_SOURCE, A.EXISTING_CUSTOMER AS LEAD_TYPE, A.CREATE_TS AS LEAD_CREATE_TIME,"
                 + "  C.u_l_status,  C.SCORE AS LEAD_SCORE, A.LEAD_COMP_INDUSTRY AS LEAD_INDUSTRY FROM dn_lead_det_e A  INNER JOIN dn_lead_e B ON A.ID = B.ID  INNER JOIN dn_scoring_e C ON A.ID = C.LEAD_ID  WHERE C.u_l_status not in (6,0) and C.USER_ID=" + id + " order by u_l_status asc, C.update_ts desc ";
                    cmd.CommandText = stringSQL;

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        TimeSpan t3 = DateTime.UtcNow - new DateTime(1970, 1, 1);
                        long secondsSinceEpoch3 = (long)t3.TotalMilliseconds;
                        //string startTime = DateTime.Now.ToString() + " Milli" + DateTime.Now.Millisecond.ToString();
                        startTime3 = secondsSinceEpoch3.ToString();

                        while (reader.Read())
                        {
                            leadMasterDetails.Add(new LeadMaster
                            {
                                LEAD_ID = long.Parse(reader["LEAD_ID"].ToString()),
                                LEAD_NAME = reader["LEAD_NAME"].ToString(),
                                COMPANY_NAME = reader["COMPANY_NAME"].ToString(),
                                STATE = reader["STATE"].ToString(),
                                CITY = reader["CITY"].ToString(),
                                LEAD_SCORE = (int)reader["LEAD_SCORE"],
                                USER_LEAD_STATUS = (int)reader["u_l_status"],
                                LeadIndustry = reader["LEAD_INDUSTRY"].ToString(),
                                CreatedOn = reader["LEAD_CREATE_TIME"].ToString(),
                                LEAD_TYPE = reader["LEAD_TYPE"].ToString(),
                                LEAD_TITLE = reader["LEAD_TITLE"].ToString()

                            });
                        }
                    }

                }
                connection.Close();
            }
           
            
            t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            secondsSinceEpoch = (long)t.TotalMilliseconds;
            //string startTime = DateTime.Now.ToString() + " Milli" + DateTime.Now.Millisecond.ToString();
            string EndTime = secondsSinceEpoch.ToString();

            if (leadMasterDetails != null)
            {
                leadMasterDetails[0].StartTime = startTime;
                leadMasterDetails[0].EndTime = EndTime;
                leadMasterDetails[0].AfterConnectionOpenTime = startTime2;
                leadMasterDetails[0].AfterExecutingSQLTime = startTime3;
            } 

            return leadMasterDetails;

        }
        public Prospect Get(long LeadID, long UserID, string Type)
        {
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            long secondsSinceEpoch = (long)t.TotalMilliseconds;
            //string startTime = DateTime.Now.ToString() + " Milli" + DateTime.Now.Millisecond.ToString();
            string startTime = secondsSinceEpoch.ToString();

            Prospect prospectData = null;

            using (MySqlConnection connection = new MySqlConnection(MyConnnectionString))
            {
                connection.Open();

                using (MySqlCommand cmd = connection.CreateCommand())
                {
                   
                     cmd.CommandText = "SELECT A.ID AS LEAD_ID, A.LEAD_NAME, A.LEAD_COMP_NAME AS COMPANY_NAME, IFNULL(A.LEAD_TITLE,'') as LEAD_TITLE,  "
                                + " A.LEAD_COMP_STATE AS STATE, A.LEAD_COMP_CITY AS CITY, A.LEAD_SRC_SYS_ID AS LEAD_SOURCE, A.LEAD_COMP_INDUSTRY AS "
                                + " INDUSTRY_INFO,A.LINE_OF_BUSINESS AS BUSINESS_NEED, A.EXISTING_CUSTOMER AS LEAD_TYPE, A.CREATE_TS AS LEAD_CREATE_TIME, "
                                + " A.STATUS, IFNULL(A.REASON_FOR_PASS,'') AS REASON_FOR_PASS, A.LEAD_COMP_PHONE_NO_1 AS PHONE,A.LEAD_COMP_EMAIL_ID AS "
                                + " EMAILID, C.u_l_status, C.USER_ID, B.LEAD_STATUS_C AS LEAD_STATUS, C.SCORE AS LEAD_SCORE, IFNULL(A.LEAD_COMP_ADDRESS,'') AS "
                                + " LEAD_COMP_ADDRESS , IFNULL(A.LEAD_COMP_ZIPCODE,'') AS LEAD_COMP_ZIPCODE, IFNULL(A.LEAD_COMP_COUNTRY,'') AS LEAD_COMP_COUNTRY, "
                                + " IFNULL(A.Fiscal_Year_End,'') AS FiscalYE, IFNULL(A.Annual_Revenue,'') AS Revenue, IFNULL(A.Net_income,'') AS NetIncome, "
                                + " IFNULL(A.Number_of_Employee,'') AS Employees, IFNULL(A.Market_Value,'') AS MarketValue, IFNULL(A.Year_Of_Founding,'') AS "
                                + " YearFounded, IFNULL(A.DBPreScreen_Score,'') AS IndustryRiskScore, IFNULL(A.Lead_Comp_County,'') AS County, "
                                + " IFNULL(A.Web_Address,'') AS WebAddress  FROM dn_lead_det_e A  INNER JOIN dn_lead_e B ON A.ID = B.ID "
                                + " INNER JOIN dn_scoring_e C ON A.ID = C.LEAD_ID WHERE C.u_l_status <> 6 and C.LEAD_ID= " + LeadID + " and C.USER_ID = " + UserID;
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            prospectData = new Prospect
                            {
                                LEAD_ID = long.Parse(reader["LEAD_ID"].ToString()),
                                LEAD_NAME = reader["LEAD_NAME"].ToString(),
                                COMPANY_NAME = reader["COMPANY_NAME"].ToString(),
                                STATE = reader["STATE"].ToString(),
                                CITY = reader["CITY"].ToString(),
                                LEAD_SOURCE = (int)reader["LEAD_SOURCE"],
                                INDUSTRY_INFO = reader["INDUSTRY_INFO"].ToString(),
                                BUSINESS_NEED = reader["BUSINESS_NEED"].ToString(),
                                LEAD_TYPE = reader["LEAD_TYPE"].ToString(),
                                LEAD_CREATE_TIME = reader["LEAD_CREATE_TIME"].ToString(),
                                LEAD_STATUS = reader["LEAD_STATUS"].ToString(),
                                LEAD_SCORE = (int)reader["LEAD_SCORE"],
                                STATUS = reader["STATUS"].ToString(),
                                REASON_FOR_PASS = reader["REASON_FOR_PASS"].ToString(),
                                PHONE = reader["PHONE"].ToString(),
                                EMAILID = reader["EMAILID"].ToString(),
                                USER_LEAD_STATUS = (int)reader["u_l_status"],
                                USER_ID = long.Parse(reader["USER_ID"].ToString()),
                                LEAD_TITLE = reader["LEAD_TITLE"].ToString(),
                                ADDRESS = reader["LEAD_COMP_ADDRESS"].ToString(),
                                ZIPCODE = reader["LEAD_COMP_ZIPCODE"].ToString(),
                                COUNTRY = reader["LEAD_COMP_COUNTRY"].ToString(),
                                FISCALYE = reader["FiscalYE"].ToString(),
                                REVENUE = reader["Revenue"].ToString(),
                                NETINCOME = reader["NetIncome"].ToString(),
                                EMPLOYEES = reader["Employees"].ToString(),
                                MARKETVALUE = reader["MarketValue"].ToString(),
                                YEARFOUNDED = reader["YearFounded"].ToString(),
                                INDUSTRYRISK = reader["IndustryRiskScore"].ToString(),
                                COUNTY = reader["County"].ToString(),
                                WebAddress = reader["WebAddress"].ToString()
                            };

                        }
                    }

                    string SQL;
                    List<CustomerInteractions> customerInteractions = new List<CustomerInteractions>();
                    SQL = "SELECT USERID,IFNULL(CustomerName,'') AS CustomerName, IFNULL(Type,'') AS Type, IFNULL(DateNTime,'') AS DateNTime, leadID "
            + " FROM donow.Customer_Interactions where leadID =" + LeadID + " and UserID=" + UserID;
                    cmd.CommandText = SQL;
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            customerInteractions.Add(new CustomerInteractions
                            {
                                UserId = long.Parse(reader["USERID"].ToString()),
                                CustomerName = reader["CustomerName"].ToString(),
                                Type = reader["Type"].ToString(),
                                DateNTime = reader["DateNTime"].ToString(),
                                LeadId = reader["leadID"].ToString()

                            });
                        }
                    }
                    prospectData.customerInteractionList = customerInteractions;

                    //MeetingList 

                    List<MeetingList> meetingDetails = new List<MeetingList>();
                    SQL = "SELECT * FROM donow.ui_meetinglist where LeadID = " + LeadID + " and UserID=" + UserID;
                    cmd.CommandText = SQL;
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            meetingDetails.Add(new MeetingList
                            {
                                UserId = int.Parse(reader["UserID"].ToString()),
                                City = reader["City"].ToString(),
                                CustomerName = reader["CustomerName"].ToString(),
                                EndDate = DateTime.Parse(reader["EndDate"].ToString()),
                                StartDate = DateTime.Parse(reader["StartDate"].ToString()),
                                Id = int.Parse(reader["ID"].ToString()),
                                LeadId = int.Parse(reader["LeadID"].ToString()),
                                State = reader["State"].ToString(),
                                Subject = reader["Subject"].ToString(),
                                Status = reader["Status"].ToString()

                            });
                        }
                    }

                    prospectData.UserMeetingList = meetingDetails;


                    //Deal Maker data
                    List<DealMaker> dealMaker = new List<DealMaker>();

                    SQL = "SELECT A.ID AS BROKER_ID, CONCAT(IFNULL(A.BROKER_FIRST_NAME, ''), ' ',IFNULL(A.BROKER_LAST_NAME, '')) AS BROKER_NAME, IFNULL(A.BROKER_EMAIL,'') AS BROKER_EMAIL, "
                        + " IFNULL(A.BROKER_STATE, '') AS STATE,IFNULL(A.BROKER_COUNTRY, '') AS COUNTRY,IFNULL(A.BROKER_INDUSTRY, '') AS INDUSTRY, "
                        + " IFNULL(A.BROKER_PHONE_NO_1, '') AS PHONE_NO,  IFNULL(A.BROKER_COMPANY, '') AS COMPANY,   IFNULL(A.broker_city, '') AS CITY, "
                        + " IFNULL(A.CREATE_TS, '') AS CREATE_TIME,     B.BROKER_SCORE,  "
                        + " IFNULL(A.BROKER_FEE,'') AS BROKER_FEE,  IFNULL(A.BROKER_TOTAL_EARNING,'') AS BROKER_TOTAL_EARNING, "
                        + " IFNULL(A.CONNECTION_TO_LEAD, '') AS CONNECTION_TO_LEAD, IFNULL(A.DOMAIN_EXPERTISE, '') AS DOMAIN_EXPERTISE, "
                         + " IFNULL(A.BROKER_TITLE,'') AS BROKER_TITLE,  A.USER_ID AS BROKER_USERID,  IFNULL(A.LINE_OF_BUSINESS,'') AS LINE_OF_BUSINESS, B.Broker_Status AS BROKER_STATUS, "
                          + " B.LEAD_ID  FROM dn_broker_det_e A INNER JOIN dn_broker_scoring_e B ON A.ID = B.BROKER_ID WHERE B.LEAD_ID=" + LeadID + " and A.USER_ID <> " + UserID + " order by B.BROKER_SCORE";


                    cmd.CommandText = SQL;
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dealMaker.Add(new DealMaker
                            {
                                BrokerID = long.Parse(reader["BROKER_ID"].ToString()),
                                BrokerName = reader["BROKER_NAME"].ToString(),
                                BrokerScore = reader["BROKER_SCORE"].ToString(),
                                State = reader["STATE"].ToString(),
                                City = reader["CITY"].ToString(),
                                Industry = reader["INDUSTRY"].ToString(),
                                Phone = reader["PHONE_NO"].ToString(),
                                Country = reader["COUNTRY"].ToString(),
                                Company = reader["COMPANY"].ToString(),
                                CreateTime = reader["CREATE_TIME"].ToString(),
                                BrokerFee = reader["BROKER_FEE"].ToString(),
                                BrokerTotalEarning = reader["BROKER_TOTAL_EARNING"].ToString(),
                                ConnectionLead = reader["CONNECTION_TO_LEAD"].ToString(),
                                DomainExpertise = reader["DOMAIN_EXPERTISE"].ToString(),
                                BrokerEmail = reader["BROKER_EMAIL"].ToString(),
                                BrokerTitle = reader["BROKER_TITLE"].ToString(),
                                BrokerLOB = reader["LINE_OF_BUSINESS"].ToString(),
                                BrokerUserID = long.Parse(reader["BROKER_USERID"].ToString()),
                                Status = int.Parse(reader["BROKER_STATUS"].ToString()),
                                LeadID = long.Parse(reader["LEAD_ID"].ToString())

                            });
                        }
                    }

                    prospectData.brokerList = dealMaker;
                }
                connection.Close();
            }

            t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            secondsSinceEpoch = (long)t.TotalMilliseconds;
            //string startTime = DateTime.Now.ToString() + " Milli" + DateTime.Now.Millisecond.ToString();
            string EndTime = secondsSinceEpoch.ToString();

            if (prospectData != null)
            {
                prospectData.StartTime = startTime;
                prospectData.EndTime = EndTime;
         
            }

            return prospectData;
        }
        
        //This method is forr testing - It should be removed
        public List<LeadMaster> Get(long id, bool temp)
                {
                    string DBEndTime = "";
                    TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
                    long secondsSinceEpoch = (long)t.TotalMilliseconds;

                    string startTime = secondsSinceEpoch.ToString();
                    string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["DoNowConnectionString"];
                    /*ontextFactoryDAO 
                   MySqlConnection connection = new MySqlConnection();
                     Utility.ContextFactoryDAO.opensqlcon(connection);*/
       string EndTimecon = "";
          
            List<LeadMaster> leadMasterDetails = new List<LeadMaster>();
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                TimeSpan tcon = DateTime.UtcNow - new DateTime(1970, 1, 1);
                long secondsSinceEpochcon = (long)tcon.TotalMilliseconds;
                //string startTime = DateTime.Now.ToString() + " Milli" + DateTime.Now.Millisecond.ToString();
                 EndTimecon = secondsSinceEpochcon.ToString();

              
                using (MySqlCommand cmd = connection.CreateCommand())
                {
                

                    string stringSQL = "DoNow_GetLeads";


                    cmd.CommandText = stringSQL;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", id);


                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        TimeSpan t2 = DateTime.UtcNow - new DateTime(1970, 1, 1);
                        long secondsSinceEpoch2 = (long)t2.TotalMilliseconds;
                        //string startTime = DateTime.Now.ToString() + " Milli" + DateTime.Now.Millisecond.ToString();
                        DBEndTime = secondsSinceEpoch2.ToString();

                        while (reader.Read())
                        {
                            leadMasterDetails.Add(new LeadMaster
                            {
                                LEAD_ID = long.Parse(reader["LEAD_ID"].ToString()),
                                LEAD_NAME = reader["LEAD_NAME"].ToString(),
                                COMPANY_NAME = reader["COMPANY_NAME"].ToString(),
                                STATE = reader["STATE"].ToString(),
                                CITY = reader["CITY"].ToString(),
                                LEAD_SCORE = (int)reader["LEAD_SCORE"],
                                USER_LEAD_STATUS = (int)reader["u_l_status"]

                            });
                        }
                    }
                }
            }
                
             

              t = DateTime.UtcNow - new DateTime(1970, 1, 1);
              secondsSinceEpoch = (long)t.TotalMilliseconds;
              //string startTime = DateTime.Now.ToString() + " Milli" + DateTime.Now.Millisecond.ToString();
              string EndTime = secondsSinceEpoch.ToString();

              if (leadMasterDetails != null)
              {
                  leadMasterDetails[0].StartTime = startTime;
                  leadMasterDetails[0].EndTime = EndTime;
                         }  

              return leadMasterDetails;

          }
        
          // GET api/values/5
          //User id will be sent in the argument. find the lead from mapping - donow_scoring_e
          public LeadDetails Get(long id,long userid)       
          {
            LeadDetails leadDetails = null;
            string DBEndTime = "";
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            long secondsSinceEpoch = (long)t.TotalMilliseconds;
            string EndTimecon = "";
            string startTime = secondsSinceEpoch.ToString();

            using (MySqlConnection connection = new MySqlConnection(MyConnnectionString))
            {
                connection.Open();
                TimeSpan tcon = DateTime.UtcNow - new DateTime(1970, 1, 1);
                long secondsSinceEpochcon = (long)tcon.TotalMilliseconds;
                //string startTime = DateTime.Now.ToString() + " Milli" + DateTime.Now.Millisecond.ToString();
                EndTimecon = secondsSinceEpochcon.ToString();
                using (MySqlCommand cmd = connection.CreateCommand())
                {
                    string stringSQL = "SELECT A.ID AS LEAD_ID, A.LEAD_NAME, A.LEAD_COMP_NAME AS COMPANY_NAME, IFNULL(A.LEAD_TITLE,'') as LEAD_TITLE, A.LEAD_COMP_STATE AS STATE, A.LEAD_COMP_CITY AS CITY, A.LEAD_SRC_SYS_ID AS LEAD_SOURCE, A.Company_info AS COMPANY_INFO,A.LINE_OF_BUSINESS AS BUSINESS_NEED, A.EXISTING_CUSTOMER AS LEAD_TYPE, A.CREATE_TS AS LEAD_CREATE_TIME,"
                   + " A.STATUS, A.REASON_FOR_PASS, A.LEAD_COMP_PHONE_NO_1 AS PHONE,A.LEAD_COMP_EMAIL_ID AS EMAILID, C.u_l_status, C.USER_ID, "
                   + " B.LEAD_STATUS_C AS LEAD_STATUS, C.SCORE AS LEAD_SCORE FROM dn_lead_det_e A  INNER JOIN dn_lead_e B ON A.ID = B.ID  INNER JOIN dn_scoring_e C ON A.ID = C.LEAD_ID  WHERE C.u_l_status <> 6 and C.LEAD_ID= " + id + " and C.USER_ID = " + userid + " order by u_l_status asc, C.SCORE desc ";
                    cmd.CommandText = stringSQL;

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string Reason = "";
                            if (!string.IsNullOrEmpty(reader["REASON_FOR_PASS"].ToString()))
                            {
                                Reason = reader["REASON_FOR_PASS"].ToString();
                            }
                            leadDetails = new LeadDetails
                            {
                                LEAD_ID = long.Parse(reader["LEAD_ID"].ToString()),
                                LEAD_NAME = reader["LEAD_NAME"].ToString(),
                                COMPANY_NAME = reader["COMPANY_NAME"].ToString(),
                                STATE = reader["STATE"].ToString(),
                                CITY = reader["CITY"].ToString(),
                                LEAD_SOURCE = (int)reader["LEAD_SOURCE"],
                                COMPANY_INFO = reader["COMPANY_INFO"].ToString(),
                                BUSINESS_NEED = reader["BUSINESS_NEED"].ToString(),
                                LEAD_TYPE = reader["LEAD_TYPE"].ToString(),
                                LEAD_CREATE_TIME = reader["LEAD_CREATE_TIME"].ToString(),
                                LEAD_STATUS = reader["LEAD_STATUS"].ToString(),
                                LEAD_SCORE = (int)reader["LEAD_SCORE"],
                                STATUS = reader["STATUS"].ToString(),
                                REASON_FOR_PASS = Reason,
                                PHONE = reader["PHONE"].ToString(),
                                EMAILID = reader["EMAILID"].ToString(),
                                USER_LEAD_STATUS = long.Parse(reader["u_l_status"].ToString()),
                                USER_ID = long.Parse(reader["USER_ID"].ToString()),
                                LEAD_TITLE = reader["LEAD_TITLE"].ToString()
                            };
                        }
                    }
                }
                connection.Close();

            }

            t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            secondsSinceEpoch = (long)t.TotalMilliseconds;
            //string startTime = DateTime.Now.ToString() + " Milli" + DateTime.Now.Millisecond.ToString();
            string EndTime = secondsSinceEpoch.ToString();

            if (leadDetails != null)
            {
                leadDetails.StartTime = startTime;
                leadDetails.EndTime = EndTime;
                leadDetails.DBEndTime = DBEndTime;
               
            }


            return leadDetails;

          }

          public List<LeadMaster> Get(long id, string update)
          {
            List<LeadMaster> leadMasterDetails = new List<LeadMaster>();
            using (MySqlConnection connection = new MySqlConnection(MyConnnectionString))
            {
                connection.Open();

                using (MySqlCommand cmd = connection.CreateCommand())
                {
                    string stringSQL = "DoNow_GetNewLeads";

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = stringSQL;
                    cmd.Parameters.AddWithValue("@UserID", id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            leadMasterDetails.Add(new LeadMaster
                            {
                                LEAD_ID = long.Parse(reader["LEAD_ID"].ToString()),
                                LEAD_NAME = reader["LEAD_NAME"].ToString(),
                                COMPANY_NAME = reader["COMPANY_NAME"].ToString(),
                                STATE = reader["STATE"].ToString(),
                                CITY = reader["CITY"].ToString(),
                                LEAD_SCORE = (int)reader["LEAD_SCORE"],
                                USER_LEAD_STATUS = (int)reader["u_l_status"],
                                LeadIndustry = reader["LEAD_INDUSTRY"].ToString(),
                                CreatedOn = reader["LEAD_CREATE_TIME"].ToString(),
                                LEAD_TYPE = reader["LEAD_TYPE"].ToString(),
                                LEAD_TITLE = reader["LEAD_TITLE"].ToString()

                            });
                        }
                    }
                }
                connection.Close();
            } 
              return leadMasterDetails;
          }


        public int Post([FromBody]LeadDetails value)
        {
            string stringSQL;
            int result = -1;
            using (MySqlConnection connection = new MySqlConnection(MyConnnectionString))
            {

                connection.Open();

                using (MySqlCommand cmd = connection.CreateCommand())
                {
                    stringSQL = "dn_app_status_update";

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = stringSQL;
                    cmd.Parameters.AddWithValue("@app_status", value.USER_LEAD_STATUS);
                    cmd.Parameters.AddWithValue("@app_lead_id", value.LEAD_ID);
                    cmd.Parameters.AddWithValue("@app_user_id", value.USER_ID);
                    cmd.Parameters.AddWithValue("@app_rfp", value.REASON_FOR_PASS);
                    result = cmd.ExecuteNonQuery();
                }
                connection.Close();

            }

            return result;

        }


    }
}
