using System;
using System.Collections.Generic;

namespace CampaignService.Entities
{
    public partial class FavoritesContents
    {
        public int IdContent { get; set; }
        public int IdUser { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual Contents IdContentNavigation { get; set; }
        public virtual Users IdUserNavigation { get; set; }
    }
}
