using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactList.Models
{
    public class CustomerMaster
    {

        public long LeadId { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }

        //the below mentiond columns should be removed
       
        public string StartTime { get; set; }
        public string EndTime { get; set; }

    }
}