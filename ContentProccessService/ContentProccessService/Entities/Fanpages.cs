using System;
using System.Collections.Generic;

namespace ContentProccessService.Entities
{
    public partial class Fanpages
    {
        public Fanpages()
        {
            TasksFanpages = new HashSet<TasksFanpages>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? IdMarketer { get; set; }
        public int? IdChannel { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool? IsActive { get; set; }
        public int? IdCustomer { get; set; }

        public virtual Channels IdChannelNavigation { get; set; }
        public virtual Users IdMarketerNavigation { get; set; }
        public virtual ICollection<TasksFanpages> TasksFanpages { get; set; }
    }
}
