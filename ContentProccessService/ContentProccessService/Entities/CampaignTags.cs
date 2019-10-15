using System;
using System.Collections.Generic;

namespace ContentProccessService.Entities
{
    public partial class CampaignTags
    {
        public int IdCampaign { get; set; }
        public int IdTags { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual Campaign IdCampaignNavigation { get; set; }
        public virtual Tags IdTagsNavigation { get; set; }
    }
}
