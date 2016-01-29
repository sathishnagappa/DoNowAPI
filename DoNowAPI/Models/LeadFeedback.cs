using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactList.Models
{
    public class LeadFeedback
    {
        public long LeadId { get; set; }
        public long UserID { get; set; }
        public string InteractionFeedBack { get; set; }
        public string ReasonForDown { get; set; }
        public string CustomerAcknowledged { get; set; }
        public string Comments { get; set; }
        public long MeetingID { get; set; }
        public string SalesStage { get; set; }
    }
}