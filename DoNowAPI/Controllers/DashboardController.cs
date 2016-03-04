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
                                crm_total_leads = (int)reader["crm_total_leads"],
                                crm_leads_with_dealmakers = (int)reader["crm_leads_with_dealmakers"],
                                crm_leads_without_dealmakers = (int)reader["crm_leads_without_dealmakers"],
                                dn_total_leads = (int)reader["dn_total_leads"],
                                dn_total_leads_accepted = (int)reader["dn_total_leads_accepted"],
                                dn_leads_with_dealmakers = (int)reader["dn_leads_with_dealmakers"],
                                dn_leads_without_dealmakers = (int)reader["dn_leads_without_dealmakers"],
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
                                Comments = reader["Comments"].ToString()

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
