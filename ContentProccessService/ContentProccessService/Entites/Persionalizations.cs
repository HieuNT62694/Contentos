using System;
using System.Collections.Generic;

namespace ContentProccessService.Entites
{
    public partial class Persionalizations
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int IdTag { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }

        public virtual Tags IdTagNavigation { get; set; }
        public virtual Users IdUserNavigation { get; set; }
    }
}
