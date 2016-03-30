using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoNowAPI.Models
{
    public class MeetingList
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int LeadId { get; set; }
        public string Subject { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CustomerName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Status { get; set; }
        public string Comments { get; set; }
        public string SFDCLead_ID { get; set; }
    }
}