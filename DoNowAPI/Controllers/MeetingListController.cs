using DoNowAPI.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Configuration;

namespace DoNowAPI.Controllers
{
    public class MeetingListController : ApiController
    {
        private string MyConnnectionString = ConfigurationManager.AppSettings["DoNowConnectionString"];

        public List<MeetingList> GetMeetingByID(long UserID)
        {
            List<MeetingList> meetingDetails = new List<MeetingList>();
            using (MySqlConnection connection = new MySqlConnection(MyConnnectionString))
            {
                connection.Open();

                using (MySqlCommand cmd = connection.CreateCommand())
                { 
                    cmd.CommandText = "SELECT * FROM donow.ui_meetinglist where  Status <> 'Done' and UserID =" + UserID;

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
                }
                connection.Close();
            }
          
            return meetingDetails;
        }

        public List<MeetingList> GetMeetingByName(string name)
        {
            List<MeetingList> meetingDetails = new List<MeetingList>();
           
           using(MySqlConnection connection = new MySqlConnection(MyConnnectionString))
            {
                connection.Open();

                using(MySqlCommand cmd = connection.CreateCommand())
               { 
                cmd.CommandText = "SELECT * FROM donow.ui_meetinglist where CustomerName ='" + name + "'";

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
                }
                connection.Close();
            }
           

            return meetingDetails;
        }

        [HttpPut]
        public int Put([FromBody]MeetingList value)
        {  
            string stringSQL;
            int result = -1;
            using (MySqlConnection connection = new MySqlConnection(MyConnnectionString))
            {
                connection.Open();
                using (MySqlCommand cmd = connection.CreateCommand())
                { 
                    stringSQL = "UPDATE ui_meetinglist set status='" + value.Status + "' where ID= " + value.Id;
                    cmd.CommandText = stringSQL;
                    result = cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
           
            return result;
        }
        
        // POST api/values
        public long Post([FromBody]MeetingList value)
        {   
            long ID = -1;

            using (MySqlConnection connection = new MySqlConnection(MyConnnectionString))
            {
                connection.Open();

                using (MySqlCommand cmd = connection.CreateCommand())
                { cmd.CommandText = "SELECT max(ID) FROM donow.ui_meetinglist";
                    ID = long.Parse(cmd.ExecuteScalar().ToString()) + 1;
                    cmd.CommandText = "INSERT INTO donow.ui_meetinglist VALUES(@ID,@UserID,@LeadID,@Subject,@StartDate,@EndDate,@CustomerName,@City,@State,@Status,@Comments)";

                    cmd.Parameters.AddWithValue("@ID", int.Parse(ID.ToString()));
                    cmd.Parameters.AddWithValue("@UserID", value.UserId);
                    cmd.Parameters.AddWithValue("@LeadID", value.LeadId);
                    cmd.Parameters.AddWithValue("@Subject", value.Subject);
                    cmd.Parameters.AddWithValue("@StartDate", value.StartDate);
                    cmd.Parameters.AddWithValue("@EndDate", value.EndDate);
                    cmd.Parameters.AddWithValue("@CustomerName", value.CustomerName);
                    cmd.Parameters.AddWithValue("@City", value.City);
                    cmd.Parameters.AddWithValue("@State", value.State);
                    cmd.Parameters.AddWithValue("@Status", value.Status);
                    cmd.Parameters.AddWithValue("@Comments", value.Status);

                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
          
            return ID;
        }
    }
}
