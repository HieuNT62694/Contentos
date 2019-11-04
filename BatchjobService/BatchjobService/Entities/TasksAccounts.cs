using System;
using System.Collections.Generic;

namespace BatchjobService.Entities
{
    public partial class TasksAccounts
    {
        public int IdAccount { get; set; }
        public int IdTask { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual Accounts IdAccountNavigation { get; set; }
        public virtual Tasks IdTaskNavigation { get; set; }
    }
}
