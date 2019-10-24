using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FBTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //var rez = Task.Run(async () =>
            //{
            //    //string url = "https://graph.facebook.com/oauth/access_token?client_id=1733081480160604&client_secret=69301b3208e86a66d904de4e7bd7bc6d&grant_type=client_credentials";
            //    //string url = "https://graph.facebook.com/106817360743488?fields=access_token&access_token=1733081480160604|kjQFmOSFrazNHfdZleITHnVc3Wk";

            //    //string url = "https://graph.facebook.com/oauth/access_token?client_id=1733081480160604&client_secret=69301b3208e86a66d904de4e7bd7bc6d&grant_type=fb_exchange_token&fb_exchange_token=EAAYoOnn95VwBAFno2C6AlLZC6Y8xzBlqUUlkmOZCoRbPhsoHM0ZAEqOpZClBO1aUbOZB2m7nzsEgKMIgyT7bZC3dXazycEdJHcrk9DXrpmxHqVSiJo4Dccz0ezGRfrkxAkhlIrX2YNlybvNh0sDOz3fL6HM0ahxk7PCfAKHqHd6YZACxiqhJbooy2xI07q5LUhPrMfv8BHXVQZDZD";

            //    //string url = "https://graph.facebook.com/me/accounts?access_token=EAAYoOnn95VwBAMZBZAMjLZBn86I104sFc6Ueqvw0W10ybhpZCfcBrKEZAqYOpKiJfolYiVDKKZBbvrZBxOZAKTuBGld3tMO2ex95l4f6vpOuXpTPwOoUgTTZB3o81lnQRvBsaUrbPhZBeU1A0ZACIhzdffLYg8aSLaZCilrqUV0AOmoXK0QC8lpqCOc7";


            //    using (var http = new HttpClient())
            //    {
            //        var httpResponse = await http.GetAsync(url);
            //        var httpContent = await httpResponse.Content.ReadAsStringAsync();

            //        return httpContent;
            //    }
            //});
            //var rezJson = JObject.Parse(rez.Result);
            //Console.WriteLine(rezJson);

            Facebook facebook = new Facebook("EAAYoOnn95VwBAHdhFDev4ZA7GqN5lbxZCG8yrDxuOl0XuPmvTIvTcmGrsSrO7UHdCKnMjRBjeTgU1G1H0l4Bc6rcP5rbEmFhcizpZCn5IcKHFvMWT4mDNR5X6xzqnM35rdIFEjlOvKArj2QBraYa3JLoY9SDIZBvJkSJSPJvKEZB5zhs3u4f1v2IoZBFFStpAZD", "106817360743488");

            //Facebook facebook = new Facebook("EAAYoOnn95VwBAAlPnR36ppAC8e5YiQfzsdTrZCXQA9KQcKGfeB10EJwaHnRcKrnlBlBXb2STHQkkE2gysiNR8y70PGm4EzMd8qq7TBCAyw5xSKkvZBHp4pvJRCLjSZAdMeMBZBw7eV15eaAKxwcZCXq8H2XqZBOfwp5ry5xjDXbYfdam0psr38", "106817360743488");
            var rezText = Task.Run(async () =>
            {
                using (var http = new HttpClient())
                {
                    return await facebook.PublishSimplePost("Test schedule 3");
                }
            });
            var rezTextJson = JObject.Parse(rezText.Result.Item2);
            if (rezText.Result.Item1 != 200)
            {
                try // return error from JSON
                {
                    Console.WriteLine($"Error posting to Facebook. {rezTextJson["error"]["message"].Value<string>()}");
                    return;
                }
                catch (Exception ex) // return unknown error
                {
                    // log exception somewhere
                    Console.WriteLine($"Unknown error posting to Facebook. {ex.Message}");
                    return;
                }
            }
            Console.WriteLine(rezTextJson);
        }
    }
}
