using System;
using EFNgApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFNgApp.Models
{
    public class User
    {
        public int user_id { get; set; }
        // public string email_id { get; set; }
        
        public DateTime created_date { get; set; }
        public DateTime updated_date { get; set; }
        public string email_id { get; set; }
        public string first_name { get; set; }
        public string is_active { get; set; }
        public string last_name { get; set; }
        public string middle_name { get; set; }
        public string password { get; set; }

        public string username { get; set; }
        
        public string salutation { get; set; }
        public string mobile_number { get; set; }

        

    }
}
