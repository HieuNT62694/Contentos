using System;
using System.Collections.Generic;

namespace ContentProccessService.Entities
{
    public partial class Campaign
    {
        public Campaign()
        {
            CampaignTags = new HashSet<CampaignTags>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? Status { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? StartedDate { get; set; }
        public DateTime? Modified { get; set; }
        public int? IdCustomer { get; set; }
        public int? IdMarketer { get; set; }
        public int? IdEditor { get; set; }

        public virtual ICollection<CampaignTags> CampaignTags { get; set; }
    }
}
