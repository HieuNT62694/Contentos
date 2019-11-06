﻿using ContentProccessService.Entities;
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
            var task = await _context.Tasks.AsNoTracking()
                .Include(i=> i.TasksTags).ThenInclude(TasksTags => TasksTags.IdTagNavigation)
                .Include(i => i.StatusNavigation)
                .Include(i => i.IdWritterNavigation)
                .Include(i => i.IdCampaignNavigation).ThenInclude(IdCampaignNavigation => IdCampaignNavigation.IdEditorNavigation)
                .Include(i => i.Contents).ThenInclude(Contents => Contents.Comments)
                .FirstOrDefaultAsync(x => x.Id == request.IdTask);
            var edtId = task.IdCampaignNavigation.IdEditorNavigation.Id;
            var content = task.Contents.Where(x => x.IsActive == true).FirstOrDefault();
            var comment = task.Contents.Where(x => x.IsActive == false && x.Version == (content.Version - 1)).FirstOrDefault();
            var campaign = task.IdCampaignNavigation.Title;
            var lstTag = new List<TagsViewModel>();
            var lTag = new List<int>();
            var Writter = new UsersModels();
            var Status = new StatusModels();
            var Editor = new UsersModels();
            //var lstTags = _context.TasksTags.Include(x => x.IdTagNavigation).Where(x => x.IdTask == request.IdTask).ToList();
            //var Customer = await _context.Tasks.AsNoTracking().Include(i => i.IdCampaignNavigation).ThenInclude(IdCampaignNavigation => IdCampaignNavigation.IdCustomerNavigation)
            // .FirstOrDefaultAsync(x => x.Id == request.IdTask);
            foreach (var item in task.TasksTags)
            {
                var tag = new TagsViewModel();
                tag.Name = item.IdTagNavigation.Name;
                tag.Id = item.IdTag;
                lTag.Add(item.IdTag);
                lstTag.Add(tag);
            }
            Writter.Id = task.IdWritter;
            Writter.Name = task.IdWritterNavigation.FirstName + " " + task.IdWritterNavigation.LastName;
            Status.Id = task.Status;
            Status.Name = task.StatusNavigation.Name;
            Status.Color = task.StatusNavigation.Color;
            Editor.Id = edtId;
            Editor.Name = task.IdCampaignNavigation.IdEditorNavigation.FirstName + " " + task.IdCampaignNavigation.IdEditorNavigation.LastName;
            var Content = new ContentModels
            {
                Id = content.Id,
                Content = content.TheContent,
                Name = content.Name
            };
            var Comment = new Comments();
            if (comment != null)
            {
                Comment.Comment = comment.Comments.FirstOrDefault(x => x.IsActive == true).Comment;
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

            };

            return taskView;
        }

    }


}


