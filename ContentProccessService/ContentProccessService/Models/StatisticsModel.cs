using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContentProccessService.Models
{
    public class StatisticsModel
    {
        public string Tags { get; set; }
        public int TimeInTeraction { get; set; }

    }

    public class ListTaskModel
    {
        public int IdTask { get; set; }
        public int TimeInTeraction { get; set; }
    }
    public class CountTask
    {
        public string Tag { get; set; }
        public int Task { get; set; }
    }
    public class ListTaskStaticModel
    {
        public int IdTask { get; set; }
        public string Title { get; set; }
        public int View { get; set; }
        public bool Published { get; set; }
    }
    public class TaskTrendViewStaicModel
    {
        public int IdTask { get; set; }
        public string Title { get; set; }
        public int View { get; set; }
    }
    public class ListTaskInteractionModel
    {
        public int IdTask { get; set; }
        public int View { get; set; }

    }

}
