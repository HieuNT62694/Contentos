using BatchjobService.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatchjobService.HangFireService

{
    public interface  IUpdateStatusService
    {
        void UpdateStatus();
    }
    public class UpdateStatusService : IUpdateStatusService
    {
        private readonly ContentoContext _context;
        public UpdateStatusService(ContentoContext contentodbContext)
        {
            _context = contentodbContext;
        }
        public  void UpdateStatus()
        {
            var lstTask = _context.Tasks.Where(x => x.Status == 2 || x.Status == 1);
            foreach (var item in lstTask)
            {
                if (item.Deadline < DateTime.UtcNow)
                {
                    item.Status = 4;
                    item.ModifiedDate = DateTime.UtcNow;
                }
            }

            _context.UpdateRange(lstTask);
            _context.SaveChanges();
        }
    }
}
