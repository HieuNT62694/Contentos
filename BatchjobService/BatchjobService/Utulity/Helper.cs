using HtmlAgilityPack;
using iText.StyledXmlParser.Jsoup;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WordPressPCL;
using WordPressPCL.Models;

namespace BatchjobService.Utulity
{
    public static class Helper
    {
        public static string removeHtml(string content)
        {
            var con = Regex.Replace(content, "<[a/].*?>", "");

            var con2 = Regex.Replace(con, "<[a-zA-Z/].*?>", Environment.NewLine);

            return Jsoup.Parse(con2).Text();
        }

        public static List<string> getImage(string content)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(content);
            var imgs = doc.DocumentNode.Descendants("img");
            List<string> listImg = new List<string>();

            foreach(var img in imgs)
            {
                listImg.Add(img.GetAttributeValue("src", null));
            }

            return listImg;
        }

        public static async Task<string> FBTokenValidate(string token)
        {
            using (var http = new HttpClient())
            {
                var httpResponse = await http.GetAsync("https://graph.facebook.com/me?access_token=" + token);

                var httpContent = await httpResponse.Content.ReadAsStringAsync();

                var results = JsonConvert.DeserializeObject<dynamic>(httpContent);

                if (results["error"] != null)
                {
                    return "";
                }
                string url = "https://www.facebook.com/" + results["id"];

                return url;
            }
        }

        public static async Task<string> WPTokenValidate(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var iis = handler.ReadToken(token).Issuer;
                var host = iis + "/wp-json/";
                var client = new WordPressClient(host);

                client.AuthMethod = AuthMethod.JWT;
                client.SetJWToken(token);

                if (await client.IsValidJWToken())
                {
                    return iis;
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            } 
        }
    }
}
