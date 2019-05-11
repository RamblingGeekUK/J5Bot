using TwitchLib.Client.Events;

namespace RG.Bot.Base
{
    public interface ICommand
    {
        void Execute(OnChatCommandReceivedArgs e);
    }
}
