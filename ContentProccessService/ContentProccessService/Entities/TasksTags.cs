﻿using System;
using System.Collections.Generic;

namespace ContentProccessService.Entities
{
    public partial class TasksTags
    {
        public int Id { get; set; }
        public int? IdTask { get; set; }
        public int? IdTag { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual Tags IdTagNavigation { get; set; }
        public virtual Tasks IdTaskNavigation { get; set; }
    }
}
