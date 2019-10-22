using System;
using System.Collections.Generic;

namespace BatchjobService.Entities
{
    public partial class StatusCampaign
    {
        public StatusCampaign()
        {
            Campaign = new HashSet<Campaign>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }

        public virtual ICollection<Campaign> Campaign { get; set; }
    }
}
