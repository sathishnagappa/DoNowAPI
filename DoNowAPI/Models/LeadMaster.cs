using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactList.Models
{
    public class LeadMaster
    {

        public long LEAD_ID { get; set; }
        public string LEAD_NAME { get; set; }
        public string COMPANY_NAME { get; set; }
        public string STATE { get; set; }
        public string CITY { get; set; }
        public int LEAD_SCORE { get; set; }
        public int USER_LEAD_STATUS { get; set; }
        public string LeadIndustry { get; set; }
        public string CreatedOn { get; set; }
        public string LEAD_TYPE { get; set; }

        //the below mentiond columns should be removed
       public string StartTime { get; set; }
        public string EndTime { get; set; }
       //public string DBEndTime { get; set; }
       // public string APIProcessTimeAfterDB { get; set; }
       // public string DBConnectionTime { get; set; }
        public string AfterConnectionOpenTime { get; set; }
        public string AfterExecutingSQLTime { get; set; }  
    }
}