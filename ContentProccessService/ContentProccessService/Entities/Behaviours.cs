using System;
using System.Collections.Generic;

namespace ContentProccessService.Entities
{
    public partial class Behaviours
    {
        public Behaviours()
        {
            Activations = new HashSet<Activations>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }

        public virtual ICollection<Activations> Activations { get; set; }
    }
}
