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
            var lstTasks = await _context.Statistics.Where(x=> x.CreatedDate >= DateTime.UtcNow.AddDays(-7) && x.CreatedDate < DateTime.UtcNow).ToListAsync();
            var countTask = CountTaskInTag(lstTasks);
            var lstTagInter = new List<StatisticsModel>();
            foreach (var item in lstTasks)
            {

                var lstTag = _context.TasksTags.Include(x=>x.IdTagNavigation).Where(x => x.IdTask == item.IdTask).Select(x => x.IdTagNavigation).ToList();
                foreach (var item2 in lstTag)
                {
                    if (lstTagInter.Any(x => x.Tags == item2.Name))
                    {
                        lstTagInter.Where(x => x.Tags == item2.Name).FirstOrDefault().TimeInTeraction += item.Views ?? 0;
                    }
                    else
                    {
                        var Alori = new StatisticsModel ();
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
        
        public List<CountTask> CountTaskInTag(List<Statistics> lstTask) 
        {
            var lstTagInter = new List<CountTask>();
            foreach (var item in lstTask)
            {
                var lstTag = _context.TasksTags.Include(x => x.IdTagNavigation).Where(x => x.IdTask == item.IdTask).Select(x => x.IdTagNavigation).ToList();
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
