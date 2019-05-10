using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchLib.Client.Events;

namespace J5.Bot.Bot.Base
{
    public interface ICommand
    {
        void Execute(OnChatCommandReceivedArgs e);
    }
}
