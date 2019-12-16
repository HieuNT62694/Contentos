using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatchjobService.Models
{
    public class FacebookInteraction
    {
        public string title { get; set; }
        public DateTime? publicDate { get; set; }
        public string reactiontCount { get; set; }
        public string shareCount { get; set; }
        public string commentCount { get; set; }
        public string link { get; set; }

        public int possitiveCommentCount { get; set; }
    }

    public class FacebookInteractionModel2
    {
        public string name { get; set; }
        public List<FacebookInteraction> data { get; set; }
    }

    public class FacebookInteractionModel
    {
        public List<FacebookInteractionModel2> data { get; set; }

        public DateTime? startDate { get; set; }

        public DateTime? endDate { get; set; }
    }

    public class FacebookPageStatisticsModel
    {
        public string name { get; set; }
        public int newLikeCount { get; set; }
        public int newInboxCount { get; set; }
    }

    public class FacebookPageStatistics
    {
        public string name { get; set; }
        public int interaction { get; set; }
        public int inbox { get; set; }
        public int view { get; set; }
    }
}
