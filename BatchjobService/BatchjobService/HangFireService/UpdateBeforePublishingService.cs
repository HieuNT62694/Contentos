using BatchjobService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatchjobService.HangFireService
{
    public interface IUpdateBeforePublishingService
    {
        void UpdateStatusBeforePublishing(int id , DateTime time, List<int> lstTag);
    }
    public class UpdateBeforePublishingService : IUpdateBeforePublishingService
    {
        private readonly ContentoDbContext _context;
        public UpdateBeforePublishingService(ContentoDbContext contentodbContext)
        {
            _context = contentodbContext;
        }
        public void UpdateStatusBeforePublishing(int id, DateTime time, List<int> lstTag)
        {
            var content = _context.Contents.FirstOrDefault(w => w.Id == id && w.IsActive == true);
            var upTask = _context.Tasks.FirstOrDefault(x => x.Id == content.IdTask);

            List<TasksTags> tags = new List<TasksTags>();

            foreach(var item in lstTag)
            {
                tags.Add(new TasksTags { IdTag = item });
            }

            if(upTask.Status <= 6)
            {
               upTask.Status = 6;
               upTask.PublishTime = time;
               upTask.TasksTags = tags;

               _context.Update(upTask);
               _context.SaveChanges();
            }
        }
    }
}
