﻿using System;
using System.Collections.Generic;

namespace ContentProccessService.Entities
{
    public partial class Personalizations
    {
        public int IdUser { get; set; }
        public int IdTag { get; set; }
        public int? Percentage { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsSuggestion { get; set; }
        public bool? IsChosen { get; set; }
        public int? TimeInteraction { get; set; }

        public virtual Tags IdTagNavigation { get; set; }
        public virtual Users IdUserNavigation { get; set; }
    }
}
