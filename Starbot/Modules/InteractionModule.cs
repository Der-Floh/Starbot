using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Interactions;
using Starbot.Log;

namespace Starbot.Modules
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

        private async Task SendError()
        {
            await RespondAsync("```diff\n- The command resolved in an error!\n```");
        }
    }
}
