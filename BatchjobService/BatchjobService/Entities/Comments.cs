using System;
using System.Collections.Generic;

namespace BatchjobService.Entities
{
    public partial class Comments
    {
        public Comments()
        {
            Contents = new HashSet<Contents>();
        }

        public int Id { get; set; }
        public string Comment { get; set; }
        public DateTime? CreateDate { get; set; }

        public virtual ICollection<Contents> Contents { get; set; }
    }
}
