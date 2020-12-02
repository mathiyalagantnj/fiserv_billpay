using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFNgApp.Models
{
    public class Ledger
    {

        public int ledger_id { get; set; }
        // public string email_id { get; set; }

        public double acc_balance { get; set; }

        public double ledger_balance { get; set; }

        public int transaction_amt { get; set; }
        public DateTime transaction_date { get; set; }
        public string transaction_from { get; set; }

        public int transaction_from_id { get; set; }

        public string transaction_reference_number { get; set; }
        public string transaction_remark { get; set; }

        public string transaction_status { get; set; }

        public string transaction_to { get; set; }

        public int transaction_to_id { get; set; }
        public string transaction_type { get; set; }

        public int user_id { get; set; }
        // public DateTime updated_date { get; set; }



    }
}
