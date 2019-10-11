using System;
using System.Collections.Generic;

namespace CampaignService.Entities
{
    public partial class Campaign
    {
        public Campaign()
        {
            PositionsAccounts = new HashSet<PositionsAccounts>();
            Tasks = new HashSet<Tasks>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }

        public virtual ICollection<PositionsAccounts> PositionsAccounts { get; set; }
        public virtual ICollection<Tasks> Tasks { get; set; }
    }
}
