using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordPressPCL;
using WordPressPCL.Models;

namespace PublishService.Utility
{
    public class Wordpress
    {
        private readonly string _wpuri = "https://contento12345670.000webhostapp.com/wp-json/";
        private readonly string _wpacc = "admin";
        private readonly string _wppwd = "admin";
        private async Task CreatePost(string title, string content, int[] tags)
        {
            try
            {
                WordPressClient client = await GetClient();
                if (await client.IsValidJWToken())
                {
                    var post = new Post
                    {
                        Title = new Title(title),
                        Content = new Content(content),
                        Tags = tags
                    };
                    await client.Posts.Create(post);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error:" + e.Message);
            }
        }

        private async Task<WordPressClient> GetClient()
        {
            // JWT authentication
            var client = new WordPressClient(_wpuri);
            client.AuthMethod = AuthMethod.JWT;
            await client.RequestJWToken(_wpacc, _wppwd);
            return client;
        }
    }
}
