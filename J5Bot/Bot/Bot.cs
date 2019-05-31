using RG.Bot.Base;
using System;
using System.Collections.Generic;
using TwitchLib.Client;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Events;
using TwitchLib.Client.Extensions;
using TwitchLib.Client.Models;


namespace RG.Bot
{
    public class Bot
    {
        private readonly TwitchClient client;
        private readonly Dictionary<string, ICommand> commands;

        public Bot()
        {

            ConnectionCredentials credentials = new ConnectionCredentials(Settings.Twitch_botusername, Settings.Twitch_token);

            this.client = new TwitchClient();
            this.client.Initialize(credentials, Settings.Twitch_channel);

            this.client.OnLog += Client_OnLog;
            this.client.OnJoinedChannel += Client_OnJoinedChannel;
            this.client.OnMessageReceived += Client_OnMessageReceived;
            this.client.OnWhisperReceived += Client_OnWhisperReceived;
            this.client.OnNewSubscriber += Client_OnNewSubscriber;
            this.client.OnConnected += Client_OnConnected;
            this.client.OnChatCommandReceived += Client_OnChatCommandReceived;
            this.client.Connect();

            this.commands = new Dictionary<string, ICommand>
            {
                { "alive", new CommandALive(client) },
                { "vector-say", new CommandVector(client) },
                { "vector-joke", new CommandTellJoke(client) },
                { "attention", new CommandAttention(client) }
            };
        }


        private void Client_OnLog(object sender, OnLogArgs e)
        {
            Console.WriteLine($"{e.DateTime.ToString()}: {e.BotUsername} - {e.Data}");
        }
     
        private void Client_OnConnected(object sender, OnConnectedArgs e)
        {
            Console.WriteLine($"Connected to {e.AutoJoinChannel}");
        }

        private void Client_OnJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            Console.WriteLine("J5 is connected to chat");
            new CommandAnnounce(client).Execute(e);
        }

        private void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            if (e.ChatMessage.Message.Contains("badword"))
                client.TimeoutUser(e.ChatMessage.Channel, e.ChatMessage.Username, TimeSpan.FromMinutes(1), "Bad word! 1 minute timeout!");

        }
        private void Client_OnChatCommandReceived(object sender, TwitchLib.Client.Events.OnChatCommandReceivedArgs e)
        {
            if (this.commands.ContainsKey(e.Command.CommandText) == false)
                return;

            this.commands[e.Command.CommandText].Execute(e);
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
    }
}
