using System;
using System.Collections.Generic;

namespace AuthenticationService.Entities
{
    public partial class Locations
    {
        public Locations()
        {
            Users = new HashSet<Users>();
        }

        public int Id { get; set; }
        public string Location { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}
