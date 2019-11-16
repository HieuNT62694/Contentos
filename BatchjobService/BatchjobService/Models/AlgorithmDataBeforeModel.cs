using BatchjobService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatchjobService.Models
{
    public class AlgorithmDataBeforeModel
    {
        public int IdUser { get; set; }
        public int IdTag { get; set; }
        public int TimeInTeraction { get; set; }
    }
    public class ListTaskModel
    {
        public int IdUser { get; set; }
        public List<TaskInterModel> IdTask { get; set; }
    }
    public class TaskInterModel
    {
        public int Id { get; set; }
        public int? Interaction { get; set; }
    }
}
