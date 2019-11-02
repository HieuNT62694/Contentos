using System;
using System.Collections.Generic;

namespace AuthenticationService.Entities
{
    public partial class Tags
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
    }
}
