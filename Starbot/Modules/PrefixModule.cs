using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Starbot.Types;

namespace Starbot.Modules
{
    public class PrefixModule : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        public async Task HandlePingCommand()
        {
            await Context.Message.ReplyAsync("Stop pinging me!");
        }

        [Command("i-help")]
        public async Task HandleHelpCommand()
        {
            EmbedBuilder builder = new EmbedBuilder();

            builder.WithTitle("Help-Commandlist");

            builder.AddField("!ping", "Sends a ping to the bot, to see if its online\n", false);

            builder.AddField("!help-baby", "Shows you how you can add a new Baby suggestion", false);
            builder.AddField("!help-item", "Shows you how you can add a new Item suggestion", false);
            builder.AddField("!help-item-active", "Shows you how you can add a new Active Item suggestion", false);
            builder.AddField("!help-enemy", "Shows you how you can add a new Enemy suggestion", false);

            IUserMessage userMessage = await Context.Channel.SendMessageAsync("", false, builder.Build());
        }

        [Command("help-baby")]
        public async Task HandleHelpBabyCommand()
        {
            EmbedBuilder builder = new EmbedBuilder();

            builder.WithTitle("!idea-baby");

            builder.AddField("Adds a new Baby idea by using the following syntax", "Name= \nCost= \nHP= \nType= \nDamage= \nFirerate= \nRecharge= \nAbilities= ", false);

            IUserMessage userMessage = await Context.Channel.SendMessageAsync("", false, builder.Build());
        }

        [Command("help-item")]
        public async Task HandleHelpItemCommand()
        {
            EmbedBuilder builder = new EmbedBuilder();

            builder.WithTitle("!idea-item");

            builder.AddField("Adds a new Item idea by using the following syntax", "Name= \nCost= \nTier= \nDescription= \nEffect= ", false);

            IUserMessage userMessage = await Context.Channel.SendMessageAsync("", false, builder.Build());
        }

        [Command("help-item-active")]
        public async Task HandleHelpItemActiveCommand()
        {
            EmbedBuilder builder = new EmbedBuilder();

            builder.WithTitle("!idea-item-active");

            builder.AddField("Adds a new Active Item idea by using the following syntax", "Name= \nDescription= \nEffect= ", false);

            IUserMessage userMessage = await Context.Channel.SendMessageAsync("", false, builder.Build());
        }

        [Command("help-enemy")]
        public async Task HandleHelpEnemyCommand()
        {
            EmbedBuilder builder = new EmbedBuilder();

            builder.WithTitle("!idea-enemy");

            builder.AddField("Adds a new Baby idea by using the following syntax", "Name= \nHP= \nType= \nDamage= \nFirerate= \nWalkspeed= \nAbilities= \nAppearance= ", false);

            IUserMessage userMessage = await Context.Channel.SendMessageAsync("", false, builder.Build());
        }

        [Command("idea-baby")]
        public async Task HandleBabyIdeaCommand([Remainder]string text)
        {
            IdeaHandler ideaHandler = new IdeaHandler(Context);
            await ideaHandler.NewIdea(text, "Baby");
        }

        [Command("idea-item")]
        public async Task HandleItemIdeaCommand([Remainder] string text)
        {
            IdeaHandler ideaHandler = new IdeaHandler(Context);
            await ideaHandler.NewIdea(text, "Item");
        }

        [Command("idea-item-active")]
        public async Task HandleItemActiveIdeaCommand([Remainder] string text)
        {
            IdeaHandler ideaHandler = new IdeaHandler(Context);
            await ideaHandler.NewIdea(text, "ItemActive");
        }

        [Command("idea-enemy")]
        public async Task HandleEnemyIdeaCommand([Remainder] string text)
        {
            IdeaHandler ideaHandler = new IdeaHandler(Context);
            await ideaHandler.NewIdea(text, "Enemy");
        }
    }
}
