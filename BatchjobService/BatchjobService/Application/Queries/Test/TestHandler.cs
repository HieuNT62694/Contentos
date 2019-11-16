using BatchjobService.Entities;
using BatchjobService.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace BatchjobService.Application.Queries.Test
{
    public class TestHandler : IRequestHandler<TestRequest, List<AlgorithmDataBeforeModel>>
    {
        private readonly ContentoDbContext _context;
        public TestHandler( ContentoDbContext context)
        {
            _context = context;
        }
        public async Task<List<AlgorithmDataBeforeModel>> Handle(TestRequest request, CancellationToken cancellationToken)
        {
            var listIduser = await _context.UsersInteractions.Select(x => x.IdUser).Distinct().ToListAsync();
            var lstAlori = new List<AlgorithmDataBeforeModel>();
            var lstTest = new List<ListTaskModel>();
            foreach (var test in listIduser)
            {
                var userInterId = await _context.UsersInteractions.Where(x=>x.IdUser == test).Select(x => new ListTaskModel
                {
                    IdUser = x.IdUser,
                    IdTask = _context.UsersInteractions.Where(z => z.IdUser == x.IdUser).Select(z => new TaskInterModel { Id = z.IdTask, Interaction = z.Interaction ?? 0 }).ToList()
                }).FirstOrDefaultAsync();
                lstTest.Add(userInterId);
            }


            var lstTagsum = new List<int>();
            foreach (var item in lstTest)
            {
                foreach (var item1 in item.IdTask)
                {
                    var lstTag = _context.TasksTags.Where(x => x.IdTask == item1.Id).Select(x => x.IdTag).ToList();
                    foreach (var item2 in lstTag)
                    {
                        if (lstAlori.Select(x => x.IdTag).Contains(item2) && lstAlori.Select(x => x.IdUser).Contains(item.IdUser))
                        {
                            lstAlori.Where(x => x.IdTag == item2 && x.IdUser == item.IdUser).FirstOrDefault().TimeInTeraction += item1.Interaction ?? 0;
                        }
                        else
                        {
                            var Alori = new AlgorithmDataBeforeModel { IdUser = item.IdUser };
                            Alori.IdTag = item2;
                            Alori.TimeInTeraction = Alori.TimeInTeraction + item1.Interaction ?? 0;
                            lstAlori.Add(Alori);
                        }
                       
                    }

                }



            }


            return lstAlori;
        }

    }
}
