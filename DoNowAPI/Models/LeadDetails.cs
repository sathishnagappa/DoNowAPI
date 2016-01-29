using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoNowAPI.Models
{
    public class LeadDetails
    {
        //public int Id { get; set; }
        //public int CustomerId { get; set; }
        //public int LeadScore { get; set; }
        //public bool IsNew { get; set; }
        //public int UserId { get; set; }
        //public string Name { get; set; }
        //public string Company { get; set; }
        //public string City { get; set; }
        //public string State { get; set; }
        //public string Source { get; set; }
        //public string CompanyInfo { get; set; }
        //public string BusinessNeeds { get; set; }
        ////status should be read from ui_lead_status
        //public string Status { get; set; }
        //public string ReasonForPass { get; set; }
        //public string SalesStage { get; set; }
        //public DateTime CreatedOn { get; set; }

        public long LEAD_ID { get; set; }
        public string LEAD_NAME { get; set; }      
        public string COMPANY_NAME { get; set; }
        public string STATE { get; set; }
        public string CITY { get; set; }
        public int LEAD_SOURCE { get; set; }
        public string COMPANY_INFO { get; set; }
        public string BUSINESS_NEED { get; set; }
        public string LEAD_TYPE { get; set; }
        public string LEAD_CREATE_TIME { get; set; }
        public string LEAD_STATUS { get; set; }
        public int LEAD_SCORE { get; set; }
        public string STATUS { get; set; }
        public string REASON_FOR_PASS { get; set; }
        public string PHONE { get; set; }
        public string EMAILID { get; set; }    
        public long USER_LEAD_STATUS { get; set; }
        public long USER_ID { get; set; }

        //the below mentiond columns should be removed
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string DBEndTime { get; set; }
       
    }

    public class LeadInfoDetails
    {
        public int LEAD_ID { get; set; }
        public string LEAD_NAME { get; set; }
        public int LEAD_SRC__SYS_ID { get; set; }
        public string LEAD_STATUS_C { get; set; }
        public string LEAD_SRC_SYS_NAME { get; set; }
        public string LEAD_CONNECTION_C { get; set; }
        public string LEAD_COMP_NAME { get; set; }
        public string LEAD_COMP_ADDRESS_LINE { get; set; }
        public string LEAD_COMP_STATE { get; set; }
        public string LEAD_COMP_COUNTRY { get; set; }
        public int LEAD_COMP_ZIPCODE { get; set; }
        public int LEAD_COMP_INDUSTRY { get; set; }
        public int LEAD_COMP_PHONE_NO_1 { get; set; }
        public int LEAD_COMP_PHONE_NO_2 { get; set; }
        public int LEAD_COMP_EMAIL_ID { get; set; }
        public int LEAD_COMP_PRIMARY_CONTACT_PER { get; set; }
        public int LEAD_COMP_SECONDARY_CONTACT_PER { get; set; }
        public string CompanyInfo { get; set; }
        public string BusinessNeeds { get; set; }
        public string Status { get; set; }
        public string ReasonForPass { get; set; }
        public string SalesStage { get; set; }
        public int UserId { get; set; }
        public string IsNew { get; set; }
        public string LEAD_TEXT { get; set; }
        public string LEAD_NEWS { get; set; }
        public string LEAD_AFFILATES { get; set; }
        public string LEAD_MEETING_ALERT_C { get; set; }
        public string LEAD_ALERTS_SLA { get; set; }
        public string LEAD_ALERTS_ACTIVE_F { get; set; }
        public int LEAD_ALERTS_TIME_REMAINING { get; set; }
        public int Lead_Score { get; set; }
        public DateTime CREATE_TS { get; set; }
        public DateTime UPDATE_TS { get; set; }

            

    }
}