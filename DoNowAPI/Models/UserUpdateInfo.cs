using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoNowAPI.Models
{
    public class UserUpdateInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}