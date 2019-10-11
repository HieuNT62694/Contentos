using System;
using System.Collections.Generic;

namespace CampaignService.Entities
{
    public partial class PositionsAccounts
    {
        public int Id { get; set; }
        public int IdAccount { get; set; }
        public int IdPosition { get; set; }
        public int IdCampaign { get; set; }
        public bool? Status { get; set; }

        public virtual Accounts IdAccountNavigation { get; set; }
        public virtual Campaign IdCampaignNavigation { get; set; }
        public virtual Positions IdPositionNavigation { get; set; }
    }
}
