using TwitchLib.Client;
using TwitchLib.Client.Events;

namespace RG.Bot.Base
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
