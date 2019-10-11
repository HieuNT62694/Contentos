using System;
using System.Collections.Generic;

namespace ContentProccessService.Entites
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
        public bool? Status { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }

        public virtual ICollection<TasksChannels> TasksChannels { get; set; }
    }
}
