using ContentProccessService.Entities;
using ContentProccessService.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContentProccessService.Application.Queries.GetTaskDetail
{
    public class GetTaskDetailHandler : IRequestHandler<GetTaskDetailRequest, TasksViewModel>
    {
        private readonly ContentoDbContext _context;
        public GetTaskDetailHandler(ContentoDbContext contentodbContext)
        {
            _context = contentodbContext;
        }
        public async Task<TasksViewModel> Handle(GetTaskDetailRequest request, CancellationToken cancellationToken)
        {
            var task = await _context.Tasks.AsNoTracking().Include(i=>i.Contents).ThenInclude(Contents=> Contents.Comments)
                .FirstOrDefaultAsync(x => x.Id == request.IdTask);
            var edtId = _context.Campaigns.Find(task.IdCampaign).IdEditor;
            var content = task.Contents.Where(x => x.IsActive == true).FirstOrDefault();
            var comment = task.Contents.Where(x => x.IsActive == false && x.Version == (content.Version - 1) ).FirstOrDefault();
            var campaign = _context.Campaigns.Find(task.IdCampaign).Title;
            var lstTag = new List<TagsViewModel>();
            var lTag = new List<int>();
            var lstTags = _context.TasksTags.Where(x=>x.IdTask == request.IdTask).ToList();
            var Customer = await _context.Tasks.AsNoTracking().Include(i => i.IdCampaignNavigation).ThenInclude(IdCampaignNavigation => IdCampaignNavigation.IdCustomerNavigation)
                .FirstOrDefaultAsync(x => x.Id == request.IdTask);
            foreach (var item in lstTags)
            {
                var tag = new TagsViewModel();
                tag.Name = _context.Tags.FirstOrDefault(x => x.Id == item.IdTag).Name;
                tag.Id = item.IdTag;
                lTag.Add(item.IdTag);
                lstTag.Add(tag);
            }
            var wtn = _context.Users.FirstOrDefault(x => x.Id == task.IdWritter);
            var Writter = new UsersModels
            {
                Id = task.IdWritter,
                Name = wtn.FirstName + " " + wtn.LastName
            };
            var Status = new StatusModels
            {
                Id = task.Status,
                Name = _context.StatusTasks.FirstOrDefault(x => x.Id == task.Status).Name,
                Color = _context.StatusTasks.FirstOrDefault(x => x.Id == task.Status).Color
            };
            var etn = _context.Users.FirstOrDefault(x => x.Id == edtId);
            var Editor = new UsersModels
            {
                Id = edtId,
                Name = etn.FirstName + " " + etn.LastName
            };
            var Content = new ContentModels
            {
                Id = content.Id, 
                Content = content.TheContent,
                Name = content.Name
            };
            var Comment = new Comments();
                if (comment != null)
            {
                Comment.Comment = comment.Comments.FirstOrDefault(x=>x.IsActive == true).Comment;
            }

            var taskView = new TasksViewModel()
               {
                   Title = task.Title,
                   Deadline = task.Deadline,
                   PublishTime = task.PublishTime,
                   Writer = Writter,
                   Description = task.Description,
                   Status = Status,
                   StartedDate = task.StartDate,
                   Editor = Editor,
                   Content = Content,
                   Comment = Comment,
                   Id = task.Id,
                   Tags = lstTag,
                   Campaign = campaign,
                   Tag = lTag,
                   Customer = Customer.IdCampaignNavigation.IdCustomerNavigation.Id
                  
            };

            return taskView;
        }
     
    }
                          

}
    

