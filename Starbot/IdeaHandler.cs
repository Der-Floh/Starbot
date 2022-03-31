using Discord;
using Discord.Commands;
using Starbot;
using Starbot.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starbot
{
    public class IdeaHandler
    {
        private SocketCommandContext Context;
        public IdeaHandler(SocketCommandContext _context)
        {
            Context = _context;
        }
        public async Task NewIdea(string text, string type)
        {
            try
            {
                switch (type)
                {
                    case "Baby": await IdeaBaby(text); break;
                    case "Item": await IdeaItem(text); break;
                    case "ItemActive": await IdeaItemActive(text); break;
                    case "Enemy": await IdeaEnemy(text); break;
                }
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

        private async Task IdeaBaby(string text)
        {
            string[] statNames = { "Name", "Cost", "HP", "Type", "Damage", "Firerate", "Recharge", "Abilities" };
            string[] stats = await GetIdeaStats(statNames, text);
            /*
            string statMsg = "";
            for (int i = 0; i < stats.Length; i++)
            {
                statMsg += statNames[i] + ": " + stats[i] + "\n";
            }
            */
            JsonHandler jsonHandler = new JsonHandler();

            int parsedInt = 0;
            float parsedFloat = 0;
            Baby baby = new Baby();
            baby.id = Context.Message.Id;
            baby.name = stats[0];
            int.TryParse(stats[1], out parsedInt);
            baby.cost = parsedInt;
            int.TryParse(stats[2], out parsedInt);
            baby.hp = parsedInt;
            baby.type = stats[3];
            int.TryParse(stats[4], out parsedInt);
            baby.damage = parsedInt;
            float.TryParse(stats[5], out parsedFloat);
            baby.firerate = parsedFloat;
            float.TryParse(stats[6], out parsedFloat);
            baby.recharge = parsedFloat;
            baby.abilities = stats[7];

            baby.creator = Context.Message.Author.Username;
            baby.verified = false;
            baby.rating = 0;
            baby.date = DateTime.Now.Date;

            //await Context.Message.ReplyAsync("Not fully implemented yet");

            await Context.Message.DeleteAsync();
            IUserMessage botMessage = await SendEmbedIdea(statNames, stats, "Baby");

            baby.id = botMessage.Id;
            await jsonHandler.AddBaby(baby);

            await botMessage.AddReactionAsync(new Emoji("👍"));
            await botMessage.AddReactionAsync(new Emoji("👎"));
            await botMessage.AddReactionAsync(new Emoji("💾"));
        }
        private async Task IdeaItem(string text)
        {
            string[] statNames = { "Name", "Cost", "Tier", "Description", "Effect" };
            string[] stats = await GetIdeaStats(statNames, text);

            JsonHandler jsonHandler = new JsonHandler();

            int parsedInt = 0;
            Item item = new Item();
            item.id = Context.Message.Id;
            item.name = stats[0];
            int.TryParse(stats[1], out parsedInt);
            item.cost = parsedInt;
            int.TryParse(stats[2], out parsedInt);
            item.tier = parsedInt;
            item.description = stats[3];
            item.effect = stats[4];

            item.creator = Context.Message.Author.Username;
            item.verified = false;
            item.rating = 0;
            item.date = DateTime.Now.Date;

            await Context.Message.DeleteAsync();
            IUserMessage botMessage = await SendEmbedIdea(statNames, stats, "Item");

            item.id = botMessage.Id;
            await jsonHandler.AddItem(item);

            await botMessage.AddReactionAsync(new Emoji("👍"));
            await botMessage.AddReactionAsync(new Emoji("👎"));
            await botMessage.AddReactionAsync(new Emoji("💾"));
        }
        private async Task IdeaItemActive(string text)
        {
            string[] statNames = { "Name", "Description", "Effect" };
            string[] stats = await GetIdeaStats(statNames, text);

            JsonHandler jsonHandler = new JsonHandler();

            ItemActive itemActive = new ItemActive();
            itemActive.id = Context.Message.Id;
            itemActive.name = stats[0];
            itemActive.description = stats[3];
            itemActive.effect = stats[4];

            itemActive.creator = Context.Message.Author.Username;
            itemActive.verified = false;
            itemActive.rating = 0;
            itemActive.date = DateTime.Now.Date;

            await Context.Message.DeleteAsync();
            IUserMessage botMessage = await SendEmbedIdea(statNames, stats, "ItemActive");

            itemActive.id = botMessage.Id;
            await jsonHandler.AddItemActive(itemActive);

            await botMessage.AddReactionAsync(new Emoji("👍"));
            await botMessage.AddReactionAsync(new Emoji("👎"));
            await botMessage.AddReactionAsync(new Emoji("💾"));
        }
        private async Task IdeaEnemy(string text)
        {
            string[] statNames = { "Name", "HP", "Type", "Damage", "Firerate", "Walkspeed", "Abilities", "Appearance"};
            string[] stats = await GetIdeaStats(statNames, text);

            JsonHandler jsonHandler = new JsonHandler();

            int parsedInt = 0;
            float parsedFloat = 0;
            Enemy enemy = new Enemy();
            enemy.id = Context.Message.Id;
            enemy.name = stats[0];
            int.TryParse(stats[1], out parsedInt);
            enemy.hp = parsedInt;
            enemy.type = stats[2];
            int.TryParse(stats[3], out parsedInt);
            enemy.damage = parsedInt;
            float.TryParse(stats[4], out parsedFloat);
            enemy.firerate = parsedFloat;
            float.TryParse(stats[5], out parsedFloat);
            enemy.walkspeed = parsedFloat;
            enemy.abilities = stats[6];
            enemy.appearance = stats[7];

            enemy.creator = Context.Message.Author.Username;
            enemy.verified = false;
            enemy.rating = 0;
            enemy.date = DateTime.Now.Date;

            await Context.Message.DeleteAsync();
            IUserMessage botMessage = await SendEmbedIdea(statNames, stats, "Enemy");

            enemy.id = botMessage.Id;
            await jsonHandler.AddEnemy(enemy);

            await botMessage.AddReactionAsync(new Emoji("👍"));
            await botMessage.AddReactionAsync(new Emoji("👎"));
            await botMessage.AddReactionAsync(new Emoji("💾"));
        }

        private async Task<string[]> GetIdeaStats(string[] statNames, string text)
        {
            int first = 0;
            int last = 0;
            string[] stats = new string[statNames.Length];

            for (int i = 0; i < statNames.Length - 1; i++)
            {
                if (text.Contains(statNames[i]))
                {
                    first = text.IndexOf(statNames[i] + "=") + (statNames[i] + "=").Length;
                    //last = text.LastIndexOf(statNames[i + 1]);
                    for (int j = i + 1; j < statNames.Length; j++)
                    {
                        last = text.Length;
                        if (text.Contains(statNames[j] + "="))
                        {
                            last = text.LastIndexOf(statNames[j] + "=");
                            break;
                        }
                    }
                    stats[i] = text.Substring(first, last - first);
                    stats[i] = stats[i].Replace("\n", "");
                }
                else
                {
                    stats[i] = "-";
                }
            }
            if (text.Contains(statNames[statNames.Length - 1]))
            {
                first = text.IndexOf(statNames[statNames.Length - 1] + "=") + (statNames[statNames.Length - 1] + "=").Length;
                last = text.Length;
                stats[statNames.Length - 1] = text.Substring(first, last - first);
                stats[statNames.Length - 1] = stats[statNames.Length - 1].Replace("\n", "");
            }

            return stats;
        }

        private async Task<IUserMessage> SendEmbedIdea(string[] statNames, string[] stats, string type)
        {
            EmbedBuilder builder = new EmbedBuilder();

            builder.WithTitle(type + " - " + stats[0]);
            for (int i = 0; i < statNames.Length; i++)
            {
                builder.AddField(statNames[i] + ": ", stats[i], true);
            }
            //builder.WithThumbnailUrl("http://...");
            switch (type)
            {
                case "Baby": builder.WithColor(Color.Gold); break;
                case "Item": builder.WithColor(Color.Blue); break;
                case "ItemActive": builder.WithColor(Color.Green); break;
                case "Enemy": builder.WithColor(Color.Red); break;
            }
            
            IUserMessage userMessage = await Context.Channel.SendMessageAsync("", false, builder.Build());
            await userMessage.ModifyAsync(x =>
            {
                //builder.WithTitle(type + " - " + stats[0] + " - " + userMessage.Id);
                builder.AddField("ID: ", userMessage.Id, true);
                x.Embed = builder.Build();
            });
            return userMessage;
        }
    }
}
