﻿using System;
using System.Collections.Generic;

namespace BatchjobService.Entities
{
    public partial class Users
    {
        public Users()
        {
            Accounts = new HashSet<Accounts>();
            CampaignIdCustomerNavigation = new HashSet<Campaign>();
            CampaignIdEditorNavigation = new HashSet<Campaign>();
            CampaignIdMarketerNavigation = new HashSet<Campaign>();
            FavoritesContents = new HashSet<FavoritesContents>();
            Persionalizations = new HashSet<Persionalizations>();
            Tokens = new HashSet<Tokens>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public bool? Gender { get; set; }
        public string Quote { get; set; }
        public int IdOccupation { get; set; }
        public int IdLocation { get; set; }
        public bool? IsActive { get; set; }
        public int IdManager { get; set; }
        public string Company { get; set; }

        public virtual Locations IdLocationNavigation { get; set; }
        public virtual Occupations IdOccupationNavigation { get; set; }
        public virtual ICollection<Accounts> Accounts { get; set; }
        public virtual ICollection<Campaign> CampaignIdCustomerNavigation { get; set; }
        public virtual ICollection<Campaign> CampaignIdEditorNavigation { get; set; }
        public virtual ICollection<Campaign> CampaignIdMarketerNavigation { get; set; }
        public virtual ICollection<FavoritesContents> FavoritesContents { get; set; }
        public virtual ICollection<Persionalizations> Persionalizations { get; set; }
        public virtual ICollection<Tokens> Tokens { get; set; }
    }
}
