using J5.Bot.Bot.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Client;
using TwitchLib.Client.Events;

namespace J5.Bot.Bot.Commands
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
        private void Vector(string say, OnChatCommandReceivedArgs e)
        {
            // CMChrisJones
            var sayAsByteArray = Encoding.UTF8.GetBytes(say);
            var encoded = System.Convert.ToBase64String(sayAsByteArray);
            //var safe = HttpUtility.HtmlEncode(say);
            var launchEndpoint = "http://localhost:5000/vector/say/" + encoded;

            try
            {
                var client = new HttpClient();
                Console.WriteLine("Calling API ..." + encoded);

                var result = client.GetAsync(launchEndpoint).Result;
            }
            catch
            {
                Console.WriteLine("Called failed, check the Vector API is running");
                this.Say(e.Command.ChatMessage.Channel ,"Message Sent...");
            }
        }
    }
}
