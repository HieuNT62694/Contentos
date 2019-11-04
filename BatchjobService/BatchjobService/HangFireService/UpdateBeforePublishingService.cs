using BatchjobService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatchjobService.HangFireService
{
    public interface IUpdateBeforePublishingService
    {
        void UpdateStatusBeforePublishing(int id , DateTime time);
    }
    public class UpdateBeforePublishingService : IUpdateBeforePublishingService
    {
        private readonly ContentoDbContext _context;
        public UpdateBeforePublishingService(ContentoDbContext contentodbContext)
        {
            _context = contentodbContext;
        }
        public void UpdateStatusBeforePublishing(int id, DateTime time)
        {
            var content = _context.Contents.FirstOrDefault(w => w.Id == id && w.IsActive == true);
            var upTask = _context.Tasks.FirstOrDefault(x => x.Id == content.IdTask);
            if(upTask.Status != 6)
            {
               upTask.Status = 6;
               upTask.PublishTime = time;
               _context.UpdateRange(upTask);
               _context.SaveChanges();
            }
        }
    }
}
