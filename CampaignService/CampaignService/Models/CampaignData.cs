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
        public string Status { get; set; }
        public int? idStatus { get; set; }
        public int? IdEditor { get; set; }
        public string editorName { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? StartedDate { get; set; }
        public List<Tag> listTag { get; set; }
        public string customerName { get; set; }
        public int? IdCustomer { get; set; }
    }

    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
