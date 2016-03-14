using DoNowAPI.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;
using MySql.Data.MySqlClient;
using System.Configuration;


namespace DoNowAPI.Controllers
{
    public class UserDetailsController : ApiController
    {
        private string MyConnnectionString = ConfigurationManager.AppSettings["DoNowConnectionString"];

        [HttpGet]
        public IEnumerable<UserDetails> Get()
        {
            List<UserDetails> userDetailsList = new List<UserDetails>();
            using (MySqlConnection connection = new MySqlConnection(MyConnnectionString))
            {
                connection.Open();
                using (MySqlCommand cmd = connection.CreateCommand())
                { 
                    cmd.CommandText = "SELECT * FROM donow.user_details";

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            userDetailsList.Add(new UserDetails
                            {
                                UserId = (int)reader["ID"],
                                City = reader["City"].ToString(),
                                Company = reader["Company"].ToString(),
                                Email = reader["Email"].ToString(),
                                FullName = reader["Full_Name"].ToString(),
                                ImageUrl = reader["ImageURL"].ToString(),
                                Industry = reader["Industry"].ToString(),
                                IsBusinessUpdatesRequired = (bool)reader["IsBusinessUpdatesRequired"],
                                IsCustomerFollowUpRequired = (bool)reader["IsCustomerFollowUpRequired"],
                                IsMeetingRemindersRequired = (bool)reader["IsMeetingRemindersRequired"],
                                IsNewLeadNotificationRequired = (bool)reader["IsNewLeadNotificationRequired"],
                                IsReferralRequestRequired = (bool)reader["IsReferrelRequestRequired"],
                                Name = reader["User_Name"].ToString(),
                                OfficeAddress = reader["Office_Address"].ToString(),
                                Password = reader["Password"].ToString(),
                                Phone = reader["Office_Address"].ToString(),
                                PreferredCompany = reader["Preferred_Company"].ToString(),
                                PreferredCustomers = reader["Preferred_Customers"].ToString(),
                                PreferredIndustry = reader["Preferred_Industry"].ToString(),
                                State = reader["State"].ToString(),
                                Title = reader["Title"].ToString(),
                                LineOfBusiness = reader["LINE_OF_BUSINESS"].ToString(),
                                Zip = reader["Zip"].ToString()

                            });
                        }
                    }
                }
                connection.Close();
            }

