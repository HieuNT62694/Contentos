using System;
using System.Collections.Generic;

namespace CampaignService.Entities
{
    public partial class Roles
    {
        public int Id { get; set; }
        public string Role { get; set; }
        public bool? Status { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}
