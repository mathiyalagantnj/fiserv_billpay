using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFNgApp.Models
{
    public class Account
    {
        public int account_id { get; set; }
        // public string email_id { get; set; }

        public double balance { get; set; }
        public DateTime created_date { get; set; }
        public string is_active { get; set; }
        public DateTime updated_date { get; set; }
        public int user_id { get; set; }
        public string account_type { get; set; }


    }
}
