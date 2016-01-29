using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoNowAPI.Models
{
    public class UserDetails
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string Industry { get; set; }
        public string OfficeAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PreferredIndustry { get; set; }
        public string PreferredCompany { get; set; }
        public string PreferredCustomers { get; set; }
        public bool IsNewLeadNotificationRequired { get; set; }
        public bool IsReferralRequestRequired { get; set; }
        public bool IsCustomerFollowUpRequired { get; set; }
        public bool IsMeetingRemindersRequired { get; set; }
        public bool IsBusinessUpdatesRequired { get; set; }
        public string ImageUrl { get; set; }
        public string LineOfBusiness { get; set; }

        //the below mentiond columns should be removed

        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string AfterConnectionOpenTime { get; set; }
        public string AfterExecutingSQLTime { get; set; }
    }
}