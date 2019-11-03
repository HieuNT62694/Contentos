using ContentProccessService.Entities;
using ContentProccessService.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContentProccessService.Application.Queries.GetContentByEditorId
{
    public class GetListContentByEditorIdHandler : IRequestHandler<GetListContentByEditorIdRequest,List<ContentViewModel>>
    {
        private readonly ContentoDbContext _context;
        public GetListContentByEditorIdHandler(ContentoDbContext contentodbContext)
        {
            _context = contentodbContext;
        }
        public async Task<List<ContentViewModel>> Handle(GetListContentByEditorIdRequest request, CancellationToken cancellationToken)
        {
            var campaigns = await _context.Campaigns.AsNoTracking().Include(x=>x.Tasks).Where(c => c.IdEditor == request.Id).ToListAsync();

            List<ContentViewModel> list = new List<ContentViewModel>();

            //foreach(var cam in campaigns)
            //{
            //    var tasks = _context.Tasks.AsNoTracking().Where(t => t.Status == 3 && t.IdCampaign == cam.Id).ToList();

            //    foreach(var task in tasks)
            //    {
            //        var contents = _context.Contents.AsNoTracking().Where(c => c.IdTask == task.Id && c.IsActive == true).First();
            //        ContentViewModel model = new ContentViewModel();
            //        model.Id = contents.Id;
            //        model.Name = contents.Name;
            //        model.Task = new ContentProccessService.Models.Task { Id = contents.IdTask, Title = task.Title };
            //        model.TheContent = contents.TheContent;
            //        model.Version = contents.Version;
            //        list.Add(model);
            //    }
            //}
            foreach (var cam in campaigns)
            {
                foreach (var task in cam.Tasks)
                {
                    var contents = _context.Contents.AsNoTracking().Where(c => c.IdTask == task.Id && c.IsActive == true).First();
                    ContentViewModel model = new ContentViewModel();
                    model.Id = contents.Id;
                    model.Name = contents.Name;
                    model.Task = new ContentProccessService.Models.Task { Id = contents.IdTask, Title = task.Title };
                    model.TheContent = contents.TheContent;
                    model.Version = contents.Version;
                    list.Add(model);
                }
            }

            return list;
        }
    }
}
