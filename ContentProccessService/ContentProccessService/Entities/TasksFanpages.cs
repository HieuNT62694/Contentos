﻿using System;
using System.Collections.Generic;

namespace ContentProccessService.Entities
{
    public partial class TasksFanpages
    {
        public int IdTask { get; set; }
        public int IdFanpage { get; set; }
        public int? IdJob { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual Fanpages IdFanpageNavigation { get; set; }
        public virtual Tasks IdTaskNavigation { get; set; }
    }
}
