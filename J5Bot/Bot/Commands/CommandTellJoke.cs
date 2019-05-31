using RG.Bot.Base;
using System;
using System.Collections.Generic;
using TwitchLib.Client;
using TwitchLib.Client.Events;

namespace RG.Bot.Base
{
    public class CommandTellJoke : CommandBase, ICommand
    {
        public CommandTellJoke(TwitchClient client)
            : base(client)
        {
        }

        public void Execute(OnChatCommandReceivedArgs e)
        {
            client.SendMessage(e.Command.ChatMessage.Channel, "Sending..");

            var joke = selectJoke();

            this.Say(e.Command.ChatMessage.Channel, joke);
            this.Vector(joke, e);
        }

        private string selectJoke()
        {
            var random = new Random();

            List<string> Joke = new List<string>();
            Joke.Add("Don't bash tech. Unless you're in the command line.");
            Joke.Add("Knock Knock, Async Function, Who's there?");
            Joke.Add("How do you get the code for the bank vault?, You checkout their branch.");
            Joke.Add("How did the developer announce their engagement?, They returned true");
            Joke.Add("What do you call a busy waiter?, A server."); 
            Joke.Add("What do you call an idle server? A waiter.");
            Joke.Add("Please enter password, fornite, Error: Password is two weak");
            Joke.Add("How many Prolog programmers does it take to change a lightbulb? Yes");
            Joke.Add("I’ve been hearing news about this big boolean. Huge if true.");
            Joke.Add("What diet did the ghost developer go on? Boolean");
            Joke.Add("Why was the developer unhappy at their job? They wanted arrays.");
            Joke.Add("Why did 10 get paid less than '10'? There was workplace inequality.");
            Joke.Add("Why was the function sad after a successful first call? It didn’t get a callback.");
            Joke.Add("Why did the angry function exceed the callstack size? It got into an Argument with itself");

            int index = random.Next(Joke.Count);
            return Joke[index];
        }
    }
}
