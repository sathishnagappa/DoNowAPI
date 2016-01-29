using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactList.Models
{
    public class CustomerFeed
    {
        public long CustomerId { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
    }
}