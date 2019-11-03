﻿using System;
using System.Collections.Generic;

namespace ContentProccessService.Entities
{
    public partial class Tags
    {
        public Tags()
        {
            Personalizations = new HashSet<Personalizations>();
            TagsCampaigns = new HashSet<TagsCampaigns>();
            TasksTags = new HashSet<TasksTags>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<Personalizations> Personalizations { get; set; }
        public virtual ICollection<TagsCampaigns> TagsCampaigns { get; set; }
        public virtual ICollection<TasksTags> TasksTags { get; set; }
    }
}
