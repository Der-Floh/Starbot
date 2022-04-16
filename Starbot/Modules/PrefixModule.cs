using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Timers;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Starbot.Types;

namespace Starbot.Modules
{
    public class PrefixModule : ModuleBase<SocketCommandContext>
    {
        private static System.Timers.Timer CommandDelay;
        private static bool commandDelayed = true;
        public PrefixModule()
        {
            CommandDelay = new System.Timers.Timer(1000);
            CommandDelay.Elapsed += TimerTick;
            CommandDelay.AutoReset = false;
        }
        private static async void TimerTick(object source, ElapsedEventArgs e)
        {
            Console.WriteLine("Command Delay");
            if (!commandDelayed)
            {
                commandDelayed = true;
            }
        }
        private async Task StartTopIdeaDelay(string delayString)
        {
            int delay = -1;
            int.TryParse(delayString, out delay);
            if (delay != -1)
            {
                commandDelayed = false;
                CommandDelay.Interval = (delay + 10) * 1000;
                CommandDelay.Start();
            }
        }

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
                builder.AddField("!itch-irule", "Sends a link to the Itch.io homepage of I.Rule\n", false);

                builder.AddField("!help-baby", "Shows you what you can do with Baby suggestions", false);
                builder.AddField("!help-item", "Shows you what you can do with Item suggestions", false);
                builder.AddField("!help-item-active", "Shows you what you can do with Active Item suggestions", false);
                builder.AddField("!help-enemy", "Shows you what you can do with Enemy suggestions", false);
                builder.AddField("!help-translation", "Shows you what you can do with translations", false);
                builder.AddField("!help-wiki", "Shows you what you can do with the wiki integration", false);
                builder.AddField("!help-github", "Shows you which github links are available", false);

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
                builder.AddField("Gets the best rated baby ideas by entering the number of best babies", "!get-top-baby 10", false);
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
                builder.AddField("Gets the best rated item ideas by entering the number of best items", "!get-top-item 10", false);
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
                builder.AddField("Gets the best rated active item ideas by entering the number of best active items", "!get-top-item-active 10", false);
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
                builder.AddField("Gets the best rated enemy ideas by entering the number of best enemies", "!get-top-enemy 10", false);
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

                builder = new EmbedBuilder();
                builder.WithTitle("!get-translators");
                builder.AddField("Gets all translators of I.Rule", "!get-translators", false);
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
                builder.AddField("!wiki", "Sends the link to the I.Rule wiki", true);
                builder.AddField("!wiki-baby", "Sends the link to the I.Rule Babies wiki subpage", true);
                builder.AddField("!wiki-item", "Sends the link to the I.Rule Items wiki subpage", true);
                builder.AddField("!wiki-enemy", "Sends the link to the I.Rule Enemies wiki subpage", true);
                builder.AddField("!wiki-bosses", "Sends the link to the I.Rule Bosses wiki subpage", true);
                builder.AddField("!wiki-modes", "Sends the link to the I.Rule Game Modes wiki subpage", true);
                builder.AddField("!wiki-floors", "Sends the link to the I.Rule Floors wiki subpage", true);
                builder.AddField("!wiki-obstacles", "Sends the link to the I.Rule Obstacles wiki subpage", true);
                builder.AddField("!wiki-achievements", "Sends the link to the I.Rule Achievements wiki subpage", true);
                builder.AddField("!wiki-pills", "Sends the link to the I.Rule Pills wiki subpage", true);
                builder.AddField("!wiki-updates", "Sends the link to the I.Rule Update history wiki subpage", true);
                builder.AddField("!wiki-effects", "Sends the link to the I.Rule Effects wiki subpage", true);
                await Context.Channel.SendMessageAsync("", false, builder.Build());
                /*
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
                builder.WithTitle("!wiki-bosses");
                builder.AddField("Sends the link to the I.Rule Bosses wiki subpage", "!wiki-bosses", false);
                await Context.Channel.SendMessageAsync("", false, builder.Build());

                builder = new EmbedBuilder();
                builder.WithTitle("!wiki-modes");
                builder.AddField("Sends the link to the I.Rule Game Modes wiki subpage", "!wiki-modes", false);
                await Context.Channel.SendMessageAsync("", false, builder.Build());

                builder = new EmbedBuilder();
                builder.WithTitle("!wiki-floors");
                builder.AddField("Sends the link to the I.Rule Floors wiki subpage", "!wiki-floors", false);
                await Context.Channel.SendMessageAsync("", false, builder.Build());

                builder = new EmbedBuilder();
                builder.WithTitle("!wiki-obstacles");
                builder.AddField("Sends the link to the I.Rule Obstacles wiki subpage", "!wiki-obstacles", false);
                await Context.Channel.SendMessageAsync("", false, builder.Build());

                builder = new EmbedBuilder();
                builder.WithTitle("!wiki-achievements");
                builder.AddField("Sends the link to the I.Rule Achievements wiki subpage", "!wiki-achievements", false);
                await Context.Channel.SendMessageAsync("", false, builder.Build());

                builder = new EmbedBuilder();
                builder.WithTitle("!wiki-pills");
                builder.AddField("Sends the link to the I.Rule Pills wiki subpage", "!wiki-pills", false);
                await Context.Channel.SendMessageAsync("", false, builder.Build());

                builder = new EmbedBuilder();
                builder.WithTitle("!wiki-updates");
                builder.AddField("Sends the link to the I.Rule Update history wiki subpage", "!wiki-updates", false);
                await Context.Channel.SendMessageAsync("", false, builder.Build());

                builder = new EmbedBuilder();
                builder.WithTitle("!wiki-effects");
                builder.AddField("Sends the link to the I.Rule Effects wiki subpage", "!wiki-effects", false);
                await Context.Channel.SendMessageAsync("", false, builder.Build());
                */
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

