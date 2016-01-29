using System;
using System.Web.Http;
using MySql.Data.MySqlClient;
using System.Configuration;
using DoNowAPI.Models;

namespace DoNowAPI.Controllers
{
    public class UserUpdateController : ApiController
    {
        private string MyConnnectionString = ConfigurationManager.AppSettings["DoNowConnectionString"];
        
        [HttpPost]
        //update based on userid for all the fields
        public int Post([FromBody]UserDetails value)
        {   
            string stringSQL;
            int result = -1;
            using (MySqlConnection connection = new MySqlConnection(MyConnnectionString))
            {
                connection.Open();

                using (MySqlCommand cmd = connection.CreateCommand())
                { 
                    stringSQL = "Update donow.user_details set User_Name = '" + value.Name + "', "
                            + " Password = '" + value.Password + "',"
                            + " Full_Name = '" + value.FullName + "',"
                            + "Title ='" + value.Title + "',"
                            + "Company ='" + value.Company + "',"
                            + "Industry = '" + value.Industry + "',"
                             + "Office_Address = '" + value.OfficeAddress + "',"
                             + "City= '" + value.City + "',"
                             + "State= '" + value.State + "',"
                          + "Zip= '" + value.Zip + "',"
                           + "Phone= '" + value.Phone + "',"
                           + "Email= '" + value.Email + "',"
                           + "LINE_OF_BUSINESS= '" + value.LineOfBusiness + "',"
                           + "Preferred_Industry= '" + value.PreferredIndustry + "',"
                           + "Preferred_Company= '" + value.PreferredCompany + "',"
                            + "Preferred_Customers= '" + value.PreferredCustomers + "',"
                            + "IsNewLeadNotificationRequired= '" + value.IsNewLeadNotificationRequired + "',"
                             + "IsReferrelRequestRequired= '" + value.IsReferralRequestRequired + "',"
                              + "IsCustomerFollowUpRequired= '" + value.IsCustomerFollowUpRequired + "',"
                              + "IsMeetingRemindersRequired= '" + value.IsMeetingRemindersRequired + "',"
                              + "IsBusinessUpdatesRequired= '" + value.IsBusinessUpdatesRequired + "',"
                              + "ImageURL= '" + value.ImageUrl + "' "
                              + "where ID=" + value.UserId;
                cmd.CommandText = stringSQL;
                result = cmd.ExecuteNonQuery();

                }
                connection.Close();
            }
               
            return result;
        }
     
    }
}
