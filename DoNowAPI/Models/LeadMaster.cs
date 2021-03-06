﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoNowAPI.Models
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
        public string LEAD_TITLE { get; set; }
        public int LEAD_SOURCE { get; set; }        
    }
}