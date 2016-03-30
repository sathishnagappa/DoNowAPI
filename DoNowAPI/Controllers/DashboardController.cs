using DoNowAPI.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace DoNowAPI.Controllers
{
    public class DashboardController : ApiController
    {
        private string MyConnnectionString = ConfigurationManager.AppSettings["DoNowConnectionString"];
        //System.Web.HttpContext.Current.Application["ConnectionString"].ToString();

        [HttpGet]
        public Dashboard Get(long id)
        {
            Dashboard DashboardDetails = null;
            using (MySqlConnection connection = new MySqlConnection(MyConnnectionString))
            {
                connection.Open();

                using (MySqlCommand cmd = connection.CreateCommand())
                {
                    string stringSQL = "dn_dashborad_show";

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = stringSQL;
                    cmd.Parameters.AddWithValue("@USERID", id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DashboardDetails = new Dashboard
                            {
                                total_customers = (int)reader["total_customers"],                               
                                total_earning = reader["total_earning"].ToString(),
                                total_lead_requests = (int)reader["total_lead_requests"],
                                total_accepted = (int)reader["total_accepted"],
                                total_referred = (int)reader["total_referred"],
                                next_meeting = reader["next_meeting"].ToString(),
                                title = reader["title"].ToString(),
                                CustomerName = reader["CustomerName"].ToString(),
                                City = reader["City"].ToString(),
                                State = reader["State"].ToString(),
                                MeetingID = int.Parse(reader["MeetingID"].ToString()),
                                Comments = reader["Comments"].ToString(),
                                CRMClosedWon = (int)reader["CRMClosedWonCount"],
                                CRMProposal = (int)reader["CRMProposalCount"],
                                CRMConnectionMade = (int)reader["CRMConnectionMadeCount"],
                                CRMWorking = (int)reader["CRMWorkingCount"],
                                CRMNew = (int)reader["CRMNewCount"],
                                DoNowClosedWon = (int)reader["DoNowClosedWonCount"],
                                DoNowProposal = (int)reader["DoNowProposalCount"],
                                DoNowConnectionMade = (int)reader["DoNowConnectionMadeCount"],
                                DoNowWorking = (int)reader["DoNowWorkingCount"],
                                DoNowNew = (int)reader["DoNowNewCount"]

                            };
                        }
                    }
                }
                connection.Close();
            }
            return DashboardDetails;
        }

    }
}
