using System;
using System.Collections.Generic;

namespace BatchjobService.Entities
{
    public partial class Channels
    {
        public Channels()
        {
            TasksChannels = new HashSet<TasksChannels>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<TasksChannels> TasksChannels { get; set; }
    }
}
