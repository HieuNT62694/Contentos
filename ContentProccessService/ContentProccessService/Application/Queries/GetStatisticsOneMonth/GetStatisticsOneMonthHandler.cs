using ContentProccessService.Entities;
using ContentProccessService.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContentProccessService.Application.Queries.GetStatisticsOneMonth
{
    public class GetStatisticsOneMonthHandler : IRequestHandler<GetStatisticsOneMonthRequest, List<StatisticsModel>>
    {
        private readonly ContentoDbContext _context;
        public GetStatisticsOneMonthHandler(ContentoDbContext contentodbContext)
        {
            _context = contentodbContext;
        }
        public async Task<List<StatisticsModel>> Handle(GetStatisticsOneMonthRequest request, CancellationToken cancellationToken)
        {
            var lstTasks = await _context.Statistics.Where(x=>x.CreatedDate >= DateTime.UtcNow.AddMonths(-1) && x.CreatedDate < DateTime.UtcNow).ToListAsync();
            var lstIdTask = await _context.Statistics.Where(x => x.CreatedDate >= DateTime.UtcNow.AddMonths(-1) && x.CreatedDate < DateTime.UtcNow).Select(x => x.IdTask).Distinct().ToListAsync();
            var countTask = CountTaskInTag(lstIdTask);
            var lstTagInter = new List<StatisticsModel>();
            foreach (var item in lstTasks)
            {

                var lstTag = _context.TasksTags.Include(x => x.IdTagNavigation).Where(x => x.IdTask == item.IdTask).Select(x => x.IdTagNavigation).ToList();
                foreach (var item2 in lstTag)
                {
                    if (lstTagInter.Any(x => x.Tags == item2.Name))
                    {
                        lstTagInter.Where(x => x.Tags == item2.Name).FirstOrDefault().TimeInTeraction += item.Views ?? 0;
                    }
                    else
                    {
                        var Alori = new StatisticsModel();
                        Alori.Tags = item2.Name;
                        Alori.TimeInTeraction += item.Views ?? 0;
                        lstTagInter.Add(Alori);
                    }
                }
            }
            foreach (var res in lstTagInter)
            {
                res.TimeInTeraction = res.TimeInTeraction / countTask.FirstOrDefault(x => x.Tag == res.Tags).Task;
            }
            if (request.Quantity == 0)
            {
                return lstTagInter.OrderByDescending(x => x.TimeInTeraction).ToList();
            }
            return lstTagInter.OrderByDescending(x => x.TimeInTeraction).Take(request.Quantity).ToList();
        }
     
        public List<CountTask> CountTaskInTag(List<int> lstTask)
        {
            var lstTagInter = new List<CountTask>();
            foreach (var item in lstTask)
            {
                var lstTag = _context.TasksTags.Include(x => x.IdTagNavigation).Where(x => x.IdTask == item).Select(x => x.IdTagNavigation).ToList();
                foreach (var item2 in lstTag)
                {
                    if (lstTagInter.Any(x => x.Tag == item2.Name))
                    {
                        lstTagInter.Where(x => x.Tag == item2.Name).FirstOrDefault().Task += 1;
                    }
                    else
                    {
                        var Alori = new CountTask();
                        Alori.Tag = item2.Name;
                        Alori.Task = 1;
                        lstTagInter.Add(Alori);
                    }
                }
            }
            return lstTagInter;
        }
    }
}
