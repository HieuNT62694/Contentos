using ContentProccessService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContentProccessService.Models
{
    public class ContentsViewModel
    {
        public int Id { get; set; }
        public Comments Comment { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
    }
}
