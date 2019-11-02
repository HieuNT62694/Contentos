using System;
using System.Collections.Generic;

namespace AuthenticationService.Entities
{
    public partial class Comments
    {
        public Comments()
        {
            Contents = new HashSet<Contents>();
        }

        public int Id { get; set; }
        public string Comment { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual ICollection<Contents> Contents { get; set; }
    }
}
