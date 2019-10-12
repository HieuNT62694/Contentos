using System;
using System.Collections.Generic;

namespace CampaignService.Entities
{
    public partial class Contents
    {
        public Contents()
        {
            Activations = new HashSet<Activations>();
            FavoritesContents = new HashSet<FavoritesContents>();
        }

        public int Id { get; set; }
        public int IdTask { get; set; }
        public string Name { get; set; }
        public string TheContent { get; set; }
        public int? Version { get; set; }
        public int? Status { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }

        public virtual Tasks IdTaskNavigation { get; set; }
        public virtual Status StatusNavigation { get; set; }
        public virtual ICollection<Activations> Activations { get; set; }
        public virtual ICollection<FavoritesContents> FavoritesContents { get; set; }
    }
}
