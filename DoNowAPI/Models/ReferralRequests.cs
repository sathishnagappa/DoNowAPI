using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactList.Models
{
      public class ReferralRequests
    {
        public long ID { get; set; }
        public string SellerName { get; set; }
        public string Industry { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Prospect { get; set; }
        public string BusinessNeeds { get; set; }
        public int Status { get; set; }
        public string CompanyInfo { get; set; }
        public string CompanyName { get; set; }
        public string LeadEmailID { get; set; }
        public string CreatedOn { get; set; }
        public long BrokerUserID { get; set; }
        public long BrokerID { get; set; }
        public long LeadID { get; set; }
        public long SellerUserID { get; set; }
    }
}