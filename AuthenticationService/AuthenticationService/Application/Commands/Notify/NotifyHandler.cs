using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using System.IO;
using System.Net;

namespace AuthenticationService.Application.Commands.Notify
{
    public class NotifyHandler : IRequestHandler<NotifyCommands,bool>
    {
        public async Task<bool> Handle(NotifyCommands request, CancellationToken cancellationToken)
        {
          
            try
            {
                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                //serverKey - Key from Firebase cloud messaging server  
                tRequest.Headers.Add(string.Format("Authorization: key={0}", "AAAAAHxqgzU:APA91bFCTHWFUIrq-aEFuP7uepcpnF_IRQF6RRZ2taQIzw-ns51gqnc1u6uvEFaTnYsLpH0goSwn4gMOciOcAuRyaPuuk6Z-2ttJ09oquYQ9JYYZC8pDPVOQ3EYTyqRwYWBXE25HKy01"));
                //Sender Id - From firebase project setting  
                tRequest.Headers.Add(string.Format("Sender: id={0}", "2087355189"));
                tRequest.ContentType = "application/json";
                var payload = new
                {
                    to = "cKddtT0riAU:APA91bHks4sUqORwyF3c0gCaTWaLJbSUNe2CLCyj4yqPaWD5woBoPCqXHulDsHiA7zrDMJ5gK_vZ6UNObq_hX3oKQGz6v9G1N1nKP93XsTJCdODohyGBkNjSIMkcqZo8GL8CFKuTZun6",
                    priority = "high",
                    content_available = true,
                    notification = new
                    {
                        body = "Test",
                        title = "Test",
                        badge = 1
                    },
                    data = new
                    {
                        image = "Conmeongu",
                    },

                };

                string postbody = JsonConvert.SerializeObject(payload).ToString();
                Byte[] byteArray = Encoding.UTF8.GetBytes(postbody);
                tRequest.ContentLength = byteArray.Length;
                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            if (dataStreamResponse != null) using (StreamReader tReader = new StreamReader(dataStreamResponse))
                                {
                                    String sResponseFromServer = tReader.ReadToEnd();
                                    Console.WriteLine(sResponseFromServer);
                                }
                        }
                    }


                }
            }
            catch (Exception ex)
            {

                string str = ex.Message;
            }
            return true;

           
        }
    }
}
