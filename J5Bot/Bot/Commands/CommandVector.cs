using System;
using System.Net.Http;
using System.Text;
using TwitchLib.Client;
using TwitchLib.Client.Events;

namespace RG.Bot.Base
{
    public class CommandVector : CommandBase, ICommand
    {
        public CommandVector(TwitchClient client)
            : base(client)
        {
        }
        
        public void Execute(OnChatCommandReceivedArgs e)
        {
            string message = e.Command.ChatMessage.Message;
            message = message.Substring(11, message.Length - 11);
            Console.WriteLine("Vector should say the following: " + message);
            BotVector(e, message);
        }
        private void BotVector(OnChatCommandReceivedArgs e, string message)
        {
            client.SendMessage(e.Command.ChatMessage.Channel, "Sending..");
            Vector(message, e);
        }
    }
}
