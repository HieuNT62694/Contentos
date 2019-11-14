﻿using BatchjobService.Entities;
using BatchjobService.Utulity;
using FBTest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BatchjobService.HangFireService
{
    public interface IPublishFBService
    {
        Task PublishToContento(int taskId, bool isAds, DateTime? adsTime);
        Task PublishToWP(int fanpageId, int contentId);
        Task PublishToFB(int fanpageId, int contentId);
    }
    public class PublishFB : IPublishFBService
    {
        private readonly ContentoDbContext _context;

        public PublishFB(ContentoDbContext contentodbContext)
        {
            _context = contentodbContext;
        }
        public async Task PublishToFB(int fanpageId, int contentId)
        {
            var fanpage = _context.Fanpages.FirstOrDefault(f => f.Id == fanpageId && f.IsActive == true);
            var content = _context.Contents.FirstOrDefault(w => w.Id == contentId && w.IsActive == true);
            var upTask = _context.Tasks.FirstOrDefault(x => x.Id == content.IdTask);
            if (upTask.Status != 7)
            {
                upTask.Status = 7;
                _context.Update(upTask);
                _context.SaveChanges();
            }

            var pageId = await GetPageIdAsync(fanpage.Token);

            Facebook facebook = new Facebook(fanpage.Token, pageId);

            var post = Helper.removeHtml(content.TheContent);

            List<string> imgs = Helper.getImage(content.TheContent);

            if(imgs.Count == 0)
            {
                using (var http = new HttpClient())
                {
                    await facebook.PublishSimplePost(post);
                }
            }
            else
            {
                using (var http = new HttpClient())
                {
                    facebook.PublishToFacebook(post, imgs);
                }
            }
        }



        public async Task PublishToWP(int fanpageId, int contentId)
        {
            var fanpage = _context.Fanpages.FirstOrDefault(f => f.Id == fanpageId && f.IsActive == true);
            var content = _context.Contents.FirstOrDefault(w => w.Id == contentId && w.IsActive == true);
            var upTask = _context.Tasks.FirstOrDefault(x => x.Id == content.IdTask);
            if(upTask.Status != 7)
            {
                upTask.Status = 7;
                _context.Update(upTask);
                _context.SaveChanges();
            }
            Wordpress wordpress = new Wordpress();

            await wordpress.PublishSimplePost(content, fanpage.Token);
        }

        public async Task PublishToContento(int taskId, bool isAds, DateTime? adsTime)
        {
            var task = _context.Tasks.FirstOrDefault(x => x.Id == taskId);

            if (task.Status != 7)
            {
                if (isAds)
                {
                    task.AdsDate = adsTime;
                }
                task.IsAds = isAds;
                task.Status = 7;
                _context.Update(task);
                await _context.SaveChangesAsync();
            }
        }

        private async Task<string> GetPageIdAsync(string token)
        {
            using (var http = new HttpClient())
            {
                var httpResponse = await http.GetAsync("https://graph.facebook.com/me?access_token=" + token);

                var httpContent = await httpResponse.Content.ReadAsStringAsync();

                var results = JsonConvert.DeserializeObject<dynamic>(httpContent);

                return results["id"];
            }
        }
    }
}   
