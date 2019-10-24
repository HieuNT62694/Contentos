using BatchjobService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordPressPCL;
using WordPressPCL.Models;

namespace BatchjobService.Utulity
{
    public class Wordpress
    {
        public async Task PublishSimplePost(Contents postText) {

            var client = new WordPressClient("https://contento12345670.000webhostapp.com/wp-json/");

            client.AuthMethod = AuthMethod.JWT;
            await client.RequestJWToken("admin", "admin");


            var post = new Post
            {
                Title = new Title(postText.Name),
                Content = new Content(postText.TheContent)
                //,
                //Tags = new int[] { 3, 4, 5, 6 }
            };

            if (await client.IsValidJWToken())
            {
                await client.Posts.Create(post);
            }
        }
    }
}
