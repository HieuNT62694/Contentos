using System;
using System.Collections.Generic;

namespace ContentProccessService.Entities
{
    public partial class Tasks
    {
        public Tasks()
        {
            Contents = new HashSet<Contents>();
            TasksChannels = new HashSet<TasksChannels>();
            TasksTags = new HashSet<TasksTags>();
        }

        public int Id { get; set; }
        public int IdCampaign { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime? PublishTime { get; set; }
        public int? IsActive { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
        public int? IdWriter { get; set; }

        public virtual Campaign IdCampaignNavigation { get; set; }
        public virtual Users IdWriterNavigation { get; set; }
        public virtual Status IsActiveNavigation { get; set; }
        public virtual ICollection<Contents> Contents { get; set; }
        public virtual ICollection<TasksChannels> TasksChannels { get; set; }
        public virtual ICollection<TasksTags> TasksTags { get; set; }
    }
}
