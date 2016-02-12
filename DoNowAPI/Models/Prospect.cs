using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
 

namespace DoNowAPI.Models
{
    public class Prospect
    {
        public long LEAD_ID { get; set; }
        public string LEAD_NAME { get; set; }
        public string COMPANY_NAME { get; set; }
        public string STATE { get; set; }
        public string CITY { get; set; }
        public int LEAD_SOURCE { get; set; }
        public string INDUSTRY_INFO { get; set; }
        public string BUSINESS_NEED { get; set; }
        public string PHONE { get; set; }
        public string EMAILID { get; set; }
        public int LEAD_SCORE { get; set; }
        public string LEAD_TYPE { get; set; }
        public string LEAD_CREATE_TIME { get; set; }
        public string LEAD_STATUS { get; set; }
        public string STATUS { get; set; }
        public string REASON_FOR_PASS { get; set; }
        public int USER_LEAD_STATUS { get; set; }
        public long USER_ID { get; set; }
        public string LEAD_TITLE { get; set; }
        public List<CustomerInteractions> customerInteractionList { get; set; }
        public List<MeetingList> UserMeetingList { get; set; }       
        public List<DealMaker> brokerList { get; set; }
        public string ADDRESS { get; set; }
        public string ZIPCODE { get; set; }
        public string COUNTRY { get; set; }
        public string FISCALYE { get; set; }
        public string REVENUE { get; set; }
        public string NETINCOME { get; set; }
        public string EMPLOYEES { get; set; }
        public string MARKETVALUE { get; set; }
        public string YEARFOUNDED { get; set; }
        public string INDUSTRYRISK { get; set; }
        public string COUNTY { get; set; }
        public string WebAddress { get; set; }


        //the below mentiond columns should be removed
        public string StartTime { get; set; }
        public string EndTime { get; set; }

    }

}