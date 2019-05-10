using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchLib.Client;
using TwitchLib.Client.Models;

namespace J5.Bot.Bot.Commands
{
    public abstract class CommandBase
    {
        protected readonly TwitchClient client;

        public CommandBase(TwitchClient client)
        {
            this.client = client;
        }
        protected void Say(string channel, string message)
        {
            Console.WriteLine(message);
            this.client.SendMessage(channel, message);
        }
    }
}
