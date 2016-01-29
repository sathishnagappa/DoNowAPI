using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactList.Models
{
    public class BrokerScoring
    {
        public long Lead_ID { get; set; }  //LEAD_ID 
        public long Broker_ID { get; set; }
        public string Broker_Score { get; set; }
        public string Title { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Industry { get; set; }
        public string Already_broker { get; set; }
        public string Deals_closed { get; set; }
        public string Deals_closed_in_the_past_with_this_prospect { get; set; }
        public string Deals_closed_in_same_indusrty_as_lead { get; set; }
        public string Deals_closed_in_same_geo_as_lead { get; set;  }        

    }
    
}