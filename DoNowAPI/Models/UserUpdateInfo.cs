using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactList.Models
{
    public class UserUpdateInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}