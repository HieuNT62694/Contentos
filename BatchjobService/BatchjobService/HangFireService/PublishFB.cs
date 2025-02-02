﻿using BatchjobService.Entities;
using BatchjobService.Utulity;
using FBTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BatchjobService.HangFireService
{
    public interface IPublishFBService
    {
        Task PublishToWP(int id);
        Task PublishToFB(int id);
    }
    public class PublishFB : IPublishFBService
    {
        private readonly ContentoContext _context;

        public PublishFB(ContentoContext contentodbContext)
        {
            _context = contentodbContext;
        }
        public async Task PublishToFB(int id)
        {
            var content = _context.Contents.FirstOrDefault(w => w.Id == id && w.IsActive == true);
            //var upTask = _context.Tasks.FirstOrDefault(x => x.Id == content.IdTask);
            //if (upTask.Status != 7)
            //{
            //    upTask.Status = 7;
            //    _context.Update(upTask);
            //    _context.SaveChanges();
            //}

            //dang set cung
            Facebook facebook = new Facebook("EAAYoOnn95VwBAHdhFDev4ZA7GqN5lbxZCG8yrDxuOl0XuPmvTIvTcmGrsSrO7UHdCKnMjRBjeTgU1G1H0l4Bc6rcP5rbEmFhcizpZCn5IcKHFvMWT4mDNR5X6xzqnM35rdIFEjlOvKArj2QBraYa3JLoY9SDIZBvJkSJSPJvKEZB5zhs3u4f1v2IoZBFFStpAZD", "106817360743488");

            var post = Helper.removeHtml(content.TheContent);

            //List<string> imgs = Helper.getImage(content.TheContent);

            //Test Post With Multiple Photos
            List<string> imgs = new List<string>();
            imgs.Add("https://marketingland.com/wp-content/ml-loads/2015/11/content-marketing-idea-lightbulb-ss-1920.jpg");

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



        public async Task PublishToWP(int id)
        {
            var content = _context.Contents.FirstOrDefault(w => w.Id == id && w.IsActive == true);
            var upTask = _context.Tasks.FirstOrDefault(x => x.Id == content.IdTask);
            if(upTask.Status != 7)
            {
                upTask.Status = 7;
                _context.Update(upTask);
                _context.SaveChanges();
            }
            Wordpress wordpress = new Wordpress();

            await wordpress.PublishSimplePost(content);
        }


        
    }
}
