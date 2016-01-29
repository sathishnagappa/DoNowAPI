using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactList.Models
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

        //the below mentiond columns should be removed
        public int LeadScore { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }

    }
}