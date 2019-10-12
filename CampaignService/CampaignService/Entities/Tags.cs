using System;
using System.Collections.Generic;

namespace CampaignService.Entities
{
    public partial class Tags
    {
        public Tags()
        {
            CampaignTags = new HashSet<CampaignTags>();
            Persionalizations = new HashSet<Persionalizations>();
            TasksTags = new HashSet<TasksTags>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }

        public virtual ICollection<CampaignTags> CampaignTags { get; set; }
        public virtual ICollection<Persionalizations> Persionalizations { get; set; }
        public virtual ICollection<TasksTags> TasksTags { get; set; }
    }
}
