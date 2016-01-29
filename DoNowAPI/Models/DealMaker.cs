using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoNowAPI.Models
{
    public class DealMaker
    {
        public long BrokerID { get; set; }
        public string BrokerName { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Industry { get; set; }
        public string Phone { get; set; }
        public string Company { get; set; }
        public string City { get; set; }
        public string CreateTime { get; set; }
        public string BrokerScore { get; set; }
        public int Status { get; set; }
        public string BrokerFee { get; set; }
        public string BrokerTotalEarning { get; set; }
        public string ConnectionLead { get; set; }
        public string DomainExpertise { get; set; }
        public string BrokerEmail { get; set; }
        public string BrokerTitle { get; set; }
        public long BrokerUserID { get; set; }
        public string BrokerLOB { get; set; }
        public long LeadID { get; set; }
        //the below mentiond columns should be removed
       /* public string StartTime { get; set; }
        public string EndTime { get; set; } */
    }
}
