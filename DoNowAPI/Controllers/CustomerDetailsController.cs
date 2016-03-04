using DoNowAPI.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace DoNowAPI.Controllers
{
    public class CustomerDetailsController : ApiController
    {
        private string MyConnnectionString = ConfigurationManager.AppSettings["DoNowConnectionString"];

        [HttpGet]
        public CustomerDetails Get(string LeadName, long UserID)
        {
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            long secondsSinceEpoch = (long)t.TotalMilliseconds;
            //string startTime = DateTime.Now.ToString() + " Milli" + DateTime.Now.Millisecond.ToString();
            string startTime = secondsSinceEpoch.ToString();
          
            CustomerDetails customerData = null;

            using (MySqlConnection connection = new MySqlConnection(MyConnnectionString)) 
            {
                connection.Open();

                using (MySqlCommand cmd = connection.CreateCommand())
                {  
                    
                    cmd.CommandText = " SELECT A.ID AS LEAD_ID, IFNULL(A.LEAD_NAME,'') AS LEAD_NAME, IFNULL(A.LEAD_TITLE, '') as LEAD_TITLE, IFNULL(A.LEAD_COMP_NAME, '')  "
                                        + " AS COMPANY_NAME,IFNULL(A.LEAD_COMP_STATE, '') AS STATE, IFNULL(A.LEAD_COMP_CITY, '') AS CITY, IFNULL(A.LEAD_COMP_INDUSTRY, '') AS  "
                                        + " COMPANY_INFO, IFNULL(A.LINE_OF_BUSINESS, '') AS BUSINESS_NEED,   IFNULL(A.LEAD_COMP_PHONE_NO_1, '') AS PHONE,  "
                                        + " IFNULL(A.LEAD_COMP_EMAIL_ID, '') AS EMAILID, A.LEAD_SRC_SYS_ID AS LEAD_SOURCE, B.score AS LEAD_SCORE, IFNULL(A.LEAD_COMP_ADDRESS,'') AS  "
                                        + " LEAD_COMP_ADDRESS , IFNULL(A.LEAD_COMP_ZIPCODE,'') AS LEAD_COMP_ZIPCODE, IFNULL(A.LEAD_COMP_COUNTRY,'') AS LEAD_COMP_COUNTRY, "
                                        + " IFNULL(A.Fiscal_Year_End,'') AS FiscalYE, IFNULL(A.Annual_Revenue,'') AS Revenue, IFNULL(A.Net_income,'') AS NetIncome, "
                                        + " IFNULL(A.Number_of_Employee,'') AS Employees, IFNULL(A.Market_Value,'') AS MarketValue, IFNULL(A.Year_Of_Founding,'') AS YearFounded, "
                                        + " IFNULL(A.DBPreScreen_Score,'') AS IndustryRiskScore,IFNULL(A.Lead_Comp_County,'') AS County, "
                                        + " IFNULL(A.Web_Address,'') AS WebAddress FROM dn_lead_det_e A INNER JOIN dn_scoring_e B ON A.ID = B.LEAD_ID  "
                                        + " WHERE B.USER_ID=" + UserID + " and A.LEAD_NAME= '" + LeadName + "'";

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            customerData = new CustomerDetails
                            {
                                //LeadId = (int)reader["LeadID"],
                                LeadId = long.Parse(reader["LEAD_ID"].ToString()),
                                Name = reader["LEAD_NAME"].ToString(),
                                Company = reader["COMPANY_NAME"].ToString(),
                                City = reader["CITY"].ToString(),
                                State = reader["STATE"].ToString(),
                                Phone = reader["PHONE"].ToString(),
                                Email = reader["EMAILID"].ToString(),
                                CompanyInfo = reader["COMPANY_INFO"].ToString(),
                                BusinessNeeds = reader["BUSINESS_NEED"].ToString(),
                                LeadSource = (int)reader["LEAD_SOURCE"],
                                LeadScore = (int)reader["LEAD_SCORE"],
                                LeadTitle = reader["LEAD_TITLE"].ToString(),
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
            + " FROM donow.Customer_Interactions where CustomerName ='" + customerData.Name + "' and UserID=" + UserID;
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
                    customerData.CustomerInteractionList = customerInteractions;

                    //MeetingList 

                    List<MeetingList> meetingDetails = new List<MeetingList>();
                    SQL = "SELECT * FROM donow.ui_meetinglist where CustomerName = '" + customerData.Name + "' and UserID=" + UserID;
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
                                Status = reader["Status"].ToString(),
                                Comments = reader["Comments"].ToString()

                            });
                        }
                    }

                    customerData.UserMeetingList = meetingDetails;
                    //Deal History List
                    List<DealHistory> dealHistory = new List<DealHistory>();
                   
                    SQL = "SELECT LeadID,Date, UserID, IFNULL(Lead_City,'') as City,IFNULL(Lead_State, '') as State,IFNULL(CustomerName, '')  " 
                            + " as CustomerName,IFNULL(Lead_Industry, '') As Industry, IFNULL((Case when BrokerID = 0 THEN '' ELSE " 
                             + " (Select CONCAT(IFNULL(BROKER_FIRST_NAME, ''), ' ', IFNULL(BROKER_LAST_NAME, '')) FROM dn_broker_det_e "
                             + " Where ID = d.BrokerID) END),'') AS BROKER_NAME FROM donow.Deal_History d where CustomerName= '" + customerData.Name + "' and UserID = " + UserID;


                    cmd.CommandText = SQL;
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
                                LeadIndustry = reader["Industry"].ToString(),
                                BrokerName = reader["BROKER_NAME"].ToString()

                            });
                        }
                    }
                    customerData.DealHistoryList = dealHistory;

                    //Deal Maker data
                    DealMaker dealMaker = null;

                    SQL =  "SELECT A.ID AS BROKER_ID, CONCAT(IFNULL(A.BROKER_FIRST_NAME, ''), ' ', IFNULL(A.BROKER_LAST_NAME, '')) AS BROKER_NAME, IFNULL(A.BROKER_EMAIL,'') AS BROKER_EMAIL, "
                                    + " IFNULL(A.BROKER_STATE, '') AS STATE,IFNULL(A.BROKER_COUNTRY, '') AS COUNTRY,IFNULL(A.BROKER_INDUSTRY, '') AS INDUSTRY, "
                                    + " IFNULL(A.BROKER_PHONE_NO_1, '') AS PHONE_NO,  IFNULL(A.BROKER_COMPANY, '') AS COMPANY,   IFNULL(A.broker_city, '') AS CITY, "
                                    + " IFNULL(A.CREATE_TS, '') AS CREATE_TIME,     B.BROKER_SCORE,  "
                                    + " IFNULL(A.BROKER_FEE,'') AS BROKER_FEE,  IFNULL(A.BROKER_TOTAL_EARNING,'') AS BROKER_TOTAL_EARNING, "
                                    + " IFNULL(A.CONNECTION_TO_LEAD, '') AS CONNECTION_TO_LEAD, IFNULL(A.DOMAIN_EXPERTISE, '') AS DOMAIN_EXPERTISE, "
                                    + " IFNULL(A.BROKER_TITLE,'') AS BROKER_TITLE,  A.USER_ID AS BROKER_USERID,  IFNULL(A.LINE_OF_BUSINESS,'') AS LINE_OF_BUSINESS, B.Broker_Status AS BROKER_STATUS, "
                                    + " B.LEAD_ID, IFNULL(A.BROKER_ADDRESS_LINE1,'') AS "
                                    + " BROKER_COMP_ADDRESS , IFNULL(A.BROKER_ZIPCODE,'') AS BROKER_ZIPCODE, IFNULL(A.BROKER_COUNTRY,'') AS BROKER_COUNTRY, "
                                    + " IFNULL(A.Fiscal_Year_End,'') AS FiscalYE, IFNULL(A.Annual_Revenue,'') AS Revenue, IFNULL(A.Net_income,'') AS NetIncome, "
                                    + " IFNULL(A.Number_of_Employee,'') AS Employees, IFNULL(A.Market_Value,'') AS MarketValue, IFNULL(A.Year_Of_Founding,'') AS "
                                    + " YearFounded, IFNULL(A.DBPreScreen_Score,'') AS IndustryRiskScore, IFNULL(A.Lead_Comp_County,'') AS County, "
                                    + " IFNULL(A.Web_Address,'') AS WebAddress, B.Deals_closed  FROM dn_broker_det_e A INNER JOIN dn_broker_scoring_e B ON A.ID = B.BROKER_ID WHERE B.LEAD_ID=" + customerData.LeadId + "  and B.Broker_Status = 4 limit 1";

                    cmd.CommandText = SQL;
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dealMaker = new DealMaker
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
                                LeadID = long.Parse(reader["LEAD_ID"].ToString()),
                                DealsClosed = int.Parse(reader["Deals_closed"].ToString()),
                                ADDRESS = reader["BROKER_COMP_ADDRESS"].ToString(),
                                ZIPCODE = reader["BROKER_ZIPCODE"].ToString(),
                                COUNTRY = reader["BROKER_COUNTRY"].ToString(),
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
               
                customerData.dealMaker = dealMaker;
                }
                connection.Close();
            }
           
            t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            secondsSinceEpoch = (long)t.TotalMilliseconds;
            //string startTime = DateTime.Now.ToString() + " Milli" + DateTime.Now.Millisecond.ToString();
            string EndTime = secondsSinceEpoch.ToString();
            customerData.StartTime = startTime;
            customerData.EndTime = EndTime;
            return customerData; 
        }

        public List<CustomerMaster> Get(long UserID)
        {
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            long secondsSinceEpoch = (long)t.TotalMilliseconds;
            //string startTime = DateTime.Now.ToString() + " Milli" + DateTime.Now.Millisecond.ToString();
            string startTime = secondsSinceEpoch.ToString();

            List<CustomerMaster> customerList = new List<CustomerMaster>();
            using (MySqlConnection connection = new MySqlConnection(MyConnnectionString))  
            {
                connection.Open();

                using (MySqlCommand cmd = connection.CreateCommand())
                { 
                    cmd.CommandText = "select distinct IFNULL(LEAD_NAME, '') AS LEAD_NAME, IFNULL(LEAD_COMP_NAME,'') as LEAD_COMP_NAME from dn_lead_det_e a " 
                                     + " inner join dn_scoring_e b on a.id = b.lead_id where a.EXISTING_CUSTOMER = 'Y' and b.User_id = " + UserID + " and b.u_l_status in (4,3) order by LEAD_NAME";

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            customerList.Add(new CustomerMaster
                            {

                                //LeadId = long.Parse(reader["LEAD_ID"].ToString()),
                                Name = reader["LEAD_NAME"].ToString(),
                                Company = reader["LEAD_COMP_NAME"].ToString(),

                                //should be reoved added for identifying the performance 
                                StartTime = null,
                                EndTime = null

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


          /*  if (customerList != null)
            {
                customerList[0].StartTime = startTime;
                customerList[0].EndTime = EndTime;
            } */
                return customerList;
        }       
    }
}
