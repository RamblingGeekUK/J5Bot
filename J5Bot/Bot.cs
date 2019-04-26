using System;
using TwitchLib.Client;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Events;
using TwitchLib.Client.Extensions;
using TwitchLib.Client.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;

namespace J5Bot
{
    public class Bot 
    {
        readonly TwitchClient client;

        public Bot()
        {

            ConnectionCredentials credentials = new ConnectionCredentials(Settings.Twitch_botusername, Settings.Twitch_token);

            client = new TwitchClient();
            client.Initialize(credentials, Settings.Twitch_channel);

            client.OnLog += Client_OnLog;
            client.OnJoinedChannel += Client_OnJoinedChannel;
            client.OnMessageReceived += Client_OnMessageReceived;
            client.OnWhisperReceived += Client_OnWhisperReceived;
            client.OnNewSubscriber += Client_OnNewSubscriber;
            client.OnConnected += Client_OnConnected;

            client.Connect();

        }


        private void Client_OnLog(object sender, OnLogArgs e)
        {
            Console.WriteLine($"{e.DateTime.ToString()}: {e.BotUsername} - {e.Data}");
        }

        private void BotAlive(OnMessageReceivedArgs e)
        {
            Console.WriteLine(" is ALIVE!");
            client.SendMessage(e.ChatMessage.Channel, " is ALIVE!");
        }
        private void BotAlive(OnJoinedChannelArgs e)
        {
            Console.WriteLine(" is ALIVE!");
            client.SendMessage(e.Channel, " is ALIVE!");
        }

        private void BotVector(OnMessageReceivedArgs e, string message)
        {
            client.SendMessage(e.ChatMessage.Channel, "Sending..");
            Vector(message);
        }

        private void Client_OnConnected(object sender, OnConnectedArgs e)
        {
            Console.WriteLine($"Connected to {e.AutoJoinChannel}");
        }

        private void Client_OnJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            Console.WriteLine("J5 is connected to chat");
            BotAlive(e);
        }

        private void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            if (e.ChatMessage.Message.Contains("badword"))
                client.TimeoutUser(e.ChatMessage.Channel, e.ChatMessage.Username, TimeSpan.FromMinutes(1), "Bad word! 1 minute timeout!");

            if (e.ChatMessage.Message.Contains("!alive"))
                BotAlive(e);

            if (e.ChatMessage.Message.Contains("!vector say"))
            {
                string message = e.ChatMessage.Message;

                message = message.Substring(11, message.Length - 11);

                Console.WriteLine("Vector should say the following: " + message);

                BotVector(e, message);
            }
        }


        private void Client_OnWhisperReceived(object sender, OnWhisperReceivedArgs e)
        {
            if (e.WhisperMessage.Username == "my_friend")
                client.SendWhisper(e.WhisperMessage.Username, "Hey! Whispers are so cool!!");
        }

        private void Client_OnNewSubscriber(object sender, OnNewSubscriberArgs e)
        {
            if (e.Subscriber.SubscriptionPlan == SubscriptionPlan.Prime)
                client.SendMessage(e.Channel, $"Welcome {e.Subscriber.DisplayName} to the substers! You just earned 500 points! So kind of you to use your Twitch Prime on this channel!");
            else
                client.SendMessage(e.Channel, $"Welcome {e.Subscriber.DisplayName} to the substers! You just earned 500 points!");
        }

        private void Vector(string say)
        {
            var launchEndpoint = "http://localhost:5000/vector/say/" + say;

            var client = new HttpClient();
            Console.WriteLine("Calling API ..." + launchEndpoint);

            var result = client.GetAsync(launchEndpoint).Result;

        }
    }
}
