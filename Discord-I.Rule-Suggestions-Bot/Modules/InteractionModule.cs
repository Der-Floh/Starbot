using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Interactions;
using Discord_I.Rule_Suggestions_Bot.Log;

namespace Discord_I.Rule_Suggestions_Bot.Modules
{
    public class InteractionModule : InteractionModuleBase<SocketInteractionContext>
    {
        public InteractionService Commands { get; set; }
        private static Logger _logger;

        public InteractionModule(ConsoleLogger logger)
        {
            _logger = logger;
        }

        [SlashCommand("ping", "Receive a ping message!")]
        public async Task HandlePingCommand()
        {
            await _logger.Log(new LogMessage(LogSeverity.Info, "PingModule : Ping", $"User: {Context.User.Username}, Command: ping", null));
            await RespondAsync("Stop pinging me!");
        }
    }
}
