using System;
using System.Collections.Generic;

namespace CampaignService.Entities
{
    public partial class TasksTags
    {
        public int Id { get; set; }
        public int? IdTask { get; set; }
        public int? IdTag { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }

        public virtual Tags IdTagNavigation { get; set; }
        public virtual Tasks IdTaskNavigation { get; set; }
    }
}
