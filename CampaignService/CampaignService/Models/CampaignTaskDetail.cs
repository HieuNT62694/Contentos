using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampaignService.Models
{
    public class CampaignTaskDetail
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? IdStatus { get; set; }
        public string Status { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? StratDate { get; set; }
        public ICollection<string> listTag { get; set; }
        public string customerName { get; set; }
        public int? IdCustomer { get; set; }
    }
}
