using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatchjobService.Models
{
    public class FanpageViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public Channel channel { get; set; }
        public Customer customer { get; set; }
        public DateTime modifiedDate { get; set; }
    }

    public class Channel
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Customer
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
