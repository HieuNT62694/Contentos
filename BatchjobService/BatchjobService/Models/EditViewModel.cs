using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatchjobService.Models
{
    public class EditViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public int channel { get; set; }
        public int customer { get; set; } = 0;
        public DateTime? modifiedDate { get; set; }
        public string token { get; set; } = "";
    }
}
