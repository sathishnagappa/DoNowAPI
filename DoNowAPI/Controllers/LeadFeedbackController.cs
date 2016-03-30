using DoNowAPI.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Configuration;

namespace DoNowAPI.Controllers
{
    public class LeadFeedbackController : ApiController
    {
        private string MyConnnectionString = ConfigurationManager.AppSettings["DoNowConnectionString"];

       
        [HttpGet]      
        public List<LeadFeedback> Get(long id)
        {

            List<LeadFeedback> leadFeedback = new List<LeadFeedback>();
            using (MySqlConnection connection = new MySqlConnection(MyConnnectionString))
            {
                connection.Open();
                using (MySqlCommand cmd = connection.CreateCommand())
                {
                    string stringSQL;
                    stringSQL = "SELECT LeadID, UserID, MeetingID, IFNULL(InteractionFeedBack,'') AS InteractionFeedBack, IFNULL(ReasonForDown,'') AS ReasonForDown, "
                                    + " IFNULL(CustomerAcknowledged,'') AS CustomerAcknowledged, IFNULL(Comments,'') AS Comments, "
                                    + " IFNULL(SalesStage,'') AS SalesStage FROM lead_feedback where LeadId =" + id;

                    cmd.CommandText = stringSQL;

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            leadFeedback.Add(new LeadFeedback
                            {
                                LeadId = long.Parse(reader["LeadId"].ToString()),
                                UserID = long.Parse(reader["UserID"].ToString()),
                                InteractionFeedBack = reader["InteractionFeedBack"].ToString(),
                                ReasonForDown = reader["ReasonForDown"].ToString(),
                                CustomerAcknowledged = reader["CustomerAcknowledged"].ToString(),
                                Comments = reader["Comments"].ToString(),
                                MeetingID = long.Parse(reader["MeetingID"].ToString()),
                                SalesStage = reader["SalesStage"].ToString()

                            });
                        }
                    }
                }
                connection.Close();
            }
            return leadFeedback;

        }

        //// POST api/values
        public long Post([FromBody]LeadFeedback value)
        {
           
            long refID = -1;

           using(MySqlConnection connection = new MySqlConnection(MyConnnectionString))
            {
                connection.Open();
                using (MySqlCommand cmd = connection.CreateCommand())
                {  
                cmd.CommandText = "UPDATE dn_lead_e Set LEAD_STATUS_C = '" + value.SalesStage +  "' where ID=" + value.LeadId;
                cmd.ExecuteNonQuery();

                
                cmd.CommandText = "select IFNULL(max(ID),0) from lead_feedback";
                refID = long.Parse(cmd.ExecuteScalar().ToString()) + 1;
              
                cmd.CommandText = "INSERT INTO lead_feedback values(@LeadID, @UserID, @InteractionFeedBack, @ReasonForDown,@CustomerAcknowledged, @Comments,@ID, @MeetingID,@SalesStage)"; 
                cmd.Parameters.AddWithValue("@ID", refID);
                cmd.Parameters.AddWithValue("@LeadID", value.LeadId);
                cmd.Parameters.AddWithValue("@UserID", value.UserID);
                cmd.Parameters.AddWithValue("@InteractionFeedBack", value.InteractionFeedBack);
                cmd.Parameters.AddWithValue("@ReasonForDown", value.ReasonForDown);
                cmd.Parameters.AddWithValue("@CustomerAcknowledged", value.CustomerAcknowledged);
                cmd.Parameters.AddWithValue("@Comments", value.Comments);
                cmd.Parameters.AddWithValue("@MeetingID", value.MeetingID);
                cmd.Parameters.AddWithValue("@SalesStage", value.SalesStage);
                cmd.ExecuteNonQuery();
            }
                connection.Close();
            }
         
            return refID;

        }
    }
}
