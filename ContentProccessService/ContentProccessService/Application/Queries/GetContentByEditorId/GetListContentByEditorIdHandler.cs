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
        private readonly ContentoContext _context;
        public GetListContentByEditorIdHandler(ContentoContext contentodbContext)
        {
            _context = contentodbContext;
        }
        public async Task<List<ContentViewModel>> Handle(GetListContentByEditorIdRequest request, CancellationToken cancellationToken)
        {
            var campaigns = _context.Campaign.AsNoTracking().Where(c => c.IdEditor == request.Id).ToList();

            List<ContentViewModel> list = new List<ContentViewModel>();

            foreach(var cam in campaigns)
            {
                var tasks = _context.Tasks.AsNoTracking().Where(t => t.Status == 3 && t.IdCampaign == cam.Id).ToList();

                foreach(var task in tasks)
                {
                    var contents = _context.Contents.AsNoTracking().Where(c => c.IdTask == task.Id && c.IsActive == true).First();
                    ContentViewModel model = new ContentViewModel();
                    model.Id = contents.Id;
                    model.Name = contents.Name;
                    model.Task = new Models.Task { Id = contents.IdTask, Title = task.Title };
                    model.TheContent = contents.TheContent;
                    model.Version = contents.Version;
                    list.Add(model);
                }
            }

            return list;
        }
    }
}
