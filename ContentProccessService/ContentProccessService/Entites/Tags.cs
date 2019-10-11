using System;
using System.Collections.Generic;

namespace ContentProccessService.Entites
{
    public partial class Tags
    {
        public Tags()
        {
            Persionalizations = new HashSet<Persionalizations>();
            TasksTags = new HashSet<TasksTags>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool? Status { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }

        public virtual ICollection<Persionalizations> Persionalizations { get; set; }
        public virtual ICollection<TasksTags> TasksTags { get; set; }
    }
}
