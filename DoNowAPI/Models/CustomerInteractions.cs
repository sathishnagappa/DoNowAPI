using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace DoNowAPI.Models
{
    public class CustomerInteractions
    {
        public long UserId { get; set; }
        public string CustomerName { get; set; }
        public string Type { get; set; }
        public string DateNTime { get; set; }
        public string LeadId { get; set; }
    }
}