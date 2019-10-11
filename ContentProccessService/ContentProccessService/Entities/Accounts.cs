using System;
using System.Collections.Generic;

namespace ContentProccessService.Entities
{
    public partial class Accounts
    {
        public Accounts()
        {
            Activations = new HashSet<Activations>();
        }

        public int Id { get; set; }
        public int IdRole { get; set; }
        public int IdUser { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }

        public virtual Users IdUserNavigation { get; set; }
        public virtual ICollection<Activations> Activations { get; set; }
    }
}
