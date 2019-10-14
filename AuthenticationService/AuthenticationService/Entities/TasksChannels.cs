using System;
using System.Collections.Generic;

namespace AuthenticationService.Entities
{
    public partial class TasksChannels
    {
        public int Id { get; set; }
        public int IdTask { get; set; }
        public int IdChannel { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual Channels IdChannelNavigation { get; set; }
        public virtual Tasks IdTaskNavigation { get; set; }
    }
}
