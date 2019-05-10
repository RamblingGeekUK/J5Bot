using J5.Bot.Bot.Base;
using System;
using TwitchLib.Client;
using TwitchLib.Client.Events;

namespace J5.Bot.Bot.Commands
{
    public class CommandALive : CommandBase, ICommand
    {
        public CommandALive(TwitchClient client)
            : base(client)
        {
        }

        public void Execute(OnChatCommandReceivedArgs e)
        {
            this.Say(e.Command.ChatMessage.Channel, " is ALIVE!");
        }
    }
}
