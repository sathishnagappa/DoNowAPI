using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoNowAPI.Models
{
    public class CustomerDetails
    { public long LeadId { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CompanyInfo { get; set; }
        public string BusinessNeeds { get; set; }
        public List<CustomerInteractions> CustomerInteractionList { get; set; }
        public List<MeetingList> UserMeetingList { get; set; }
        public List<DealHistory> DealHistoryList { get; set; }
        public DealMaker dealMaker { get; set; }
        public int LeadSource{ get; set;}
        public string LeadTitle { get; set; }
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
        public int LeadScore { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }

    }
}