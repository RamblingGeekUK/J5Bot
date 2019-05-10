using System;
using System.Windows.Input;
using TwitchLib.Client;
using TwitchLib.Client.Events;

namespace J5.Bot.Bot.Commands
{
    public class CommandAnnounce : CommandBase
    {
        public CommandAnnounce(TwitchClient client)
            : base(client)
        {
        }

        public void Execute(OnJoinedChannelArgs e)
        {
            this.Say(e.Channel, " is ALIVE!");
        }
    }
}