        [Command("help-github")]
        public async Task HandleHelpGithubCommand()
        {
            try
            {
                EmbedBuilder builder = new EmbedBuilder();
                builder.WithTitle("!github-releases");
                builder.AddField("Sends a link to the Github Releases page of I.Rule", "!github-releases", false);
                await Context.Channel.SendMessageAsync("", false, builder.Build());

                builder = new EmbedBuilder();
                builder.WithTitle("!github-ruler");
                builder.AddField("Sends a link to the Github Ruler page of I.Rule", "!github-releases", false);
                await Context.Channel.SendMessageAsync("", false, builder.Build());

                builder = new EmbedBuilder();
                builder.WithTitle("!github-translations");
                builder.AddField("Sends a link to the Github Translations page of I.Rule", "!github-releases", false);
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
            if (!commandDelayed)
            {
                Console.WriteLine("Command Delay still active");
                return;
            }
            IdeaHandler ideaHandler = new IdeaHandler(Context);
            await StartTopIdeaDelay(text);
            await ideaHandler.GetBestRatedIdea("Baby", text);
        }
        [Command("get-top-baby")]
        public async Task HandleGetTopBabyOneCommand()
        {
            Console.WriteLine("test");
            IdeaHandler ideaHandler = new IdeaHandler(Context);
            await ideaHandler.GetBestRatedIdea("Baby", "1");
        }
        [Command("get-top-item")]
        public async Task HandleGetTopItemCommand([Remainder] string text)
        {
            if (!commandDelayed)
            {
                Console.WriteLine("Command Delay still active");
                return;
            }
            IdeaHandler ideaHandler = new IdeaHandler(Context);
            await StartTopIdeaDelay(text);
            await ideaHandler.GetBestRatedIdea("Item", text);
        }
        [Command("get-top-item")]
        public async Task HandleGetTopItemOneCommand()
        {
            IdeaHandler ideaHandler = new IdeaHandler(Context);
            await ideaHandler.GetBestRatedIdea("Item", "1");
        }
        [Command("get-top-item-active")]
        public async Task HandleGetTopItemActiveCommand([Remainder] string text)
        {
            if (!commandDelayed)
            {
                Console.WriteLine("Command Delay still active");
                return;
            }
            IdeaHandler ideaHandler = new IdeaHandler(Context);
            await StartTopIdeaDelay(text);
            await ideaHandler.GetBestRatedIdea("ItemActive", text);
        }
        [Command("get-top-item-active")]
        public async Task HandleGetTopItemActiveOneCommand()
        {
            IdeaHandler ideaHandler = new IdeaHandler(Context);
            await ideaHandler.GetBestRatedIdea("ItemActive", "1");
        }
        [Command("get-top-enemy")]
        public async Task HandleGetTopEnemyCommand([Remainder] string text)
        {
            if (!commandDelayed)
            {
                Console.WriteLine("Command Delay still active");
                return;
            }
            IdeaHandler ideaHandler = new IdeaHandler(Context);
            await StartTopIdeaDelay(text);
            await ideaHandler.GetBestRatedIdea("Enemy", text);
        }
        [Command("get-top-enemy")]
        public async Task HandleGetTopEnemyOneCommand()
        {
            IdeaHandler ideaHandler = new IdeaHandler(Context);
            await ideaHandler.GetBestRatedIdea("Enemy", "1");
        }

        [Command("wiki")]
        public async Task HandleWikiCommand()
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.AddField("I.Rule Homepage: ", "[Wiki-Home](https://i-rule.fandom.com/wiki/I_RULE_Wiki)", true);
            await Context.Channel.SendMessageAsync("", false, builder.Build());
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
        [Command("wiki-bosses")]
        public async Task HandleWikiBossCommand()
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.AddField("I.Rule Bosses page: ", "[Wiki-Bosses](https://i-rule.fandom.com/wiki/Bosses)", true);
            await Context.Channel.SendMessageAsync("", false, builder.Build());
        }
        [Command("wiki-modes")]
        public async Task HandleWikiModeCommand()
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.AddField("I.Rule Game Modes page: ", "[Wiki-GameModes](https://i-rule.fandom.com/wiki/Game_Modes)", true);
            await Context.Channel.SendMessageAsync("", false, builder.Build());
        }
        [Command("wiki-floors")]
        public async Task HandleWikiFloorCommand()
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.AddField("I.Rule Floors page: ", "[Wiki-Floors](https://i-rule.fandom.com/wiki/Floors)", true);
            await Context.Channel.SendMessageAsync("", false, builder.Build());
        }
        [Command("wiki-obstacles")]
        public async Task HandleWikiObstacleCommand()
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.AddField("I.Rule Obstacles page: ", "[Wiki-Obstacles](https://i-rule.fandom.com/wiki/Obstacles)", true);
            await Context.Channel.SendMessageAsync("", false, builder.Build());
        }
        [Command("wiki-achievements")]
        public async Task HandleWikiAchievementCommand()
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.AddField("I.Rule Achievements page: ", "[Wiki-Achievements](https://i-rule.fandom.com/wiki/Achievements)", true);
            await Context.Channel.SendMessageAsync("", false, builder.Build());
        }
        [Command("wiki-pills")]
        public async Task HandleWikiPillCommand()
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.AddField("I.Rule Pills page: ", "[Wiki-Pills](https://i-rule.fandom.com/wiki/Pills)", true);
            await Context.Channel.SendMessageAsync("", false, builder.Build());
        }
        [Command("wiki-updates")]
        public async Task HandleWikiUpdateCommand()
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.AddField("I.Rule Update history page: ", "[Wiki-UpdateHistory](https://i-rule.fandom.com/wiki/Update_history)", true);
            await Context.Channel.SendMessageAsync("", false, builder.Build());
        }
        [Command("wiki-effects")]
        public async Task HandleWikiEffectCommand()
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.AddField("I.Rule Effects page: ", "[Wiki-Effects](https://i-rule.fandom.com/wiki/Effects)", true);
            await Context.Channel.SendMessageAsync("", false, builder.Build());
        }

        [Command("github-releases")]
        public async Task HandleGithubIRuleCommand()
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.AddField("I.Rule Releases Github page: ", "[Github-Releases](https://github.com/Steviegt6/i-rule-storage)", true);
            await Context.Channel.SendMessageAsync("", false, builder.Build());
        }
        [Command("github-ruler")]
        public async Task HandleGithubRulerCommand()
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.AddField("I.Rule Ruler Github page: ", "[Github-Ruler](https://github.com/Steviegt6/ruler)", true);
            await Context.Channel.SendMessageAsync("", false, builder.Build());
        }
        [Command("github-translation")]
        public async Task HandleGithubTranslationCommand()
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.AddField("I.Rule Translations Github page: ", "[Github-Translations](https://github.com/Steviegt6/i-rule-translations)", true);
            await Context.Channel.SendMessageAsync("", false, builder.Build());
        }

        [Command("itch-irule")]
        public async Task HandleItchIRuleCommand()
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.AddField("I.Rule Itch.io page: ", "[I-Rule](https://doctorhummer.itch.io/irule)", true);
            await Context.Channel.SendMessageAsync("", false, builder.Build());
        }

        [Command("get-translation")]
        public async Task HandleGetTranslationCommand([Remainder] string text)
        {
            TranslationHandler translationHandler = new TranslationHandler(Context);
            await translationHandler.GetTranslation(text);
        }
        [Command("get-translators")]
        public async Task HandleGetTranslatorsCommand()
        {
            TranslationHandler translationHandler = new TranslationHandler(Context);
            await translationHandler.GetTranslators();
        }

        [Command("killbot")]
        public async Task HandleKillBotCommand()
        {
            var user = Context.User as SocketGuildUser;
            if (user == null) return;

            var role = user.Roles.SingleOrDefault(x => x.Id == 807325063757037589); //admin role id = 329532167791312896
            if (role == null) return;

            Console.WriteLine("Killed by Admin");
            Environment.Exit(0);
        }
    }
}
