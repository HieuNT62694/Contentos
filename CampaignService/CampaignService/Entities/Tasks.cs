using System;
using System.Collections.Generic;

namespace CampaignService.Entities
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
        public int? Status { get; set; }
        public DateTime? StartedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? IdWriter { get; set; }

        public virtual Campaign IdCampaignNavigation { get; set; }
        public virtual Status StatusNavigation { get; set; }
        public virtual ICollection<Contents> Contents { get; set; }
        public virtual ICollection<TasksChannels> TasksChannels { get; set; }
        public virtual ICollection<TasksTags> TasksTags { get; set; }
    }
}
