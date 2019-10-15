using System;
using System.Collections.Generic;

namespace ContentProccessService.Entities
{
    public partial class Comments
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
