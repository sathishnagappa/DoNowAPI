using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactList.Models
{
    public class DealHistory
    {
        public long LeadId { get; set; }
        public long UserId { get; set; }
        public string Date { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string CustomerName { get; set; }
        //public string country { get; set; }
        public long BrokerId { get; set; }
        public string LeadIndustry { get; set; }
        public string BrokerName { get; set; }
    }
}