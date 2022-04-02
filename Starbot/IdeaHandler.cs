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

        public async Task GetIdea(string type, string text)
        {
            try
            {
                ulong id = 0;
                if (ulong.TryParse(text, out id))
                {
                    switch (type)
                    {
                        case "Baby": await GetIdeaByIdBaby(id); break;
                        case "Item": await GetIdeaByIdItem(id); break;
                        case "ItemActive": await GetIdeaByIdItemActive(id); break;
                        case "Enemy": await GetIdeaByIdEnemy(id); break;
                    }
                }
                else
                {
                    switch (type)
                    {
                        case "Baby": await GetIdeaByNameBaby(text); break;
                        case "Item": await GetIdeaByNameItem(text); break;
                        case "ItemActive": await GetIdeaByNameItemActive(text); break;
                        case "Enemy": await GetIdeaByNameEnemy(text); break;
                    }
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

        public async Task GetBestRatedIdea(string type, string text)
        {
            try
            {
                int rateNo = int.Parse(text);
                switch (type)
                {
                    case "Baby": break;
                    case "Item": break;
                    case "ItemActive": break;
                    case "Enemy": break;
                    case "All": break;
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
            baby.date = DateTime.Now;

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
            item.date = DateTime.Now;

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
            itemActive.description = stats[1];
            itemActive.effect = stats[2];

            itemActive.creator = Context.Message.Author.Username;
            itemActive.verified = false;
            itemActive.rating = 0;
            itemActive.date = DateTime.Now;

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
            enemy.date = DateTime.Now;

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

            for (int i = 0; i <= statNames.Length - 1; i++)
            {
                if (text.Contains(statNames[i]))
                {
                    first = text.IndexOf(statNames[i] + "=") + (statNames[i] + "=").Length;
                    //last = text.LastIndexOf(statNames[i + 1]);
                    last = text.Length;
                    for (int j = i + 1; j < statNames.Length; j++)
                    {
                        //last = text.Length;
                        if (text.Contains(statNames[j] + "="))
                        {
                            last = text.LastIndexOf(statNames[j] + "=");
                            break;
                        }
                    }
                    stats[i] = text.Substring(first, last - first);
                    stats[i] = stats[i].Replace("\n", "");
                    if (stats[i][0] == ' ')
                    {
                        stats[i] = stats[i].Remove(0, 1);
                        Console.WriteLine("Removed space=" + stats[i]);
                    }
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
                builder.WithFooter("ID: " + userMessage.Id.ToString());
                //builder.AddField("ID: ", userMessage.Id, true);
                x.Embed = builder.Build();
            });
            return userMessage;
        }

        private async Task GetIdeaByIdBaby(ulong id)
        {
            JsonHandler jsonHandler = new JsonHandler();
            Baby baby = await jsonHandler.GetIdeaByIdBaby(id);

            if (baby == null)
            {
                await Context.Channel.SendMessageAsync("```diff\n- There is no Baby idea with the id: " + id + "```");
                return;
            }

            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle(" Baby - " + baby.name);
            builder.WithFooter("ID: " + baby.id.ToString());
            builder.WithColor(Color.Gold);

            builder.AddField("Name: ", baby.name, true);
            builder.AddField("Cost: ", baby.cost, true);
            builder.AddField("HP: ", baby.hp, true);
            builder.AddField("Type: ", baby.type, true);
            builder.AddField("Damage: ", baby.damage, true);
            builder.AddField("Firerate: ", baby.firerate, true);
            builder.AddField("Recharge: ", baby.recharge, true);
            builder.AddField("Abilities: ", baby.abilities, true);

            builder.AddField("Creator: ", baby.creator, true);
            builder.AddField("Verified: ", baby.verified, true);
            builder.AddField("Rating: ", baby.rating, true);
            builder.AddField("Date: ", baby.date.ToString("MM/dd/yyyy"), true);

            await Context.Channel.SendMessageAsync("", false, builder.Build());
        }

        private async Task GetIdeaByIdItem(ulong id)
        {
            JsonHandler jsonHandler = new JsonHandler();
            Item item = await jsonHandler.GetIdeaByIdItem(id);

            if (item == null)
            {
                await Context.Channel.SendMessageAsync("```diff\n- There is no Item idea with the id: " + id + "```");
                return;
            }

            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle(" Item - " + item.name);
            builder.WithFooter("ID: " + item.id.ToString());
            builder.WithColor(Color.Blue);

            builder.AddField("Name: ", item.name, true);
            builder.AddField("Cost: ", item.cost, true);
            builder.AddField("Tier: ", item.tier, true);
            builder.AddField("Description: ", item.description, true);
            builder.AddField("Effect: ", item.effect, true);

            builder.AddField("Creator: ", item.creator, true);
            builder.AddField("Verified: ", item.verified, true);
            builder.AddField("Rating: ", item.rating, true);
            builder.AddField("Date: ", item.date.ToString("MM/dd/yyyy"), true);

            await Context.Channel.SendMessageAsync("", false, builder.Build());
        }

        private async Task GetIdeaByIdItemActive(ulong id)
        {
            JsonHandler jsonHandler = new JsonHandler();
            ItemActive itemActive = await jsonHandler.GetIdeaByIdItemActive(id);

            if (itemActive == null)
            {
                await Context.Channel.SendMessageAsync("```diff\n- There is no Active Item idea with the id: " + id + "```");
                return;
            }

            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle(" Item - " + itemActive.name);
            builder.WithFooter("ID: " + itemActive.id.ToString());
            builder.WithColor(Color.Green);

            builder.AddField("Name: ", itemActive.name, true);
            builder.AddField("Description: ", itemActive.description, true);
            builder.AddField("Effect: ", itemActive.effect, true);

            builder.AddField("Creator: ", itemActive.creator, true);
            builder.AddField("Verified: ", itemActive.verified, true);
            builder.AddField("Rating: ", itemActive.rating, true);
            builder.AddField("Date: ", itemActive.date.ToString("MM/dd/yyyy"), true);

            await Context.Channel.SendMessageAsync("", false, builder.Build());
        }

        private async Task GetIdeaByIdEnemy(ulong id)
        {
            JsonHandler jsonHandler = new JsonHandler();
            Enemy enemy = await jsonHandler.GetIdeaByIdEnemy(id);

            if (enemy == null)
            {
                await Context.Channel.SendMessageAsync("```diff\n- There is no Enemy idea with the id: " + id + "```");
                return;
            }

            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle(" Baby - " + enemy.name);
            builder.WithFooter("ID: " + enemy.id.ToString());
            builder.WithColor(Color.Red);

            builder.AddField("Name: ", enemy.name, true);
            builder.AddField("HP: ", enemy.hp, true);
            builder.AddField("Type: ", enemy.type, true);
            builder.AddField("Damage: ", enemy.damage, true);
            builder.AddField("Firerate: ", enemy.firerate, true);
            builder.AddField("Walkspeed: ", enemy.walkspeed, true);
            builder.AddField("Abilities: ", enemy.abilities, true);
            builder.AddField("Appearance: ", enemy.appearance, true);

            builder.AddField("Creator: ", enemy.creator, true);
            builder.AddField("Verified: ", enemy.verified, true);
            builder.AddField("Rating: ", enemy.rating, true);
            builder.AddField("Date: ", enemy.date.ToString("MM/dd/yyyy"), true);

            await Context.Channel.SendMessageAsync("", false, builder.Build());
        }

        private async Task GetIdeaByNameBaby(string name)
        {
            JsonHandler jsonHandler = new JsonHandler();
            Baby baby = await jsonHandler.GetIdeaByNameBaby(name);

            if (baby == null)
            {
                await Context.Channel.SendMessageAsync("```diff\n- There is no Baby idea with the name: " + name + "```");
                return;
            }

            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle(" Baby - " + baby.name);
            builder.WithFooter("ID: " + baby.id.ToString());
            builder.WithColor(Color.Gold);

            builder.AddField("Name: ", baby.name, true);
            builder.AddField("Cost: ", baby.cost, true);
            builder.AddField("HP: ", baby.hp, true);
            builder.AddField("Type: ", baby.type, true);
            builder.AddField("Damage: ", baby.damage, true);
            builder.AddField("Firerate: ", baby.firerate, true);
            builder.AddField("Recharge: ", baby.recharge, true);
            builder.AddField("Abilities: ", baby.abilities, true);

            builder.AddField("Creator: ", baby.creator, true);
            builder.AddField("Verified: ", baby.verified, true);
            builder.AddField("Rating: ", baby.rating, true);
            builder.AddField("Date: ", baby.date.ToString("MM/dd/yyyy"), true);

            await Context.Channel.SendMessageAsync("", false, builder.Build());
        }

        private async Task GetIdeaByNameItem(string name)
        {
            JsonHandler jsonHandler = new JsonHandler();
            Item item = await jsonHandler.GetIdeaByNameItem(name);

            if (item == null)
            {
                await Context.Channel.SendMessageAsync("```diff\n- There is no Item idea with the name: " + name + "```");
                return;
            }

            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle(" Item - " + item.name);
            builder.WithFooter("ID: " + item.id.ToString());
            builder.WithColor(Color.Blue);

            builder.AddField("Name: ", item.name, true);
            builder.AddField("Cost: ", item.cost, true);
            builder.AddField("Tier: ", item.tier, true);
            builder.AddField("Description: ", item.description, true);
            builder.AddField("Effect: ", item.effect, true);

            builder.AddField("Creator: ", item.creator, true);
            builder.AddField("Verified: ", item.verified, true);
            builder.AddField("Rating: ", item.rating, true);
            builder.AddField("Date: ", item.date.ToString("MM/dd/yyyy"), true);

            await Context.Channel.SendMessageAsync("", false, builder.Build());
        }

        private async Task GetIdeaByNameItemActive(string name)
        {
            JsonHandler jsonHandler = new JsonHandler();
            ItemActive itemActive = await jsonHandler.GetIdeaByNameItemActive(name);

            if (itemActive == null)
            {
                await Context.Channel.SendMessageAsync("```diff\n- There is no Active Item idea with the name: " + name + "```");
                return;
            }

            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle(" Item - " + itemActive.name);
            builder.WithFooter("ID: " + itemActive.id.ToString());
            builder.WithColor(Color.Green);

            builder.AddField("Name: ", itemActive.name, true);
            builder.AddField("Description: ", itemActive.description, true);
            builder.AddField("Effect: ", itemActive.effect, true);

            builder.AddField("Creator: ", itemActive.creator, true);
            builder.AddField("Verified: ", itemActive.verified, true);
            builder.AddField("Rating: ", itemActive.rating, true);
            builder.AddField("Date: ", itemActive.date.ToString("MM/dd/yyyy"), true);

            await Context.Channel.SendMessageAsync("", false, builder.Build());
        }

        private async Task GetIdeaByNameEnemy(string name)
        {
            JsonHandler jsonHandler = new JsonHandler();
            Enemy enemy = await jsonHandler.GetIdeaByNameEnemy(name);

            if (enemy == null)
            {
                await Context.Channel.SendMessageAsync("```diff\n- There is no Enemy idea with the name: " + name + "```");
                return;
            }

            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle(" Baby - " + enemy.name);
            builder.WithFooter("ID: " + enemy.id.ToString());
            builder.WithColor(Color.Red);

            builder.AddField("Name: ", enemy.name, true);
            builder.AddField("HP: ", enemy.hp, true);
            builder.AddField("Type: ", enemy.type, true);
            builder.AddField("Damage: ", enemy.damage, true);
            builder.AddField("Firerate: ", enemy.firerate, true);
            builder.AddField("Walkspeed: ", enemy.walkspeed, true);
            builder.AddField("Abilities: ", enemy.abilities, true);
            builder.AddField("Appearance: ", enemy.appearance, true);

            builder.AddField("Creator: ", enemy.creator, true);
            builder.AddField("Verified: ", enemy.verified, true);
            builder.AddField("Rating: ", enemy.rating, true);
            builder.AddField("Date: ", enemy.date.ToString("MM/dd/yyyy"), true);

            await Context.Channel.SendMessageAsync("", false, builder.Build());
        }

        private async Task GetBestRatedBaby(int rateNo)
        {
            JsonHandler jsonHandler = new JsonHandler();
            List<Baby> _baby = await jsonHandler.GetBestRatedBaby(rateNo);

            if (_baby == null)
            {
                await Context.Channel.SendMessageAsync("```diff\n- There are no rated Babies.```");
                return;
            }
            int i = 0;
            foreach (Baby baby in _baby)
            {
                i++;
                if (i > rateNo)
                {
                    break;
                }
                EmbedBuilder builder = new EmbedBuilder();
                builder.WithTitle(" Baby - " + baby.name);
                builder.WithFooter("ID: " + baby.id.ToString());
                builder.WithColor(Color.Gold);

                builder.AddField("Name: ", baby.name, true);
                builder.AddField("Cost: ", baby.cost, true);
                builder.AddField("HP: ", baby.hp, true);
                builder.AddField("Type: ", baby.type, true);
                builder.AddField("Damage: ", baby.damage, true);
                builder.AddField("Firerate: ", baby.firerate, true);
                builder.AddField("Recharge: ", baby.recharge, true);
                builder.AddField("Abilities: ", baby.abilities, true);

                builder.AddField("Creator: ", baby.creator, true);
                builder.AddField("Verified: ", baby.verified, true);
                builder.AddField("Rating: ", baby.rating, true);
                builder.AddField("Date: ", baby.date.ToString("MM/dd/yyyy"), true);

                await Context.Channel.SendMessageAsync("", false, builder.Build());
            }
        }
    }
}
