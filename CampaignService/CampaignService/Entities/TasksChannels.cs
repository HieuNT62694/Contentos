using System;
using System.Collections.Generic;

namespace CampaignService.Entities
{
    public partial class TasksChannels
    {
        public TasksChannels()
        {
            Contents = new HashSet<Contents>();
        }

        public int Id { get; set; }
        public int IdTask { get; set; }
        public int IdChannel { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }

        public virtual Channels IdChannelNavigation { get; set; }
        public virtual Tasks IdTaskNavigation { get; set; }
        public virtual ICollection<Contents> Contents { get; set; }
    }
}
