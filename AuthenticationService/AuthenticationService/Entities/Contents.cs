using System;
using System.Collections.Generic;

namespace AuthenticationService.Entities
{
    public partial class Contents
    {
        public int Id { get; set; }
        public int? IdTask { get; set; }
        public int? IdComment { get; set; }
        public string Name { get; set; }
        public string TheContent { get; set; }
        public int? Version { get; set; }
        public string IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual Comments IdCommentNavigation { get; set; }
        public virtual Tasks IdTaskNavigation { get; set; }
    }
}
