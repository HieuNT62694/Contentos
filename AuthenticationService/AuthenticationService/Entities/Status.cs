using System;
using System.Collections.Generic;

namespace AuthenticationService.Entities
{
    public partial class Status
    {
        public Status()
        {
            Contents = new HashSet<Contents>();
            Tasks = new HashSet<Tasks>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Contents> Contents { get; set; }
        public virtual ICollection<Tasks> Tasks { get; set; }
    }
}
