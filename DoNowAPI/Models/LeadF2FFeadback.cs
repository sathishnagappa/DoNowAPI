using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactList.Models
{
    public class LeadF2FFeadback
    {
        public long LeadId { get; set; }
        public long UserID { get; set; }
        public string ConfirmMeeting { get; set; }
        public string ReasonForDown { get; set; }
        public string MeetingInfoHelpFull { get; set; }
        public string LeadAdvanced { get; set; }
        public string CustomerCategorization { get; set; }
        public string SalesStage { get; set; }
        public string NextSteps { get; set; }
        public long MeetingID { get; set; }
       

    }
}
