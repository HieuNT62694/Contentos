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
        private readonly ContentoContext _context;
        public GetTaskDetailHandler(ContentoContext contentodbContext)
        {
            _context = contentodbContext;
        }
        public async Task<TasksViewModel> Handle(GetTaskDetailRequest request, CancellationToken cancellationToken)
        {
            var task = await _context.Tasks.AsNoTracking().Include(i=>i.Contents)
                .FirstOrDefaultAsync(x => x.Id == request.IdTask);
            var edtId = _context.Campaign.Find(task.IdCampaign).IdEditor;
            var content = task.Contents.Where(x => x.IsActive == true).FirstOrDefault();
            var campaign = _context.Campaign.Find(task.IdCampaign).Title;
            var lstTag = new List<TagsViewModel>();
            var lstTags = _context.TasksTags.Where(x=>x.IdTask == request.IdTask).ToList();
            foreach (var item in lstTags)
            {
                var tag = new TagsViewModel();
                tag.Name = _context.Tags.FirstOrDefault(x => x.Id == item.IdTag).Name;
                tag.Id = item.IdTag;
                lstTag.Add(tag);
            }
            var Writter = new UsersModels
            {
                Id = task.IdWriter,
                Name = _context.Users.FirstOrDefault(x => x.Id == task.IdWriter).Name
            };
            var Status = new StatusModels
            {
                Id = task.Status,
                Name = _context.StatusTasks.FirstOrDefault(x => x.Id == task.Status).Name,
                Color = _context.StatusTasks.FirstOrDefault(x => x.Id == task.Status).Color
            };
            var Editor = new UsersModels
            {
                Id = edtId,
                Name = _context.Users.FirstOrDefault(x => x.Id == edtId).Name
            };
            var Content = new ContentModels
            {
                Id = content.Id,
                Content = content.TheContent,
                Name = content.Name
            };
            var Comment = new Comments();
            if (task.Contents.FirstOrDefault(x => x.IsActive == true).IdComment != null)
            {
                Comment.Comment = _context.Comments.FirstOrDefault(x => x.Id == content.IdComment).Comment;
            }
           
               var taskView = new TasksViewModel()
               {
                   Title = task.Title,
                   Deadline = task.Deadline,
                   PublishTime = task.PublishTime,
                   Writer = Writter,
                   Description = task.Description,
                   Status = Status,
                   StartedDate = task.StartedDate,
                   Editor = Editor,
                   Content = Content,
                   Comment = Comment,
                   Id = task.Id,
                   Tags = lstTag,
                   Campaign = campaign
               };

            return taskView;
        }
     
    }
                          

}
    

