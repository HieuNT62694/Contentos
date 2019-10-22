using System;
using System.Collections.Generic;

namespace BatchjobService.Entities
{
    public partial class Activations
    {
        public int Id { get; set; }
        public int IdAccount { get; set; }
        public int IdBehaviour { get; set; }
        public int IdContent { get; set; }
        public DateTime? Date { get; set; }
        public int? Quantity { get; set; }

        public virtual Accounts IdAccountNavigation { get; set; }
        public virtual Behaviours IdBehaviourNavigation { get; set; }
        public virtual Contents IdContentNavigation { get; set; }
    }
}
