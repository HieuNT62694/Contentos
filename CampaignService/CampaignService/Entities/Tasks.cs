using System;
using System.Collections.Generic;

namespace CampaignService.Entities
{
    public partial class Tasks
    {
        public Tasks()
        {
            TasksAccounts = new HashSet<TasksAccounts>();
            TasksChannels = new HashSet<TasksChannels>();
            TasksTags = new HashSet<TasksTags>();
        }

        public int Id { get; set; }
        public int IdCampaign { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime? PublishTime { get; set; }
        public string Status { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }

        public virtual Campaign IdCampaignNavigation { get; set; }
        public virtual ICollection<TasksAccounts> TasksAccounts { get; set; }
        public virtual ICollection<TasksChannels> TasksChannels { get; set; }
        public virtual ICollection<TasksTags> TasksTags { get; set; }
    }
}
