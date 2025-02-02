﻿using System;
using System.Collections.Generic;

namespace CampaignService.Entities
{
    public partial class Persionalizations
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int IdTag { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual Tags IdTagNavigation { get; set; }
        public virtual Users IdUserNavigation { get; set; }
    }
}
