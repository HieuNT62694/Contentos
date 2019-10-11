using System;
using System.Collections.Generic;

namespace ContentProccessService.Entites
{
    public partial class Tokens
    {
        public int Id { get; set; }
        public int? IdUser { get; set; }
        public string Token { get; set; }
        public string DeviceType { get; set; }
        public bool? Status { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }

        public virtual Users IdUserNavigation { get; set; }
    }
}
