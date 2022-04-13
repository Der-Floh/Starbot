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
            try
            {
                await Context.Message.ReplyAsync("Stop pinging me!");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Catched Error");
                Console.WriteLine(ex);
                Console.ResetColor();
                await Context.Channel.SendMessageAsync("```diff\n- The command resolved in an error!\n\n" + ex + "```");
            }
        }

        [Command("i-help")]
        public async Task HandleHelpCommand()
        {
            try
            {
                EmbedBuilder builder = new EmbedBuilder();

                builder.WithTitle("Help-Commandlist");

                builder.AddField("!ping", "Sends a ping to the bot, to see if its online\n", false);

                builder.AddField("!help-baby", "Shows you what you can do with Baby suggestions", false);
                builder.AddField("!help-item", "Shows you what you can do with Item suggestions", false);
                builder.AddField("!help-item-active", "Shows you what you can do with Active Item suggestions", false);
                builder.AddField("!help-enemy", "Shows you what you can do with Enemy suggestions", false);
                builder.AddField("!help-translation", "Shows you what you can do with translations", false);
                builder.AddField("!help-wiki", "Shows you what you can do with the wiki integration", false);

                IUserMessage userMessage = await Context.Channel.SendMessageAsync("", false, builder.Build());
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Catched Error");
                Console.WriteLine(ex);
                Console.ResetColor();
                await Context.Channel.SendMessageAsync("```diff\n- The command resolved in an error!\n\n" + ex + "```");
            }
        }

        [Command("help-baby")]
        public async Task HandleHelpBabyCommand()
        {
            try
            {
                EmbedBuilder builder = new EmbedBuilder();
                builder.WithTitle("!idea-baby");
                builder.AddField("Adds a new Baby idea by using the following syntax", "Name= \nCost= \nHP= \nType= \nDamage= \nFirerate= \nRecharge= \nAbilities= ", false);
                await Context.Channel.SendMessageAsync("", false, builder.Build());

                builder = new EmbedBuilder();
                builder.WithTitle("!get-baby");
                builder.AddField("Gets a baby idea by entering the babies id or name", "!get-baby id/name", false);
                await Context.Channel.SendMessageAsync("", false, builder.Build());

                builder = new EmbedBuilder();
                builder.WithTitle("!get-existing-baby");
                builder.AddField("Gets an existing baby by entering the babies id or name", "!get-existing-baby id/name", false);
                await Context.Channel.SendMessageAsync("", false, builder.Build());

                builder = new EmbedBuilder();
                builder.WithTitle("!get-top-baby");
                builder.AddField("Gets the best rated baby ideas by entering the number of the babies", "!get-top-baby 10", false);
                await Context.Channel.SendMessageAsync("", false, builder.Build());
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Catched Error");
                Console.WriteLine(ex);
                Console.ResetColor();
                await Context.Channel.SendMessageAsync("```diff\n- The command resolved in an error!\n\n" + ex + "```");
            }
        }

        [Command("help-item")]
        public async Task HandleHelpItemCommand()
        {
            try
            {
                EmbedBuilder builder = new EmbedBuilder();
                builder.WithTitle("!idea-item");
                builder.AddField("Adds a new Item idea by using the following syntax", "Name= \nCost= \nTier= \nDescription= \nEffect= ", false);
                await Context.Channel.SendMessageAsync("", false, builder.Build());

                builder = new EmbedBuilder();
                builder.WithTitle("!get-item");
                builder.AddField("Gets an item idea by entering the items id or name", "!get-item id/name", false);
                await Context.Channel.SendMessageAsync("", false, builder.Build());

                builder = new EmbedBuilder();
                builder.WithTitle("!get-existing-item");
                builder.AddField("Gets an existing item by entering the items id or name", "!get-existing-item id/name", false);
                await Context.Channel.SendMessageAsync("", false, builder.Build());

                builder = new EmbedBuilder();
                builder.WithTitle("!get-top-item");
                builder.AddField("Gets the best rated item ideas by entering the number of the items", "!get-top-item 10", false);
                await Context.Channel.SendMessageAsync("", false, builder.Build());
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Catched Error");
                Console.WriteLine(ex);
                Console.ResetColor();
                await Context.Channel.SendMessageAsync("```diff\n- The command resolved in an error!\n\n" + ex + "```");
            }
        }

        [Command("help-item-active")]
        public async Task HandleHelpItemActiveCommand()
        {
            try
            {
                EmbedBuilder builder = new EmbedBuilder();
                builder.WithTitle("!idea-item-active");
                builder.AddField("Adds a new Active Item idea by using the following syntax", "Name= \nDescription= \nEffect= ", false);
                await Context.Channel.SendMessageAsync("", false, builder.Build());

                builder = new EmbedBuilder();
                builder.WithTitle("!get-item-active");
                builder.AddField("Gets an active item idea by entering the items id or name", "!get-item-active id/name", false);
                await Context.Channel.SendMessageAsync("", false, builder.Build());

                builder = new EmbedBuilder();
                builder.WithTitle("!get-existing-item-active");
                builder.AddField("Gets an existing active item by entering the items id or name", "!get-existing-item-active id/name", false);
                await Context.Channel.SendMessageAsync("", false, builder.Build());

                builder = new EmbedBuilder();
                builder.WithTitle("!get-top-item-active");
                builder.AddField("Gets the best rated active item ideas by entering the number of the active items", "!get-top-item-active 10", false);
                await Context.Channel.SendMessageAsync("", false, builder.Build());
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Catched Error");
                Console.WriteLine(ex);
                Console.ResetColor();
                await Context.Channel.SendMessageAsync("```diff\n- The command resolved in an error!\n\n" + ex + "```");
            }
        }

        [Command("help-enemy")]
        public async Task HandleHelpEnemyCommand()
        {
            try
            {
                EmbedBuilder builder = new EmbedBuilder();
                builder.WithTitle("!idea-enemy");
                builder.AddField("Adds a new Baby idea by using the following syntax", "Name= \nHP= \nType= \nDamage= \nFirerate= \nWalkspeed= \nAbilities= \nAppearance= ", false);
                await Context.Channel.SendMessageAsync("", false, builder.Build());

                builder = new EmbedBuilder();
                builder.WithTitle("!get-enemy");
                builder.AddField("Gets an enemy idea by entering the enemies id or name", "!get-enemy id/name", false);
                await Context.Channel.SendMessageAsync("", false, builder.Build());

                builder = new EmbedBuilder();
                builder.WithTitle("!get-existing-enemy");
                builder.AddField("Gets an existing enemy by entering the enemies id or name", "!get-existing-enemy id/name", false);
                await Context.Channel.SendMessageAsync("", false, builder.Build());

                builder = new EmbedBuilder();
                builder.WithTitle("!get-top-enemy");
                builder.AddField("Gets the best rated enemy ideas by entering the number of the enemies", "!get-top-enemy 10", false);
                await Context.Channel.SendMessageAsync("", false, builder.Build());
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Catched Error");
                Console.WriteLine(ex);
                Console.ResetColor();
                await Context.Channel.SendMessageAsync("```diff\n- The command resolved in an error!\n\n" + ex + "```");
            }
        }

        [Command("help-translation")]
        public async Task HandleHelpTranslationCommand()
        {
            try
            {
                EmbedBuilder builder = new EmbedBuilder();
                builder.WithTitle("!get-translation");
                builder.AddField("Gets the translation of language entered in shortform", "!get-translation en", false);
                await Context.Channel.SendMessageAsync("", false, builder.Build());
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Catched Error");
                Console.WriteLine(ex);
                Console.ResetColor();
                await Context.Channel.SendMessageAsync("```diff\n- The command resolved in an error!\n\n" + ex + "```");
            }
        }

        [Command("help-wiki")]
        public async Task HandleHelpWikiCommand()
        {
            try
            {
                EmbedBuilder builder = new EmbedBuilder();
                builder.WithTitle("!wiki");
                builder.AddField("Sends the link to the I.Rule wiki", "!wiki", false);
                await Context.Channel.SendMessageAsync("", false, builder.Build());

                builder = new EmbedBuilder();
                builder.WithTitle("!wiki-baby");
                builder.AddField("Sends the link to the I.Rule Babies wiki subpage", "!wiki-baby", false);
                await Context.Channel.SendMessageAsync("", false, builder.Build());

                builder = new EmbedBuilder();
                builder.WithTitle("!wiki-item");
                builder.AddField("Sends the link to the I.Rule Items wiki subpage", "!wiki-item", false);
                await Context.Channel.SendMessageAsync("", false, builder.Build());

                builder = new EmbedBuilder();
                builder.WithTitle("!wiki-enemy");
                builder.AddField("Sends the link to the I.Rule Enemies wiki subpage", "!wiki-enemy", false);
                await Context.Channel.SendMessageAsync("", false, builder.Build());

                builder = new EmbedBuilder();
                builder.WithTitle("!wiki-boss");
                builder.AddField("Sends the link to the I.Rule Bosses wiki subpage", "!wiki-boss", false);
                await Context.Channel.SendMessageAsync("", false, builder.Build());

                builder = new EmbedBuilder();
                builder.WithTitle("!wiki-modes");
                builder.AddField("Sends the link to the I.Rule Game Modes wiki subpage", "!wiki-modes", false);
                await Context.Channel.SendMessageAsync("", false, builder.Build());

                builder = new EmbedBuilder();
                builder.WithTitle("!wiki-floor");
                builder.AddField("Sends the link to the I.Rule Floors wiki subpage", "!wiki-floor", false);
                await Context.Channel.SendMessageAsync("", false, builder.Build());

                builder = new EmbedBuilder();
                builder.WithTitle("!wiki-obstacle");
                builder.AddField("Sends the link to the I.Rule Obstacles wiki subpage", "!wiki-obstacle", false);
                await Context.Channel.SendMessageAsync("", false, builder.Build());

                builder = new EmbedBuilder();
                builder.WithTitle("!wiki-achievement");
                builder.AddField("Sends the link to the I.Rule Achievements wiki subpage", "!wiki-achievement", false);
                await Context.Channel.SendMessageAsync("", false, builder.Build());

                builder = new EmbedBuilder();
                builder.WithTitle("!wiki-pill");
                builder.AddField("Sends the link to the I.Rule Pills wiki subpage", "!wiki-pill", false);
                await Context.Channel.SendMessageAsync("", false, builder.Build());

                builder = new EmbedBuilder();
                builder.WithTitle("!wiki-update");
                builder.AddField("Sends the link to the I.Rule Update history wiki subpage", "!wiki-update", false);
                await Context.Channel.SendMessageAsync("", false, builder.Build());

                builder = new EmbedBuilder();
                builder.WithTitle("!wiki-effect");
                builder.AddField("Sends the link to the I.Rule Effects wiki subpage", "!wiki-effect", false);
                await Context.Channel.SendMessageAsync("", false, builder.Build());
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Catched Error");
                Console.WriteLine(ex);
                Console.ResetColor();
                await Context.Channel.SendMessageAsync("```diff\n- The command resolved in an error!\n\n" + ex + "```");
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

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


        [Command("get-baby")]
        public async Task HandleGetBabyCommand([Remainder] string text)
        {
            IdeaHandler ideaHandler = new IdeaHandler(Context);
            await ideaHandler.GetIdea("Baby", text);
        }
        [Command("get-item")]
        public async Task HandleGetItemCommand([Remainder] string text)
        {
            IdeaHandler ideaHandler = new IdeaHandler(Context);
            await ideaHandler.GetIdea("Item", text);
        }
        [Command("get-item-active")]
        public async Task HandleGetItemActiveCommand([Remainder] string text)
        {
            IdeaHandler ideaHandler = new IdeaHandler(Context);
            await ideaHandler.GetIdea("ItemActive", text);
        }
        [Command("get-enemy")]
        public async Task HandleGetEnemyCommand([Remainder] string text)
        {
            IdeaHandler ideaHandler = new IdeaHandler(Context);
            await ideaHandler.GetIdea("Enemy", text);
        }

        [Command("get-existing-baby")]
        public async Task HandleGetExistingBabyCommand([Remainder] string text)
        {
            IdeaHandler ideaHandler = new IdeaHandler(Context);
            await ideaHandler.GetExistingIdea("Baby", text);
        }
        [Command("get-existing-item")]
        public async Task HandleGetExistingItemCommand([Remainder] string text)
        {
            IdeaHandler ideaHandler = new IdeaHandler(Context);
            await ideaHandler.GetExistingIdea("Item", text);
        }
        [Command("get-existing-item-active")]
        public async Task HandleGetExistingItemActiveCommand([Remainder] string text)
        {
            IdeaHandler ideaHandler = new IdeaHandler(Context);
            await ideaHandler.GetExistingIdea("ItemActive", text);
        }
        [Command("get-existing-enemy")]
        public async Task HandleGetExistingEnemyCommand([Remainder] string text)
        {
            IdeaHandler ideaHandler = new IdeaHandler(Context);
            await ideaHandler.GetExistingIdea("Enemy", text);
        }


        [Command("get-top-baby")]
        public async Task HandleGetTopBabyCommand([Remainder] string text)
        {
            IdeaHandler ideaHandler = new IdeaHandler(Context);
            await ideaHandler.GetBestRatedIdea("Baby", text);
        }
        [Command("get-top-item")]
        public async Task HandleGetTopItemCommand([Remainder] string text)
        {
            IdeaHandler ideaHandler = new IdeaHandler(Context);
            await ideaHandler.GetBestRatedIdea("Item", text);
        }
        [Command("get-top-item-active")]
        public async Task HandleGetTopItemActiveCommand([Remainder] string text)
        {
            IdeaHandler ideaHandler = new IdeaHandler(Context);
            await ideaHandler.GetBestRatedIdea("ItemActive", text);
        }
        [Command("get-top-enemy")]
        public async Task HandleGetTopEnemyCommand([Remainder] string text)
        {
            IdeaHandler ideaHandler = new IdeaHandler(Context);
            await ideaHandler.GetBestRatedIdea("Enemy", text);
        }

        [Command("wiki")]
        public async Task HandleWikiCommand()
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.AddField("I.Rule Homepage: ", "[Wiki-Home](https://i-rule.fandom.com/wiki/I_RULE_Wiki)", true);
            await Context.Channel.SendMessageAsync("", false, builder.Build());
            //await Context.Channel.SendMessageAsync("```diff\n[Wiki-Home](https://i-rule.fandom.com/wiki/I_RULE_Wiki)```");
        }
        [Command("wiki-baby")]
        public async Task HandleWikiBabyCommand()
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.AddField("I.Rule Babies page: ", "[Wiki-Babies](https://i-rule.fandom.com/wiki/Babies)", true);
            await Context.Channel.SendMessageAsync("", false, builder.Build());
        }
        [Command("wiki-item")]
        public async Task HandleWikiItemCommand()
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.AddField("I.Rule Items page: ", "[Wiki-Items](https://i-rule.fandom.com/wiki/Items)", true);
            await Context.Channel.SendMessageAsync("", false, builder.Build());
        }
        [Command("wiki-enemy")]
        public async Task HandleWikiEnemyCommand()
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.AddField("I.Rule Enemies page: ", "[Wiki-Enemies](https://i-rule.fandom.com/wiki/Enemies)", true);
            await Context.Channel.SendMessageAsync("", false, builder.Build());
        }
        [Command("wiki-boss")]
        public async Task HandleWikiBossCommand()
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.AddField("I.Rule Bosses page: ", "[Wiki-Bosses](https://i-rule.fandom.com/wiki/Bosses)", true);
            await Context.Channel.SendMessageAsync("", false, builder.Build());
        }
        [Command("wiki-mode")]
        public async Task HandleWikiModeCommand()
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.AddField("I.Rule Game Modes page: ", "[Wiki-GameModes](https://i-rule.fandom.com/wiki/Game_Modes)", true);
            await Context.Channel.SendMessageAsync("", false, builder.Build());
        }
        [Command("wiki-floor")]
        public async Task HandleWikiFloorCommand()
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.AddField("I.Rule Floors page: ", "[Wiki-Floors](https://i-rule.fandom.com/wiki/Floors)", true);
            await Context.Channel.SendMessageAsync("", false, builder.Build());
        }
        [Command("wiki-obstacle")]
        public async Task HandleWikiObstacleCommand()
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.AddField("I.Rule Obstacles page: ", "[Wiki-Obstacles](https://i-rule.fandom.com/wiki/Obstacles)", true);
            await Context.Channel.SendMessageAsync("", false, builder.Build());
        }
        [Command("wiki-achievement")]
        public async Task HandleWikiAchievementCommand()
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.AddField("I.Rule Achievements page: ", "[Wiki-Achievements](https://i-rule.fandom.com/wiki/Achievements)", true);
            await Context.Channel.SendMessageAsync("", false, builder.Build());
        }
        [Command("wiki-pill")]
        public async Task HandleWikiPillCommand()
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.AddField("I.Rule Pills page: ", "[Wiki-Pills](https://i-rule.fandom.com/wiki/Pills)", true);
            await Context.Channel.SendMessageAsync("", false, builder.Build());
        }
        [Command("wiki-update")]
        public async Task HandleWikiUpdateCommand()
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.AddField("I.Rule Update history page: ", "[Wiki-UpdateHistory](https://i-rule.fandom.com/wiki/Update_history)", true);
            await Context.Channel.SendMessageAsync("", false, builder.Build());
        }
        [Command("wiki-effect")]
        public async Task HandleWikiEffectCommand()
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.AddField("I.Rule Effects page: ", "[Wiki-Effects](https://i-rule.fandom.com/wiki/Effects)", true);
            await Context.Channel.SendMessageAsync("", false, builder.Build());
        }

        [Command("get-translation")]
        public async Task HandleGetTranslationCommand([Remainder] string text)
        {
            TranslationHandler translationHandler = new TranslationHandler(Context);
            await translationHandler.GetTranslation(text);
        }
    }
}
