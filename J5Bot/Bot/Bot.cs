using RG.Bot.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;

namespace RG.Bot
{

    public interface IPubSubService
    {
        Task Execute();
    }

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
            this.client.OnConnected += Client_OnConnected;
            this.client.OnChatCommandReceived += Client_OnChatCommandReceived;
            this.client.Connect();

            this.commands = new Dictionary<string, ICommand>
            {
                { "alive", new CommandALive(client) },
                { "vector-say", new CommandSay(client) },
                { "vector-joke", new CommandTellJoke(client) },
                { "attention", new CommandAttention(client) },
                { "lurk", new CommandLurk(client) },
                { "unlurk", new CommandUnLurk(client) },
                { "freeplay", new CommandFreePlay(client) },
                { "commands", new CommandCommands(client) },
            };
        }

        private void Client_OnJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            Console.WriteLine("J5 is connected to chat");
            //Run_cmd("D:\\Dev\\Stream\\Vector\\VectorREST\\remote_control.py","");
            new CommandAnnounce(client).Execute("Vector is Alive!", e);
        }

        private void Run_cmd(string cmd, string args)
        {
            ProcessStartInfo start = new ProcessStartInfo
            {
                FileName = "py",
                Arguments = string.Format("{0} {1}", cmd, args),
                UseShellExecute = false,
                RedirectStandardOutput = true
            };
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    Console.Write(result);
                }
            }
        }

        private static void Client_OnLog(object sender, OnLogArgs e)
        {
            Console.WriteLine($"{e.DateTime.ToString()}: {e.BotUsername} - {e.Data}");
        }
     
        private static void Client_OnConnected(object sender, OnConnectedArgs e)
        {
            Console.WriteLine($"Connected to {e.AutoJoinChannel}");
        }

        private void Client_OnChatCommandReceived(object sender, TwitchLib.Client.Events.OnChatCommandReceivedArgs e)
        { 
            if (this.commands.ContainsKey(e.Command.CommandText.ToLower()) == false)
                return;

            this.commands[e.Command.CommandText.ToLower()].Execute(e);
        }
    }
}
