using DoNowAPI.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;
using MySql.Data.MySqlClient;
using System.Configuration;
namespace DoNowAPI.Controllers
{
    public class DealMakerController : ApiController
    {
        private string MyConnnectionString = ConfigurationManager.AppSettings["DoNowConnectionString"];

              
        [HttpGet]
        public List<DealMaker> Get(long Id,int UserId,string type)
        {
           /* TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            long secondsSinceEpoch = (long)t.TotalMilliseconds;
            //string startTime = DateTime.Now.ToString() + " Milli" + DateTime.Now.Millisecond.ToString();
            string startTime = secondsSinceEpoch.ToString();*/
          
            List<DealMaker> dealMaker = new List<DealMaker>();
            using (MySqlConnection connection = new MySqlConnection(MyConnnectionString))
            {
                connection.Open();
                using (MySqlCommand cmd = connection.CreateCommand())
                { 

                    string stringSQL;
                stringSQL = " SELECT A.ID AS BROKER_ID, CONCAT(IFNULL(A.BROKER_FIRST_NAME, ''), ' ',IFNULL(A.BROKER_LAST_NAME, '')) AS BROKER_NAME, "
                                + " IFNULL(A.BROKER_EMAIL,'') AS BROKER_EMAIL, IFNULL(A.BROKER_STATE, '') AS STATE,IFNULL(A.BROKER_COUNTRY, '') AS COUNTRY, "
                                + " IFNULL(A.BROKER_INDUSTRY, '') AS INDUSTRY, IFNULL(A.BROKER_PHONE_NO_1, '') AS PHONE_NO, "
                                + " IFNULL(A.BROKER_COMPANY, '') AS COMPANY, IFNULL(A.broker_city, '') AS CITY, IFNULL(A.CREATE_TS, '') AS CREATE_TIME, "
                                + " B.BROKER_SCORE, IFNULL(A.BROKER_FEE,'') AS BROKER_FEE, IFNULL(A.BROKER_TOTAL_EARNING,'') AS BROKER_TOTAL_EARNING, "
                                + " IFNULL(A.CONNECTION_TO_LEAD, '') AS CONNECTION_TO_LEAD, IFNULL(A.DOMAIN_EXPERTISE, '') AS DOMAIN_EXPERTISE, "
                                + " IFNULL(A.BROKER_TITLE,'') AS BROKER_TITLE, A.USER_ID AS BROKER_USERID, IFNULL(A.LINE_OF_BUSINESS,'') AS LINE_OF_BUSINESS, "
                                + " B.Broker_Status AS BROKER_STATUS, B.LEAD_ID, IFNULL(A.BROKER_ADDRESS_LINE1,'') AS "
                                + " BROKER_COMP_ADDRESS , IFNULL(A.BROKER_ZIPCODE,'') AS BROKER_ZIPCODE, IFNULL(A.BROKER_COUNTRY,'') AS BROKER_COUNTRY, "
                                + " IFNULL(A.Fiscal_Year_End,'') AS FiscalYE, IFNULL(A.Annual_Revenue,'') AS Revenue, IFNULL(A.Net_income,'') AS NetIncome, "
                                + " IFNULL(A.Number_of_Employee,'') AS Employees, IFNULL(A.Market_Value,'') AS MarketValue, IFNULL(A.Year_Of_Founding,'') AS "
                                + " YearFounded, IFNULL(A.DBPreScreen_Score,'') AS IndustryRiskScore, IFNULL(A.Lead_Comp_County,'') AS County, "
                                + " IFNULL(A.Web_Address,'') AS WebAddress, B.Deals_closed  FROM dn_broker_det_e A INNER JOIN dn_broker_scoring_e B ON A.ID = B.BROKER_ID "
                                + " WHERE B.LEAD_ID=" + Id + " and A.USER_ID <>" + UserId;


                    cmd.CommandText = stringSQL;
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
                            LeadID = long.Parse(reader["LEAD_ID"].ToString()),
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
                            WebAddress = reader["WebAddress"].ToString(),
                            DealsClosed = int.Parse(reader["Deals_closed"].ToString())

                        });
                    }
                }
                }
                connection.Close();
            }
            
          /*  t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            secondsSinceEpoch = (long)t.TotalMilliseconds;
            //string startTime = DateTime.Now.ToString() + " Milli" + DateTime.Now.Millisecond.ToString();
            string EndTime = secondsSinceEpoch.ToString();
            if (dealMaker != null)
            {
                dealMaker[0].StartTime = startTime;
                dealMaker[0].EndTime = EndTime;

            } **/
            return dealMaker;
        }

        [HttpGet]
        public List<DealMaker> Get(string IndustryName, string LOB,int UserId)
        {   List<DealMaker> dealMaker = new List<DealMaker>();
            using (MySqlConnection connection = new MySqlConnection(MyConnnectionString) ) 
            {
                connection.Open();
                using (MySqlCommand cmd = connection.CreateCommand())
                {  
                        string stringSQL;
                    stringSQL = "SELECT A.ID AS BROKER_ID, CONCAT (IFNULL(A.BROKER_FIRST_NAME,''),' ', IFNULL(A.BROKER_LAST_NAME,'') ) AS BROKER_NAME, IFNULL(A.BROKER_EMAIL,'')  AS BROKER_EMAIL, "
                  + " IFNULL(A.BROKER_STATE,'') AS STATE, IFNULL(A.BROKER_COUNTRY,'') AS COUNTRY, IFNULL(A.BROKER_INDUSTRY,'') AS INDUSTRY, "
                  + " IFNULL(A.BROKER_PHONE_NO_1,'') AS PHONE_NO, IFNULL(A.BROKER_COMPANY,'') AS COMPANY, IFNULL(A.broker_city,'') AS CITY, "
                  + " IFNULL(A.BROKER_FEE,'') AS BROKER_FEE,  IFNULL(A.BROKER_TOTAL_EARNING,'') AS BROKER_TOTAL_EARNING, IFNULL(A.CONNECTION_TO_LEAD, '') AS CONNECTION_TO_LEAD, IFNULL(A.DOMAIN_EXPERTISE, '') AS DOMAIN_EXPERTISE, "
                  + " IFNULL(A.BROKER_TITLE,'') AS BROKER_TITLE,  A.USER_ID AS BROKER_USERID,  IFNULL(A.LINE_OF_BUSINESS,'') AS LINE_OF_BUSINESS, B.Broker_Status AS BROKER_STATUS, B.LEAD_ID, "
                  + " IFNULL(A.CREATE_TS,'') AS CREATE_TIME, B.BROKER_SCORE, B.Deals_closed FROM dn_broker_det_e A "
                  + " INNER JOIN dn_broker_scoring_e B ON A.ID = B.BROKER_ID "
                  + " WHERE A.BROKER_INDUSTRY LIKE '%" + IndustryName + "%' and A.LINE_OF_BUSINESS  LIKE '%" + LOB + "%' and A.USER_ID <> " + UserId;

               
                    cmd.CommandText = stringSQL;
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
                                LeadID = long.Parse(reader["LEAD_ID"].ToString()),
                                DealsClosed = int.Parse(reader["Deals_closed"].ToString())

                            });
                        }
                    }
                }
                connection.Close();
            }
          
            return dealMaker;
        }

      
        [HttpGet]
        public List<DealMaker> Get(long LeadID, int BrokerStatus)
        {
            List<DealMaker> dealMaker = new List<DealMaker>();
            using (MySqlConnection connection = new MySqlConnection(MyConnnectionString) )
            {
                connection.Open();
                using (MySqlCommand cmd = connection.CreateCommand() )
                {
                    string stringSQL = "SELECT A.ID AS BROKER_ID, CONCAT(IFNULL(A.BROKER_FIRST_NAME, ''), ' ',IFNULL(A.BROKER_LAST_NAME, '')) AS BROKER_NAME, IFNULL(A.BROKER_EMAIL,'') AS BROKER_EMAIL, "
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
                                            + " IFNULL(A.Web_Address,'') AS WebAddress, B.Deals_closed     FROM dn_broker_det_e A INNER JOIN dn_broker_scoring_e B ON A.ID = B.BROKER_ID WHERE B.LEAD_ID=" + LeadID + "  and B.Broker_Status = " + BrokerStatus + " limit 1";


                    cmd.CommandText = stringSQL;
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


                            });
                        }
                    }
                }
                connection.Close();
            }
           
            return dealMaker;
        }


        [HttpGet]
        public List<DealMaker> Get(long BrokerID, string IDType)
        {
            List<DealMaker> dealMaker = new List<DealMaker>();
            using(MySqlConnection connection = new MySqlConnection(MyConnnectionString))
            {
                connection.Open();
                using(MySqlCommand cmd = connection.CreateCommand())
                { 
                string stringSQL = "SELECT A.ID AS BROKER_ID, CONCAT(IFNULL(A.BROKER_FIRST_NAME, ''),' ',IFNULL(A.BROKER_LAST_NAME, '')) AS BROKER_NAME, IFNULL(A.BROKER_EMAIL,'') AS BROKER_EMAIL, "
                    + " IFNULL(A.BROKER_STATE, '') AS STATE,IFNULL(A.BROKER_COUNTRY, '') AS COUNTRY,IFNULL(A.BROKER_INDUSTRY, '') AS INDUSTRY, "
                    + " IFNULL(A.BROKER_PHONE_NO_1, '') AS PHONE_NO,  IFNULL(A.BROKER_COMPANY, '') AS COMPANY,   IFNULL(A.broker_city, '') AS CITY, "
                    + " IFNULL(A.CREATE_TS, '') AS CREATE_TIME,     B.BROKER_SCORE,  "
                    + " IFNULL(A.BROKER_FEE,'') AS BROKER_FEE,  IFNULL(A.BROKER_TOTAL_EARNING,'') AS BROKER_TOTAL_EARNING, "
                    + " IFNULL(A.CONNECTION_TO_LEAD, '') AS CONNECTION_TO_LEAD, IFNULL(A.DOMAIN_EXPERTISE, '') AS DOMAIN_EXPERTISE, "
                     + " IFNULL(A.BROKER_TITLE,'') AS BROKER_TITLE,  A.USER_ID AS BROKER_USERID,  IFNULL(A.LINE_OF_BUSINESS,'') AS LINE_OF_BUSINESS, B.Broker_Status AS BROKER_STATUS, "
                      + " B.LEAD_ID,B.Deals_closed  FROM dn_broker_det_e A INNER JOIN dn_broker_scoring_e B ON A.ID = B.BROKER_ID WHERE A.USER_ID=" + BrokerID;

               
                cmd.CommandText = stringSQL;
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
                            LeadID = long.Parse(reader["LEAD_ID"].ToString()),
                            DealsClosed = int.Parse(reader["Deals_closed"].ToString())

                        });
                    }
                }
                }
                connection.Close();
            }
           
            return dealMaker;
        }

        //// POST api/values
        public int Post([FromBody]DealMaker value)
        {  
            string stringSQL;
            int result = -1;
            using (MySqlConnection connection = new MySqlConnection(MyConnnectionString) )
            {
                connection.Open();
                using(MySqlCommand cmd = connection.CreateCommand())
                { 
                    stringSQL = "Update dn_broker_scoring_e set Broker_Status =" + value.Status + " where LEAD_ID=" + value.LeadID + " and BROKER_ID=" + value.BrokerID;
                    cmd.CommandText = stringSQL;
                    result = cmd.ExecuteNonQuery();
                }
                connection.Close();
            }           
            return result;
        }

    }
}
