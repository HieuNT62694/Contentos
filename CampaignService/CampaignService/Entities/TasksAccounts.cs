using System;
using System.Collections.Generic;

namespace CampaignService.Entities
{
    public partial class TasksAccounts
    {
        public int Id { get; set; }
        public int IdAccount { get; set; }
        public int IdTask { get; set; }
        public bool? Status { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }

        public virtual Accounts IdAccountNavigation { get; set; }
        public virtual Tasks IdTaskNavigation { get; set; }
    }
}
