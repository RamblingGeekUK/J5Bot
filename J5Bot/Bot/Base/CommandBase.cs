using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.PubSub;

namespace RG.Bot.Base
{
    public abstract class CommandBase
    {
        protected readonly TwitchClient client;
        protected readonly TwitchPubSub clientpubsub;
        protected readonly string VectorRestURL = "http://localhost:5000";

        public CommandBase(TwitchClient client)
        {
            this.client = client;
        }

        public CommandBase(TwitchPubSub clientpubsub)
        {
            this.clientpubsub = clientpubsub;
        }

        protected void SendMessage(string channel, string message)
        {
            Console.WriteLine(message);
            this.client.SendMessage(channel, message);
        }

        protected void VectorRESTPost()
        {
            try
            {
                var client = new HttpClient();
                Console.WriteLine("Calling API setFreeplayEnabled...");
                
                FreePlay freeplaycommand = new FreePlay();
                string json = JsonConvert.SerializeObject(freeplaycommand);
                var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
                var result = client.PostAsync(VectorRestURL + "/setFreeplayEnabled", content).Result;
                
                Console.WriteLine("Free play return status {0}: ", result.StatusCode);
            }
            catch
            {
                Console.WriteLine("Called failed, check the Vector API is running");
            }
        }

        internal class FreePlay
        {
#pragma warning disable IDE1006 // Naming Styles
            public Boolean isFreeplayEnabled { get; set; }
#pragma warning restore IDE1006 // Naming Styles
        }
    }
}