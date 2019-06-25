using TwitchLib.Client;
using TwitchLib.Client.Events;

namespace RG.Bot.Base
{
    public class CommandALive : CommandBase, ICommand
    {
        public CommandALive(TwitchClient client)
            : base(client)
        {
        }

        public void Execute(OnChatCommandReceivedArgs e)
        {
            this.SendMessage(e.Command.ChatMessage.Channel, " is ALIVE!");
        }
    }
}
