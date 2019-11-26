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

}
