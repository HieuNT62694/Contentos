using ContentProccessService.Entities;
using ContentProccessService.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContentProccessService.Application.Queries.GetStatistics
{
    public class GetStatisticsHandler : IRequestHandler<GetStatisticsRequest, List<StatisticsModel>>
    {
        private readonly ContentoDbContext _context;
        public GetStatisticsHandler(ContentoDbContext contentodbContext)
        {
            _context = contentodbContext;
        }
        public async Task<List<StatisticsModel>> Handle(GetStatisticsRequest request, CancellationToken cancellationToken)
        {
            var lstTasks = await _context.UsersInteractions.ToListAsync();
            var listTaskWeek = GetTaskOneWeek(lstTasks);
            var lstTagInter = new List<StatisticsModel>();
            foreach (var item in listTaskWeek)
            {

                var lstTag = _context.TasksTags.Include(x=>x.IdTagNavigation).Where(x => x.IdTask == item.IdTask).Select(x => x.IdTagNavigation).ToList();
                foreach (var item2 in lstTag)
                {
                    if (lstTagInter.Any(x => x.Tags == item2.Name))
                    {
                        lstTagInter.Where(x => x.Tags == item2.Name).FirstOrDefault().TimeInTeraction += item.TimeInTeraction;
                    }
                    else
                    {
                        var Alori = new StatisticsModel ();
                        Alori.Tags = item2.Name;
                        Alori.TimeInTeraction += item.TimeInTeraction;
                        lstTagInter.Add(Alori);
                    }
                }
            }
            Array s1 = Array.CreateInstance(typeof(string), new[] { 5 }, new[] { 1 });
            if (request.Quantity == 0)
            {
                return lstTagInter.OrderByDescending(x => x.TimeInTeraction).ToList();
              
            }
            return lstTagInter.OrderByDescending(x => x.TimeInTeraction).Take(request.Quantity).ToList();
         
        }
        public List<ListTaskModel> GetTaskOneWeek(List<UsersInteractions> ListTags)
        {
            var lstTaskTwo = new List<ListTaskModel>();
            foreach (var item in ListTags)
            {
                var task = _context.Tasks.FirstOrDefault(x => x.Id == item.IdTask
                && x.Status == 7
                && x.Contents.Any(t => t.IsActive == true)
                && x.TasksFanpages.Any(t => t.IdFanpage == 1));
                var test = DateTime.UtcNow.AddDays(-7);
                if (task != null)
                {
                    if (task.PublishTime >= DateTime.UtcNow.AddDays(-7) && task.PublishTime < DateTime.UtcNow)
                    {
                        var newTask = new ListTaskModel()
                        {
                            IdTask = item.IdTask,
                            TimeInTeraction = item.Interaction ?? 0
                        };
                        lstTaskTwo.Add(newTask);
                    }
                }

            }
            return lstTaskTwo;
        }


    }
}
