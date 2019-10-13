using System;
using System.Collections.Generic;

namespace AuthenticationService.Entities
{
    public partial class Roles
    {
        public int Id { get; set; }
        public string Role { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
