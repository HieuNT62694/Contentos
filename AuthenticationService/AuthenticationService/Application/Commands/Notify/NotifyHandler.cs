using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using Firebase.Database;
using Firebase.Database.Query;

namespace AuthenticationService.Application.Commands.Notify
{
    public class NotifyHandler : IRequestHandler<NotifyCommands,bool>
    {
        public async Task<bool> Handle(NotifyCommands request, CancellationToken cancellationToken)
        {
            var auth = "gREVaDJG8lOlOsWVj4J9tdKsnADGa8VkDlTuNP55";
            var firebase = new FirebaseClient("https://contento-d8c16.firebaseio.com/", new FirebaseOptions
            { AuthTokenAsyncFactory = () => Task.FromResult(auth) });
            // add new item to list of data and let the client generate new key for you (done offline)


            //// note that there is another overload for the PostAsync method which delegates the new key generation to the firebase server

            //Console.WriteLine($"Key for the new dinosaur: {dino.Key}");

            // add new item directly to the specified location (this will overwrite whatever data already exists at that location)
            await firebase.Child("message")
              .PosttAsync(request);

            
            return true;

           
        }
    }
}