            return userDetailsList.ToArray();

        }


        public UserDetails GetEmailID(string EmailID)
        {
            UserDetails userDetails = null;
            using (MySqlConnection connection = new MySqlConnection(MyConnnectionString))
            {
                connection.Open();

                using (MySqlCommand cmd = connection.CreateCommand())
                { 
                    //cmd.CommandText = "SELECT * FROM donow.user_details where Email='" + EmailID.ToString() + "'";
                    string stringSQL = "dn_GetUserByEmail";
                    cmd.CommandText = stringSQL;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", EmailID);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        userDetails = new UserDetails
                        {
                            UserId = (int)reader["ID"],
                            City = reader["City"].ToString(),
                            Company = reader["Company"].ToString(),
                            Email = reader["Email"].ToString(),
                            FullName = reader["Full_Name"].ToString(),
                            ImageUrl = reader["ImageURL"].ToString(),
                            Industry = reader["Industry"].ToString(),
                            IsBusinessUpdatesRequired = bool.Parse(reader["IsBusinessUpdatesRequired"].ToString()),
                            IsCustomerFollowUpRequired = bool.Parse(reader["IsCustomerFollowUpRequired"].ToString()),
                            IsMeetingRemindersRequired = bool.Parse(reader["IsMeetingRemindersRequired"].ToString()),
                            IsNewLeadNotificationRequired = bool.Parse(reader["IsNewLeadNotificationRequired"].ToString()),
                            IsReferralRequestRequired = bool.Parse(reader["IsReferrelRequestRequired"].ToString()),
                            Name = reader["User_Name"].ToString(),
                            OfficeAddress = reader["Office_Address"].ToString(),
                            Password = reader["Password"].ToString(),
                            Phone = reader["Office_Address"].ToString(),
                            PreferredCompany = reader["Preferred_Company"].ToString(),
                            PreferredCustomers = reader["Preferred_Customers"].ToString(),
                            PreferredIndustry = reader["Preferred_Industry"].ToString(),
                            State = reader["State"].ToString(),
                            Title = reader["Title"].ToString(),
                            LineOfBusiness = reader["LINE_OF_BUSINESS"].ToString(),
                            Zip = reader["Zip"].ToString(),
                            MeetingCount = (int)reader["MeetingCount"],
                            LeadCount = (int)reader["LeadCount"]
                        };
                    }
                }
                }
                connection.Close();
            }

            return userDetails;
        }

        public UserDetails Get(int id)
        {
            UserDetails userDetails = null ;
            using (MySqlConnection connection = new MySqlConnection(MyConnnectionString))
            {
                connection.Open();
                using (MySqlCommand cmd = connection.CreateCommand())
                { 
                    cmd.CommandText = "SELECT * FROM donow.user_details where ID =" + id.ToString();

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        userDetails = new UserDetails
                        {
                            UserId = (int)reader["ID"],
                            City = reader["City"].ToString(),
                            Company = reader["Company"].ToString(),
                            Email = reader["Email"].ToString(),
                            FullName = reader["Full_Name"].ToString(),
                            ImageUrl = reader["ImageURL"].ToString(),
                            Industry = reader["Industry"].ToString(),
                            IsBusinessUpdatesRequired = (bool)reader["IsBusinessUpdatesRequired"],
                            IsCustomerFollowUpRequired = (bool)reader["IsCustomerFollowUpRequired"],
                            IsMeetingRemindersRequired = (bool)reader["IsMeetingRemindersRequired"],
                            IsNewLeadNotificationRequired = (bool)reader["IsNewLeadNotificationRequired"],
                            IsReferralRequestRequired = (bool)reader["IsReferrelRequestRequired"],
                            Name = reader["User_Name"].ToString(),
                            OfficeAddress = reader["Office_Address"].ToString(),
                            Password = reader["Password"].ToString(),
                            Phone = reader["Office_Address"].ToString(),
                            PreferredCompany = reader["Preferred_Company"].ToString(),
                            PreferredCustomers = reader["Preferred_Customers"].ToString(),
                            PreferredIndustry = reader["Preferred_Industry"].ToString(),
                            State = reader["State"].ToString(),
                            Title = reader["Title"].ToString(),
                            LineOfBusiness = reader["LINE_OF_BUSINESS"].ToString(),
                            Zip = reader["Zip"].ToString()
                        };
                    }
                }
                }
                connection.Close();
            }
           
            return userDetails;
        }

        public UserDetails GetUserByName(string name)
        {
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            long secondsSinceEpoch = (long)t.TotalMilliseconds;
            //string startTime = DateTime.Now.ToString() + " Milli" + DateTime.Now.Millisecond.ToString();
            string startTime = secondsSinceEpoch.ToString();
            string startTime2;
            string startTime3;


            UserDetails userDetails = null;
            using(MySqlConnection connection = new MySqlConnection(MyConnnectionString))
            {
                connection.Open();

                TimeSpan t2 = DateTime.UtcNow - new DateTime(1970, 1, 1);
                long secondsSinceEpoch2 = (long)t2.TotalMilliseconds;
                //string startTime = DateTime.Now.ToString() + " Milli" + DateTime.Now.Millisecond.ToString();
                 startTime2 = secondsSinceEpoch2.ToString();

                using (MySqlCommand cmd = connection.CreateCommand())
                {
                    //cmd.CommandText = "SELECT * FROM donow.user_details where User_Name ='" + name + "'";
                    string stringSQL = "dn_GetUserByName";
                    cmd.CommandText = stringSQL;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@name", name);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                        TimeSpan t3 = DateTime.UtcNow - new DateTime(1970, 1, 1);
                        long secondsSinceEpoch3 = (long)t3.TotalMilliseconds;
                        //string startTime = DateTime.Now.ToString() + " Milli" + DateTime.Now.Millisecond.ToString();
                        startTime3 = secondsSinceEpoch3.ToString();

                        while (reader.Read())
                    {
                        userDetails = new UserDetails
                        {
                            UserId = (int)reader["ID"],
                            City = reader["City"].ToString(),
                            Company = reader["Company"].ToString(),
                            Email = reader["Email"].ToString(),
                            FullName = reader["Full_Name"].ToString(),
                            ImageUrl = reader["ImageURL"].ToString(),
                            Industry = reader["Industry"].ToString(),
                            IsBusinessUpdatesRequired = (bool)reader["IsBusinessUpdatesRequired"],
                            IsCustomerFollowUpRequired = (bool)reader["IsCustomerFollowUpRequired"],
                            IsMeetingRemindersRequired = (bool)reader["IsMeetingRemindersRequired"],
                            IsNewLeadNotificationRequired = (bool)reader["IsNewLeadNotificationRequired"],
                            IsReferralRequestRequired = (bool)reader["IsReferrelRequestRequired"],
                            Name = reader["User_Name"].ToString(),
                            OfficeAddress = reader["Office_Address"].ToString(),
                            Password = reader["Password"].ToString(),
                            Phone = reader["Office_Address"].ToString(),
                            PreferredCompany = reader["Preferred_Company"].ToString(),
                            PreferredCustomers = reader["Preferred_Customers"].ToString(),
                            PreferredIndustry = reader["Preferred_Industry"].ToString(),
                            State = reader["State"].ToString(),
                            Title = reader["Title"].ToString(),
                            LineOfBusiness = reader["LINE_OF_BUSINESS"].ToString(),
                            Zip = reader["Zip"].ToString(),                          
                            MeetingCount = (int)reader["MeetingCount"],
                            LeadCount = (int)reader["LeadCount"] 
                        };
                    }
                }
                }

                connection.Close();
            }
           
            if (userDetails == null)
            {
                userDetails = new UserDetails();
            }
            TimeSpan t1 = DateTime.UtcNow - new DateTime(1970, 1, 1);
            secondsSinceEpoch = (long)t1.TotalMilliseconds;
            string EndTime = secondsSinceEpoch.ToString();
            userDetails.StartTime = startTime;
            userDetails.EndTime = EndTime;
            userDetails.AfterConnectionOpenTime = startTime2;
            userDetails.AfterExecutingSQLTime = startTime3;
            return userDetails;
        }

        public bool GetUserExistsByName(string username)
        {
           bool result = false;
           using (MySqlConnection connection = new MySqlConnection(MyConnnectionString))
            {
                connection.Open();

                using (MySqlCommand cmd = connection.CreateCommand())
                { 
                    cmd.CommandText = "SELECT count(*) FROM donow.user_details where User_Name ='" + username + "'";

                result=(long.Parse(cmd.ExecuteScalar().ToString()) > 0 ? true : false);
                }
               
            }
            return false;
        }

      
        //return the id of user
        // POST api/values
        public  long Post([FromBody]UserDetails value)
        {
           long ID = -1;

           using (MySqlConnection connection = new MySqlConnection(MyConnnectionString))
            {
                connection.Open();

                using (MySqlCommand cmd = connection.CreateCommand())
                {  
                    cmd.CommandText = "SELECT IFNULL(max(ID),0) FROM donow.user_details";
                    ID = long.Parse(cmd.ExecuteScalar().ToString()) + 1;

                    cmd.CommandText = "INSERT INTO donow.user_details VALUES (@ID,@User_Name,@Password,@Full_Name,@Title,@Company,@Industry,@Office_Address,@City,@State,@Zip,@Phone,@Email,@LINE_OF_BUSINESS,@Preferred_Industry,@Preferred_Company,@Preferred_Customers,@IsNewleadNotificationRequired,@IsReferrelRequestRequired,@IsCustomerFollowUpRequired,@IsMeetingRemindersRequired,@IsBusinessUpdatesRequired,@ImageURL)";

                    cmd.Parameters.AddWithValue("@ID", int.Parse(ID.ToString()));
                    cmd.Parameters.AddWithValue("@User_Name", value.Name);
                    cmd.Parameters.AddWithValue("@Password", value.Password);
                    cmd.Parameters.AddWithValue("@Full_Name", value.FullName);
                    cmd.Parameters.AddWithValue("@Title", value.Title);
                    cmd.Parameters.AddWithValue("@Company", value.Company);
                    cmd.Parameters.AddWithValue("@Industry", value.Industry);
                    cmd.Parameters.AddWithValue("@Office_Address", value.OfficeAddress);
                    cmd.Parameters.AddWithValue("@City", value.City);
                    cmd.Parameters.AddWithValue("@State", value.State);
                    cmd.Parameters.AddWithValue("@Zip", value.Zip);
                    cmd.Parameters.AddWithValue("@Phone", value.Phone);
                    cmd.Parameters.AddWithValue("@Email", value.Email);
                    cmd.Parameters.AddWithValue("@LINE_OF_BUSINESS", value.LineOfBusiness);
                    cmd.Parameters.AddWithValue("@Preferred_Company", value.PreferredCompany);
                    cmd.Parameters.AddWithValue("@Preferred_Customers", value.PreferredCustomers);
                    cmd.Parameters.AddWithValue("@Preferred_Industry", value.PreferredIndustry);
                    cmd.Parameters.AddWithValue("@IsNewleadNotificationRequired", (value.IsNewLeadNotificationRequired == true ? 1 : 0));
                    cmd.Parameters.AddWithValue("@IsReferrelRequestRequired", (value.IsReferralRequestRequired == true ? 1 : 0));
                    cmd.Parameters.AddWithValue("@IsCustomerFollowUpRequired", (value.IsCustomerFollowUpRequired == true ? 1 : 0));
                    cmd.Parameters.AddWithValue("@IsMeetingRemindersRequired", (value.IsMeetingRemindersRequired == true ? 1 : 0));
                    cmd.Parameters.AddWithValue("@IsBusinessUpdatesRequired", (value.IsBusinessUpdatesRequired == true ? 1 : 0));
                    cmd.Parameters.AddWithValue("@ImageURL", value.ImageUrl);

                    cmd.ExecuteNonQuery();

                }
                connection.Close();
            }
          
            return ID;
          
        }
    }
}
