﻿using System;
using System.Collections.Generic;

namespace CampaignService.Entities
{
    public partial class FavoritesContents
    {
        public int Id { get; set; }
        public int IdContent { get; set; }
        public int IdUser { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }

        public virtual Users IdContent1 { get; set; }
        public virtual Contents IdContentNavigation { get; set; }
    }
}
