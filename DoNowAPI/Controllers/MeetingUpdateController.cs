using ContactList.Models;
using MySql.Data.MySqlClient;
using System;
using System.Web.Http;
using System.Configuration;


namespace DoNowAPI.Controllers
{
    public class MeetingUpdateController : ApiController
    {
        private string MyConnnectionString = ConfigurationManager.AppSettings["DoNowConnectionString"];
        public int Post([FromBody]MeetingList value)
        {  
            string stringSQL;
            int result = -1;
            using(MySqlConnection connection = new MySqlConnection(MyConnnectionString))
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
    }
}
