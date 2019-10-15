using System;
using System.Collections.Generic;

namespace ContentProccessService.Entities
{
    public partial class Tasks
    {
        public Tasks()
        {
            TasksChannels = new HashSet<TasksChannels>();
            TasksTags = new HashSet<TasksTags>();
        }

        public int Id { get; set; }
        public int IdCampaign { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime? PublishTime { get; set; }
        public int? Status { get; set; }
        public DateTime? StartedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? IdWriter { get; set; }

        public virtual ICollection<TasksChannels> TasksChannels { get; set; }
        public virtual ICollection<TasksTags> TasksTags { get; set; }
    }
}
