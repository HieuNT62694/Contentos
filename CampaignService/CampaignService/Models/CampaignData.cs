using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampaignService.Models
{
    public class CampaignData
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public Editor Editor { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? StartedDate { get; set; }
        public List<Tag> listTag { get; set; }
        public Customer Customer { get; set; }

    }

    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Customer
    {
        public int? Id { get; set; }
        public string Name { get; set; }
    }

    public class Editor
    {
        public int? Id { get; set; }
        public string Name { get; set; }
    }

    public class Status
    {
        public int? Id { get; set; }
        public string Name { get; set; }
    }
}
