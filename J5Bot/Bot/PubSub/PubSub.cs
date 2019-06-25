using RG.Bot.Base;
using System;
using TwitchLib.Client.Events;
using TwitchLib.PubSub;
using TwitchLib.PubSub.Events;


namespace RG.Bot.PubSub
{
    public class PubSub : CommandBase
    {

        public PubSub(TwitchPubSub twitchPubSub) : base(twitchPubSub)
        {
           
        }

        public void Execute()
        {
            this.clientpubsub.OnPubSubServiceConnected += OnPubSubServiceConnected;
            this.clientpubsub.OnBitsReceived += OnBitsReceived;
            this.clientpubsub.OnListenResponse += OnListenResponse;
            this.clientpubsub.OnChannelSubscription += OnChannelSub;
            this.clientpubsub.OnFollow += OnChannelFollow;
            this.clientpubsub.Connect();
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

        public void OnChannelFollow(object sender, OnFollowArgs e)
        {
            var message = $"Thank you for the follow, {e.DisplayName}, it's appreciated!!";
            Console.WriteLine(message);
           // new CommandAnnounce(client).Execute(message);
        }

        public void OnChannelSub(object sender, OnChannelSubscriptionArgs e)
        {
            var message = $"Thank you for the sub, {e.Subscription.DisplayName}, it's appreciated!!";
            Console.WriteLine(message);
            //new CommandAnnounce(client).Execute(message);
        }
        public void OnBitsReceived(object sender, OnBitsReceivedArgs e)
        {
            var message = $"Just received {e.BitsUsed} bits from {e.Username}. That brings their total to {e.TotalBitsUsed} bits!";
            Console.WriteLine(message);
            //  new CommandAnnounce(client).Execute(message);

        }

       

    }
}
