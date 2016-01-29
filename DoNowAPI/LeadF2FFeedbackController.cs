using ContactList.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ContactList.Controllers
{
    public class LeadF2FFeedbackController : ApiController
    {
        //private const string MyConnnectionString = "server=donowlvm.cloudapp.net;Port=3307;Database=donow;Uid=root;Pwd=rootdonow;";

        private const string EndpointUrl = @"https://donowdocdb.documents.azure.com:443/";
        private const string AuthorizationKey = "mLpM3D6rEgkp2onAy3EYJZn+dIrZmcv8IqqN8LXAPuoLURnUtm2DOoXElcKltulWgg0T3H2cj8LveHjzMAt8CQ==";
        private const string MyConnnectionString = "server=donowlvm.cloudapp.net;Port=3307;Database=donow;Uid=root;Pwd=rootdonow;";

        [HttpGet]
        public IEnumerable<LeadF2FFeadback> Get()
        {
            MySqlConnection connection = new MySqlConnection(MyConnnectionString);
            MySqlCommand cmd;
            connection.Open();

            List<LeadF2FFeadback> feedBackList = new List<LeadF2FFeadback>();
            try
            {

                cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM donow.lead_f2f_feedback";

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        feedBackList.Add(new LeadF2FFeadback
                        {
                            //LeadId = (int)reader["LeadID"],
                            LeadId = (int)reader["ID"],
                            AnswerType = (int)reader["AnswerType"],
                            Comments = reader["Comments"].ToString(),
                            Options = (int)reader["Options"],
                            QuestionNo = (int)reader["QuestionNo"]
                        });

                    }
                }

            }
            catch (Exception)
            {

            }
            connection.Close();

            return feedBackList.ToArray();

            // return new LeadFeedback[]{
            //    new LeadFeedback {   LeadId = 5 , AnswerType = 1, Comments = "mandatory", Options = 4, QuestionNo = 2},
            //    new LeadFeedback {   LeadId = 2 , AnswerType = 2, Comments = "mandatory", Options = 3, QuestionNo = 3}
            //};
        }

        //lead id will be passed
        [HttpGet]
        public LeadF2FFeadback Get(int LeadId)
        {
            MySqlConnection connection = new MySqlConnection(MyConnnectionString);
            MySqlCommand cmd;
            connection.Open();

            LeadF2FFeadback leadFeedback = null;
            try
            {

                cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM donow.lead_f2f_feedback where ID =" + LeadId.ToString();
                //cmd.CommandText = "SELECT * FROM donow.lead_f2f_feedback where id =" + id;

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        leadFeedback = new LeadF2FFeadback
                        {
                            LeadId = (int)reader["ID"],
                            //LeadId = (int)reader["LeadId"],
                            QuestionNo = (int)reader["QuestionNo"],
                            AnswerType = (int)reader["AnswerType"],
                            Options = (int)reader["Options"],
                            Comments = reader["Comments"].ToString()
                        };
                    }
                }
            }
            catch (Exception)
            {

            }
            connection.Close();

            return leadFeedback;
        }

        //// POST api/values
        //public long Post([FromBody]LeadF2FFeadback value)
        //{
        //    return -1;
        //}
    }
}
