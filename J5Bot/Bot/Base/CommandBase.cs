using System;
using System.Net.Http;
using System.Text;
using TwitchLib.Client;
using TwitchLib.Client.Events;

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
        protected void Vector(string say, OnChatCommandReceivedArgs e)
        {
            // Added by CMChrisJones
            var sayAsByteArray = Encoding.UTF8.GetBytes(say);
            var encoded = System.Convert.ToBase64String(sayAsByteArray);
            var launchEndpoint = "http://192.168.1.23:4000/vector/say/" + encoded;

            try
            {
                var client = new HttpClient();
                Console.WriteLine("Calling API ..." + encoded);

                var result = client.GetAsync(launchEndpoint).Result;
            }
            catch
            {
                Console.WriteLine("Called failed, check the Vector API is running");
                this.Say(e.Command.ChatMessage.Channel, "Message Sent...");
            }
        }
    }
}