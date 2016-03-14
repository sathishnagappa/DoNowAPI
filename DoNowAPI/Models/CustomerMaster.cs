using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoNowAPI.Models
{
    public class CustomerMaster
    {

        public long LeadId { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public int LEAD_SOURCE { get; set; }
    }
}