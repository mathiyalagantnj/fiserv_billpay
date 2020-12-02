using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFNgApp.Models
{
    public partial class Amount
    {
        public int sendAmount { get; set; }
        public int toAmount { get; set; }
        public int touserId { get; set; }

        public int senduserId { get; set; }

    }
}
