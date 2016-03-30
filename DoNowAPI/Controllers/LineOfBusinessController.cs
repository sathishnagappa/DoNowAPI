using DoNowAPI.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Configuration;

namespace DoNowAPI.Controllers
{
    public class LineOfBusinessController : ApiController
    {
        private string MyConnnectionString = ConfigurationManager.AppSettings["DoNowConnectionString"];

        [HttpGet]
        public IEnumerable<LineOfBusiness> Get()
         {

             List<LineOfBusiness> lineOfBusinessList = new List<LineOfBusiness>();
             using(MySqlConnection connection = new MySqlConnection(MyConnnectionString))
            {
                connection.Open();

                using (MySqlCommand cmd = connection.CreateCommand())
                { 
                    cmd.CommandText = "SELECT IFNULL(IndustryName,'') IndustryName, IFNULL(LineOfBusiness,'') LineOfBusiness  FROM ui_lineofbusiness";

                 using (MySqlDataReader reader = cmd.ExecuteReader())
                 {
                     while (reader.Read())
                     {
                         lineOfBusinessList.Add(new LineOfBusiness
                         {
                             IndustryName = reader["IndustryName"].ToString(),
                             LineofBusiness = reader["LineOfBusiness"].ToString()
                         });
                     }
                 }

             }
                connection.Close();
            }
             
             return lineOfBusinessList.ToArray();

         }
    }
}
