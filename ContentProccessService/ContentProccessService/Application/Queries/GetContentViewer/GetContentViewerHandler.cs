using ContentProccessService.Entities;
using ContentProccessService.Models;
using HtmlAgilityPack;
using iText.StyledXmlParser.Jsoup;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContentProccessService.Application.Queries.GetContentViewer
{
    public class GetContentViewerHandler : IRequestHandler<GetContentViewerRequest, List<ContentViewer>>
    {
        private readonly ContentoContext _context;
        public GetContentViewerHandler(ContentoContext contentodbContext)
        {
            _context = contentodbContext;
        }
        public async Task<List<ContentViewer>> Handle(GetContentViewerRequest request, CancellationToken cancellationToken)
        {
            if (request.Tags.Count > 0 && !request.Tags.Contains(0))
            {
                var content = _context.Tasks.AsNoTracking()
              .Where(x => x.Status == 7
              && x.Contents.Any(t => t.IsActive == true)
              && x.TasksChannels.Any(t => t.IdChannel == 1)
              && x.TasksTags.Any(z => request.Tags.Contains(z.IdTag)))
              .OrderByDescending(x=>x.PublishTime)
              .Select(x => new
              {
                  x,
                  Contents = x.Contents.Where(c => c.IsActive == true).FirstOrDefault(),
                  TasksTags = x.TasksTags.ToList()
              }).ToList();
                var lstContentReturn = new List<ContentViewer>();
                foreach (var item in content)
                {
                    List<string> imgs = getImage(item.Contents.TheContent);
                    if (imgs.Count == 0)
                    {
                        imgs.Add("https://marketingland.com/wp-content/ml-loads/2015/11/content-marketing-idea-lightbulb-ss-1920.jpg");
                    }
                    var Cnt = new ContentModels
                    {
                        Id = item.Contents.Id,
                        Name = item.Contents.Name
                    };
                    var lstTag = new List<TagsViewModel>();
                    foreach (var item1 in item.TasksTags)
                    {
                        var Tag = new TagsViewModel
                        {
                            Id = item1.IdTag,
                            Name = _context.Tags.Find(item1.IdTag).Name


                        };
                        lstTag.Add(Tag);
                    }
                    var ContentReturn = new ContentViewer
                    {
                        IdTask = item.x.Id,
                        PublishTime = item.x.PublishTime,
                        Contents = Cnt,
                        Image = imgs,
                        ListTags = lstTag
                    };
                    lstContentReturn.Add(ContentReturn);
                }

                return lstContentReturn;
            }
            else
            {
                var content = _context.Tasks.AsNoTracking()
              .Where(x => x.Status == 7
              && x.Contents.Any(t => t.IsActive == true)
              && x.TasksChannels.Any(t => t.IdChannel == 1))
              .OrderByDescending(x => x.PublishTime)
              .Select(x => new
              {
                  x,
                  Contents = x.Contents.Where(c => c.IsActive == true).FirstOrDefault(),
                  TasksTags = x.TasksTags.ToList()
              }).ToList();
                var lstContentReturn = new List<ContentViewer>();
                foreach (var item in content)
                {
                    List<string> imgs = getImage(item.Contents.TheContent);
                    if (imgs.Count == 0)
                    {
                        imgs.Add("https://marketingland.com/wp-content/ml-loads/2015/11/content-marketing-idea-lightbulb-ss-1920.jpg");
                    }
                    var Cnt = new ContentModels
                    {
                        Id = item.Contents.Id,
                        Name = item.Contents.Name
                    };
                    var lstTag = new List<TagsViewModel>();
                    foreach (var item1 in item.TasksTags)
                    {
                        var Tag = new TagsViewModel
                        {
                            Id = item1.IdTag,
                            Name = _context.Tags.Find(item1.IdTag).Name
                        };
                        lstTag.Add(Tag);
                    }
                    var ContentReturn = new ContentViewer
                    {
                        IdTask = item.x.Id,
                        PublishTime = item.x.PublishTime,
                        Contents = Cnt,
                        Image = imgs,
                        ListTags = lstTag
                    };
                    lstContentReturn.Add(ContentReturn);
                }

                return lstContentReturn;
            }

        }
        public String html2text(String html)
        {
            return Jsoup.Parse(html).Text();
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
