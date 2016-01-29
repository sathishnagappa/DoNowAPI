using System;
using System.Web.Http;
using MySql.Data.MySqlClient;
using ContactList.Models;
using System.Configuration;

namespace DoNowAPI.Controllers
{
    public class SFDCController : ApiController
    {
        private string MyConnnectionString = ConfigurationManager.AppSettings["DoNowConnectionString"];
        [HttpGet]
        public SFDC Get(long Id)
        {
            SFDC SFDCDetails = null;
            using(MySqlConnection connection = new MySqlConnection(MyConnnectionString))
            {
                connection.Open();

                using (MySqlCommand cmd = connection.CreateCommand())
                { 
                    cmd.CommandText = "SELECT UserID,IFNULL(Url, '') As Url, IFNULL(UserName,'') as UserName,IFNULL(Password, '') As Password, IFNULL(SecurityCode,'') as SecurityCode,IFNULL(ClientID, '') as ClientID,IFNULL(ClientSecret, '') as ClientSecret FROM User_SFDCCredentials U where UserID =" + Id;

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SFDCDetails = new SFDC
                        {
                            UserID = long.Parse(reader["UserID"].ToString()),
                            Url = reader["Url"].ToString(),
                            UserName = reader["UserName"].ToString(),
                            Password = reader["Password"].ToString(),
                            SecurityCode = reader["SecurityCode"].ToString(),
                            ClientID = reader["ClientID"].ToString(),
                            ClientSecret = reader["ClientSecret"].ToString()                           
                        };
                    }
                }
                }
                connection.Close();
            }
            return SFDCDetails;
        }
    }
}
