using System;
using TwitchLib.Client;

namespace RG.Bot.Base
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
