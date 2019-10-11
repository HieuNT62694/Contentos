using System;
using System.Collections.Generic;

namespace CampaignService.Entities
{
    public partial class Positions
    {
        public Positions()
        {
            PositionsAccounts = new HashSet<PositionsAccounts>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool? Status { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }

        public virtual ICollection<PositionsAccounts> PositionsAccounts { get; set; }
    }
}
