using System;
using System.Collections.Generic;

namespace BatchjobService.Entities
{
    public partial class Tokens
    {
        public int Id { get; set; }
        public int? IdUser { get; set; }
        public string Token { get; set; }
        public string DeviceType { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual Users IdUserNavigation { get; set; }
    }
}
