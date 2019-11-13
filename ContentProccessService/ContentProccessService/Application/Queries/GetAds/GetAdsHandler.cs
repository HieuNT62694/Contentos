
using ContentProccessService.Entities;
using ContentProccessService.Models;
using HtmlAgilityPack;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AuthenticationService.Application.Queries.GetAds
{
    public class GetAdsHandler : IRequestHandler<GetAdsRequest, List<ContentViewer>>
    {
        private readonly ContentoDbContext _context;

        public GetAdsHandler(ContentoDbContext context)
        {
            _context = context;
        }

        public async Task<List<ContentViewer>> Handle(GetAdsRequest request, CancellationToken cancellationToken)
        {
            var list = await _context.Contents.AsNoTracking()
                .Include(c=> c.IdTaskNavigation)
                .Include(c => c.IdTaskNavigation.TasksFanpages)
                .Include(c => c.IdTaskNavigation.TasksTags).ThenInclude(TasksTags => TasksTags.IdTagNavigation)
                .Where(c => c.IdTaskNavigation.TasksFanpages.Any(t => t.IdFanpage == request.IdFanpage))
                .Where(c => c.IsActive == true)
                .Where(c => c.IsAds == true).ToListAsync();
            var lstContentReturn = new List<ContentViewer>();
            foreach (var item in list)
            {
                List<string> imgs = getImage(item.TheContent);
                if (imgs.Count == 0)
                {
                    imgs.Add("https://marketingland.com/wp-content/ml-loads/2015/11/content-marketing-idea-lightbulb-ss-1920.jpg");
                }
                var Cnt = new ContentModels
                {
                    Id = item.Id,
                    Name = item.Name
                };


                var lstTag = new List<TagsViewModel>();
                foreach (var item1 in item.IdTaskNavigation.TasksTags)
                {
                    
                    var Tag = new TagsViewModel
                    {
                        Id = item1.IdTag,
                        Name = item1.IdTagNavigation.Name
                    };
                    lstTag.Add(Tag);
                }
                var ContentReturn = new ContentViewer
                {
                    IdTask = item.IdTaskNavigation.Id,
                    PublishTime = item.IdTaskNavigation.PublishTime,
                    Contents = Cnt,
                    Image = imgs,
                    ListTags = lstTag
                };
                lstContentReturn.Add(ContentReturn);
            }
            return lstContentReturn;
        }

        public static List<string> getImage(string content)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(content);
            var imgs = doc.DocumentNode.Descendants("img");
            List<string> listImg = new List<string>();

            foreach (var img in imgs)
            {
                listImg.Add(img.GetAttributeValue("src", null));
            }

            return listImg;
        }
    }
}
