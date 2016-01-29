using DoNowAPI.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Configuration;

namespace DoNowAPI.Controllers
{
    public class LeadF2FFeedbackController : ApiController
    {      
        private string MyConnnectionString = ConfigurationManager.AppSettings["DoNowConnectionString"];
       
        [HttpGet]
        public List<LeadF2FFeadback> Get(long id)
        {
            List<LeadF2FFeadback> leadFeedback = new List<LeadF2FFeadback>();
            using (MySqlConnection connection = new MySqlConnection(MyConnnectionString))
            {
                connection.Open();
                using (MySqlCommand cmd = connection.CreateCommand())
                {
                    string stringSQL;
                    stringSQL = " SELECT LeadID,USERID,MeetingID, IFNULL(ConfirmMeeting,'') AS ConfirmMeeting, IFNULL(ReasonForDown,'') AS ReasonForDown, "
                                   + " IFNULL(MeetingInfoHelpFull,'') AS MeetingInfoHelpFull,  IFNULL(LeadAdvanced,'') AS LeadAdvanced, "
                                   + " IFNULL(CustomerCategorization,'') AS CustomerCategorization, IFNULL(SalesStage,'') AS SalesStage, "
                                   + " IFNULL(NextSteps,'') AS NextSteps FROM donow.lead_f2f_feedback  where LeadID =" + id;


                    cmd.CommandText = stringSQL;

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            leadFeedback.Add(new LeadF2FFeadback
                            {
                                LeadId = long.Parse(reader["LeadID"].ToString()),
                                UserID = long.Parse(reader["USERID"].ToString()),
                                ConfirmMeeting = reader["ConfirmMeeting"].ToString(),
                                ReasonForDown = reader["ReasonForDown"].ToString(),
                                MeetingInfoHelpFull = reader["MeetingInfoHelpFull"].ToString(),
                                LeadAdvanced = reader["LeadAdvanced"].ToString(),
                                CustomerCategorization = reader["CustomerCategorization"].ToString(),
                                SalesStage = reader["SalesStage"].ToString(),
                                NextSteps = reader["NextSteps"].ToString(),
                                MeetingID = long.Parse(reader["MeetingID"].ToString())

                            });
                        }
                    }
                }
                connection.Close();
            }          

            return leadFeedback;
        }

        //// POST api/values
        public long Post([FromBody]LeadF2FFeadback value)
        {
            long refID = -1;

            using (MySqlConnection connection = new MySqlConnection(MyConnnectionString))
            {
                connection.Open();
                using (MySqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "UPDATE dn_lead_e Set LEAD_STATUS_C = '" + value.SalesStage + "' where ID=" + value.LeadId;
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "select IFNULL(max(ID),0) from donow.lead_f2f_feedback ";
                    refID = long.Parse(cmd.ExecuteScalar().ToString()) + 1;


                    cmd.CommandText = "INSERT INTO donow.lead_f2f_feedback values(@LeadID, @UserID, @ConfirmMeeting, @ReasonForDown,@MeetingInfoHelpFull, @LeadAdvanced,@CustomerCategorization,@SalesStage,@NextSteps,@ID,@MeetingID)";
                    cmd.Parameters.AddWithValue("@ID", refID);
                    cmd.Parameters.AddWithValue("@LeadID", value.LeadId);
                    cmd.Parameters.AddWithValue("@UserID", value.UserID);
                    cmd.Parameters.AddWithValue("@ConfirmMeeting", value.ConfirmMeeting);
                    cmd.Parameters.AddWithValue("@ReasonForDown", value.ReasonForDown);
                    cmd.Parameters.AddWithValue("@MeetingInfoHelpFull", value.MeetingInfoHelpFull);
                    cmd.Parameters.AddWithValue("@LeadAdvanced", value.LeadAdvanced);
                    cmd.Parameters.AddWithValue("@CustomerCategorization", value.CustomerCategorization);
                    cmd.Parameters.AddWithValue("@SalesStage", value.SalesStage);
                    cmd.Parameters.AddWithValue("@NextSteps", value.NextSteps);
                    cmd.Parameters.AddWithValue("@MeetingID", value.MeetingID);
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
            return refID;
        }
        
    }
}
