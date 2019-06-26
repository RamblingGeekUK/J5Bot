using System;
using System.Threading.Tasks;
using TwitchLib.PubSub;
using TwitchLib.PubSub.Events;

namespace RG.Bot.PubSub
{
    public class PubSubService : IPubSubService
    {

        protected readonly TwitchPubSub clientpubsub;

        public PubSubService()
        {
            this.clientpubsub.OnPubSubServiceConnected += OnPubSubServiceConnected;
            this.clientpubsub.OnListenResponse += OnListenResponse;
            this.clientpubsub.Connect();
        }

        public Task Execute()
        {
            this.clientpubsub.OnPubSubServiceConnected += OnPubSubServiceConnected;
            this.clientpubsub.OnListenResponse += OnListenResponse;
            this.clientpubsub.Connect();

            return Task.FromResult(0);
        }

        public void OnPubSubServiceConnected(object sender, System.EventArgs e)
        {
            var TwitchID = Settings.Twitch_ID;
            var TwitchOAuthToken = Settings.Twitch_OAuth;

            clientpubsub.ListenToBitsEvents(TwitchID);
            clientpubsub.ListenToSubscriptions(TwitchID);
            clientpubsub.ListenToFollows(TwitchID);
            clientpubsub.SendTopics(TwitchOAuthToken);
        }

        public static void OnListenResponse(object sender, OnListenResponseArgs e)
        {
            if (e.Successful)
            {
                Console.WriteLine($"Successfully verified listening to topic: {e.Topic}");
            }
            else
            {
                Console.WriteLine($"Failed to listen! Error: {e.Response.Error}");
            }
        }

     
    }
}
