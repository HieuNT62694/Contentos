﻿using System;
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
    }

    public class FacebookInteractionModel
    {
        public string name { get; set; }
        public List<FacebookInteraction> data { get; set; }
    }
}
