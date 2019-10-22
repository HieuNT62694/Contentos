using System;
using System.Collections.Generic;

namespace BatchjobService.Entities
{
    public partial class Contents
    {
        public Contents()
        {
            Activations = new HashSet<Activations>();
            FavoritesContents = new HashSet<FavoritesContents>();
        }

        public int Id { get; set; }
        public int? IdComment { get; set; }
        public int IdTask { get; set; }
        public string Name { get; set; }
        public string TheContent { get; set; }
        public int? Version { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual Comments IdCommentNavigation { get; set; }
        public virtual Tasks IdTaskNavigation { get; set; }
        public virtual ICollection<Activations> Activations { get; set; }
        public virtual ICollection<FavoritesContents> FavoritesContents { get; set; }
    }
}
