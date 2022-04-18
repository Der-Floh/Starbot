using Discord;
using Discord.Commands;
using Starbot;
using Starbot.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Starbot
{
    public class IdeaHandler
    {
        private SocketCommandContext Context;
        private System.Timers.Timer deletionTime;
        public IdeaHandler(SocketCommandContext _context)
        {
            Context = _context;

            deletionTime = new System.Timers.Timer(30000);
            deletionTime.Elapsed += TimerTick;
            deletionTime.AutoReset = false;
        }
        private async void TimerTick(object source, ElapsedEventArgs e)
        {
            Console.WriteLine("Deletion canceled");
            Idea.deleteBaby = 0;
            Idea.deleteItem = 0;
            Idea.deleteItemActive = 0;
            Idea.deleteEnemy = 0;
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
                switch (type)
                {
                    case "Baby": await GetIdeaBaby(text); break;
                    case "Item": await GetIdeaItem(text); break;
                    case "ItemActive": await GetIdeaItemActive(text); break;
                    case "Enemy": await GetIdeaEnemy(text); break;
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

        public async Task GetExistingIdea(string type, string text)
        {
            try
            {
                switch (type)
                {
                    case "Baby": await GetExistingBaby(text); break;
                    case "Item": await GetExistingItem(text); break;
                    case "ItemActive": await GetExistingItemActive(text); break;
                    case "Enemy": await GetExistingEnemy(text); break;
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
                int rateNo = 0;
                text = text.Replace(" ", "");
                if (text == "")
                {
                    rateNo = 1;
                }
                else
                {
                    rateNo = int.Parse(text);
                }
                switch (type)
                {
                    case "Baby": await GetBestRatedBaby(rateNo); break;
                    case "Item": await GetBestRatedItem(rateNo); break;
                    case "ItemActive": await GetBestRatedItemActive(rateNo); break;
                    case "Enemy":  await GetBestRatedEnemy(rateNo); break;
                    case "All":  break;
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

        public async Task DeleteIdea(string type, string text)
        {
            try
            {
                switch (type)
                {
                    case "Baby": await DeleteIdeaBaby(text); break;
                    case "Item": await DeleteIdeaItem(text); break;
                    case "ItemActive": await DeleteIdeaItemActive(text); break;
                    case "Enemy": await DeleteIdeaEnemy(text); break;
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

        public async Task DeleteIdeaAll(string type, string text)
        {
            try
            {
                switch (type)
                {
                    case "Baby": await DeleteAllIdeaBaby(text); break;
                    case "Item": await DeleteAllIdeaItem(text); break;
                    case "ItemActive": await DeleteAllIdeaItemActive(text); break;
                    case "Enemy": await DeleteAllIdeaEnemy(text); break;
                    case "All": await DeleteAllIdea(text); break;
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

            if (stats == null) return;
            //JsonHandler jsonHandler = new JsonHandler();

            int parsedInt = 0;
            float parsedFloat = 0;
            Baby baby = new Baby();
            baby.id = Context.Message.Id;
            baby.name = stats[0];
            if (!int.TryParse(stats[1], out parsedInt)) parsedInt = await StringValueCostToIntBaby(stats[1]);
            baby.cost = parsedInt;
            if (!int.TryParse(stats[2], out parsedInt)) parsedInt = await StringValueHPToIntBaby(stats[2]);
            baby.hp = parsedInt;
            baby.type = stats[3];
            if (!int.TryParse(stats[4], out parsedInt)) parsedInt = await StringValueDamageToIntBaby(stats[4]);
            baby.damage = parsedInt;
            if (!float.TryParse(stats[5], out parsedFloat)) parsedFloat = await StringValueFirerateToFloatBaby(stats[5]);
            baby.firerate = parsedFloat;
            if (!float.TryParse(stats[6], out parsedFloat)) parsedFloat = await StringValueRechargeToFloatBaby(stats[6]);
            baby.recharge = parsedFloat;
            baby.abilities = stats[7];

            baby.creator = Context.Message.Author.Username;
            baby.verified = false;
            baby.rating = 0;
            baby.date = DateTime.Now;

            if (baby.name.Length <= 2) return;
            if (baby.abilities.Length <= 2) return;

            stats[1] = baby.cost.ToString();
            stats[2] = baby.hp.ToString();
            stats[4] = baby.damage.ToString();
            stats[5] = baby.firerate.ToString();
            stats[6] = baby.recharge.ToString();

            await Context.Message.DeleteAsync();
            IUserMessage botMessage = await SendEmbedIdea(statNames, stats, "Baby");

            baby.id = botMessage.Id;
            await Idea.AddBaby(baby); //jsonHandler.AddBaby(baby);

            await botMessage.AddReactionAsync(new Emoji("👍"));
            await botMessage.AddReactionAsync(new Emoji("👎"));
            await botMessage.AddReactionAsync(new Emoji("💾"));
        }
        private async Task IdeaItem(string text)
        {
            string[] statNames = { "Name", "Cost", "Tier", "Description", "Effect" };
            string[] stats = await GetIdeaStats(statNames, text);

            if (stats == null) return;
            //JsonHandler jsonHandler = new JsonHandler();

            int parsedInt = 0;
            Item item = new Item();
            item.id = Context.Message.Id;
            item.name = stats[0];
            if (!int.TryParse(stats[1], out parsedInt)) parsedInt = await StringValueCostToIntItem(stats[1]);
            item.cost = parsedInt;
            switch (item.cost)
            {
                case 33:
                    item.tier = 1;
                    break;
                case 99:
                    item.tier = 2;
                    break;
            }
            if (item.cost != 33 && item.cost != 99)
            {
                if (!int.TryParse(stats[2], out parsedInt)) parsedInt = await StringValueTierToIntItem(stats[2]);
                item.tier = parsedInt;
                switch (item.tier)
                {
                    case 1:
                        item.cost = 33;
                        break;
                    case 2:
                        item.cost = 99;
                        break;
                }
            }
            
            item.description = stats[3];
            item.effect = stats[4];

            item.creator = Context.Message.Author.Username;
            item.verified = false;
            item.rating = 0;
            item.date = DateTime.Now;

            if (item.name.Length <= 2) return;
            if (item.cost <= 0) return;
            if (item.description.Length <= 2 && item.effect.Length <= 2) return;

            stats[1] = item.cost.ToString();
            stats[2] = item.tier.ToString();

            await Context.Message.DeleteAsync();
            IUserMessage botMessage = await SendEmbedIdea(statNames, stats, "Item");

            item.id = botMessage.Id;
            await Idea.AddItem(item); //jsonHandler.AddItem(item);

            await botMessage.AddReactionAsync(new Emoji("👍"));
            await botMessage.AddReactionAsync(new Emoji("👎"));
            await botMessage.AddReactionAsync(new Emoji("💾"));
        }
        private async Task IdeaItemActive(string text)
        {
            string[] statNames = { "Name", "Description", "Effect" };
            string[] stats = await GetIdeaStats(statNames, text);

            if (stats == null) return;
            //JsonHandler jsonHandler = new JsonHandler();

            ItemActive itemActive = new ItemActive();
            itemActive.id = Context.Message.Id;
            itemActive.name = stats[0];
            itemActive.description = stats[1];
            itemActive.effect = stats[2];

            itemActive.creator = Context.Message.Author.Username;
            itemActive.verified = false;
            itemActive.rating = 0;
            itemActive.date = DateTime.Now;

            if (itemActive.name.Length <= 2) return;
            if (itemActive.description.Length <= 2 && itemActive.effect.Length <= 2) return;

            await Context.Message.DeleteAsync();
            IUserMessage botMessage = await SendEmbedIdea(statNames, stats, "ItemActive");

            itemActive.id = botMessage.Id;
            await Idea.AddItemActive(itemActive); //jsonHandler.AddItemActive(itemActive);

            await botMessage.AddReactionAsync(new Emoji("👍"));
            await botMessage.AddReactionAsync(new Emoji("👎"));
            await botMessage.AddReactionAsync(new Emoji("💾"));
        }
        private async Task IdeaEnemy(string text)
        {
            string[] statNames = { "Name", "HP", "Type", "Damage", "Firerate", "Walkspeed", "Abilities", "Appearance"};
            string[] stats = await GetIdeaStats(statNames, text);

            if (stats == null) return;
            //JsonHandler jsonHandler = new JsonHandler();

            int parsedInt = 0;
            float parsedFloat = 0;
            Enemy enemy = new Enemy();
            enemy.id = Context.Message.Id;
            enemy.name = stats[0];
            if (!int.TryParse(stats[1], out parsedInt)) parsedInt = await StringValueHPToIntEnemy(stats[1]);
            enemy.hp = parsedInt;
            enemy.type = stats[2];
            if (!int.TryParse(stats[3], out parsedInt)) parsedInt = await StringValueDamageToIntEnemy(stats[3]);
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

            if (enemy.name.Length <= 2) return;

            stats[1] = enemy.hp.ToString();
            stats[3] = enemy.damage.ToString();
            stats[4] = enemy.firerate.ToString();
            stats[5] = enemy.walkspeed.ToString();

            await Context.Message.DeleteAsync();
            IUserMessage botMessage = await SendEmbedIdea(statNames, stats, "Enemy");

            enemy.id = botMessage.Id;
            await Idea.AddEnemy(enemy); //jsonHandler.AddEnemy(enemy);

            await botMessage.AddReactionAsync(new Emoji("👍"));
            await botMessage.AddReactionAsync(new Emoji("👎"));
            await botMessage.AddReactionAsync(new Emoji("💾"));
        }

        private async Task<string[]> GetIdeaStats(string[] statNames, string text)
        {
            int first = 0;
            int last = 0;
            string[] stats = new string[statNames.Length];

            string equalsString = "=";
            if (!text.Contains('='))
            {
                if (text.Contains(':'))
                {
                    Console.WriteLine("No '=' found setting equalsString to ':'");
                    equalsString = ":";
                }
                else
                {
                    Console.WriteLine("Couldn't find equalsString");
                    return null;
                }
            }

            for (int i = 0; i <= statNames.Length - 1; i++)
            {
                if (text.Contains(statNames[i], StringComparison.OrdinalIgnoreCase))
                {
                    first = text.IndexOf(statNames[i] + equalsString, StringComparison.OrdinalIgnoreCase) + (statNames[i] + equalsString).Length;
                    //last = text.LastIndexOf(statNames[i + 1]);
                    last = text.Length;
                    for (int j = i + 1; j < statNames.Length; j++)
                    {
                        //last = text.Length;
                        if (text.Contains(statNames[j] + equalsString, StringComparison.OrdinalIgnoreCase))
                        {
                            last = text.LastIndexOf(statNames[j] + equalsString, StringComparison.OrdinalIgnoreCase);
                            break;
                        }
                    }
                    stats[i] = text.Substring(first, last - first);
                    stats[i] = stats[i].Replace("\n", "");
                    
                    stats[i] = stats[i].Trim();
                    /*
                    while (stats[i][0] == ' ')
                    {
                        stats[i] = stats[i].Remove(0, 1);
                    }
                    while (stats[i][stats[i].Length - 1] == ' ')
                    {
                        stats[i] = stats[i].Remove(0, 1);
                    }*/
                    /*
                    if (stats[i][0] == ' ')
                    {
                        stats[i] = stats[i].Remove(0, 1);
                        Console.WriteLine("Removed space=" + stats[i]);
                    }*/
                }
                else
                {
                    stats[i] = "-";
                }
            }
            if (text.Contains(statNames[statNames.Length - 1], StringComparison.OrdinalIgnoreCase))
            {
                first = text.IndexOf(statNames[statNames.Length - 1] + equalsString, StringComparison.OrdinalIgnoreCase) + (statNames[statNames.Length - 1] + equalsString).Length;
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

        private async Task<int> StringValueCostToIntBaby(string strValue)
        {
            strValue = strValue.ToLower();
            switch (strValue)
            {
                case "small": return 25;
                case "little": return 25;
                case "low": return 25;
                case "normal": return 100;
                case "middle": return 100;
                case "average": return 100;
                case "high": return 200;
                case "big": return 200;
                case "huge": return 300;
            }
            return 0;
        }
        private async Task<int> StringValueHPToIntBaby(string strValue)
        {
            strValue = strValue.ToLower();
            switch (strValue)
            {
                case "small": return 40;
                case "little": return 40;
                case "low": return 40;
                case "normal": return 80;
                case "middle": return 80;
                case "average": return 80;
                case "high": return 400;
                case "big": return 400;
                case "huge": return 400;
            }
            return 0;
        }
        private async Task<int> StringValueDamageToIntBaby(string strValue)
        {
            strValue = strValue.ToLower();
            switch (strValue)
            {
                case "small": return 16;
                case "little": return 16;
                case "low": return 16;
                case "normal": return 20;
                case "middle": return 20;
                case "average": return 20;
                case "high": return 40;
                case "big": return 40;
                case "huge": return 60;
                case "explosion": return 2000;
                case "exploding": return 2000;
            }
            return 0;
        }
        private async Task<float> StringValueFirerateToFloatBaby(string strValue)
        {
            strValue = strValue.ToLower();
            switch (strValue)
            {
                case "small": return 0.75f;
                case "little": return 0.75f;
                case "low": return 0.75f;
                case "normal": return 1.16f;
                case "middle": return 1.16f;
                case "average": return 1.16f;
                case "high": return 30;
                case "big": return 30;
                case "huge": return 50;
            }
            return 0;
        }
        private async Task<float> StringValueRechargeToFloatBaby(string strValue)
        {
            strValue = strValue.ToLower();
            switch (strValue)
            {
                case "small": return 2.5f;
                case "little": return 2.5f;
                case "low": return 2.5f;
                case "normal": return 5;
                case "middle": return 5;
                case "average": return 5;
                case "high": return 16.5f;
                case "big": return 16.5f;
                case "huge": return 20;
            }
            return 0;
        }
        private async Task<int> StringValueCostToIntItem(string strValue)
        {
            strValue = strValue.ToLower();
            switch (strValue)
            {
                case "small": return 33;
                case "little": return 33;
                case "low": return 33;
                case "normal": return 33;
                case "middle": return 33;
                case "average": return 33;
                case "high": return 99;
                case "big": return 99;
                case "huge": return 133;
            }
            return 0;
        }
        private async Task<int> StringValueTierToIntItem(string strValue)
        {
            strValue = strValue.ToLower();
            switch (strValue)
            {
                case "small": return 1;
                case "little": return 1;
                case "low": return 1;
                case "normal": return 1;
                case "middle": return 1;
                case "average": return 1;
                case "high": return 2;
                case "big": return 2;
                case "huge": return 3;
            }
            return 0;
        }
        private async Task<int> StringValueHPToIntEnemy(string strValue)
        {
            strValue = strValue.ToLower();
            switch (strValue)
            {
                case "small": return 50;
                case "little": return 50;
                case "low": return 50;
                case "normal": return 200;
                case "middle": return 200;
                case "average": return 200;
                case "high": return 500;
                case "big": return 500;
                case "stone": return 1000;
                case "hard": return 1000;
                case "huge": return 2000;
            }
            return 0;
        }
        private async Task<int> StringValueDamageToIntEnemy(string strValue)
        {
            strValue = strValue.ToLower();
            switch (strValue)
            {
                case "small": return 4;
                case "little": return 4;
                case "low": return 4;
                case "normal": return 8;
                case "middle": return 8;
                case "average": return 8;
                case "high": return 25;
                case "big": return 25;
                case "huge": return 50;
                case "infinite": return 400;
                case "infinity": return 400;
            }
            return 0;
        }
        private async Task<float> StringValueFirerateToFloatEnemy(string strValue)
        {
            strValue = strValue.ToLower();
            switch (strValue)
            {
                case "small": return 0;
                case "little": return 0;
                case "low": return 0;
                case "normal": return 0;
                case "middle": return 0;
                case "average": return 0;
                case "high": return 0;
                case "big": return 0;
                case "huge": return 0;
            }
            return 0;
        }
        private async Task<float> StringValueWalkspeedToFloatEnemy(string strValue)
        {
            strValue = strValue.ToLower();
            switch (strValue)
            {
                case "small": return 0;
                case "little": return 0;
                case "low": return 0;
                case "normal": return 0;
                case "middle": return 0;
                case "average": return 0;
                case "high": return 0;
                case "big": return 0;
                case "huge": return 0;
            }
            return 0;
        }

        private async Task GetIdeaBaby(string text)
        {
            Baby baby = await Idea.GetBaby(text);

            if (baby == null)
            {
                await Context.Channel.SendMessageAsync("```diff\n- There is no Baby idea with the id or name: " + text + "```");
                return;
            }

            await buildMessageBaby(baby);
        }
        private async Task GetIdeaItem(string text)
        {
            Item item = await Idea.GetItem(text);

            if (item == null)
            {
                await Context.Channel.SendMessageAsync("```diff\n- There is no Item idea with the id or name: " + text + "```");
                return;
            }

            await buildMessageItem(item);
        }
        private async Task GetIdeaItemActive(string text)
        {
            ItemActive itemActive = await Idea.GetItemActive(text);

            if (itemActive == null)
            {
                await Context.Channel.SendMessageAsync("```diff\n- There is no Active Item idea with the id or name: " + text + "```");
                return;
            }

            await buildMessageItemActive(itemActive);
        }
        private async Task GetIdeaEnemy(string text)
        {
            Enemy enemy = await Idea.GetEnemy(text);

            if (enemy == null)
            {
                await Context.Channel.SendMessageAsync("```diff\n- There is no Enemy idea with the id or name: " + text + "```");
                return;
            }

            await buildMessageEnemy(enemy);
        }

        private async Task GetBestRatedBaby(int rateNo)
        {
            List<Baby> _baby = await Idea.GetBestRatedBabies();

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
                await buildMessageBaby(baby);
            }
        }
        private async Task GetBestRatedItem(int rateNo)
        {
            List<Item> _item = await Idea.GetBestRatedItems();

            if (_item == null)
            {
                await Context.Channel.SendMessageAsync("```diff\n- There are no rated Items.```");
                return;
            }
            int i = 0;
            foreach (Item item in _item)
            {
                i++;
                if (i > rateNo)
                {
                    break;
                }
                await buildMessageItem(item);
            }
        }
        private async Task GetBestRatedItemActive(int rateNo)
        {
            List<ItemActive> _itemActive = await Idea.GetBestRatedItemsActive();

            if (_itemActive == null)
            {
                await Context.Channel.SendMessageAsync("```diff\n- There are no rated Active Items.```");
                return;
            }
            int i = 0;
            foreach (ItemActive itemActive in _itemActive)
            {
                i++;
                if (i > rateNo)
                {
                    break;
                }
                await buildMessageItemActive(itemActive);
            }
        }
        private async Task GetBestRatedEnemy(int rateNo)
        {
            List<Enemy> _enemy = await Idea.GetBestRatedEnemies();

            if (_enemy == null)
            {
                await Context.Channel.SendMessageAsync("```diff\n- There are no rated Enemies.```");
                return;
            }
            int i = 0;
            foreach (Enemy enemy in _enemy)
            {
                i++;
                if (i > rateNo)
                {
                    break;
                }
                await buildMessageEnemy(enemy);
            }
        }

        private async Task GetExistingBaby(string text)
        {
            ExistingBaby existingBaby = await Idea.GetExistingBaby(text);

            if (existingBaby == null)
            {
                await Context.Channel.SendMessageAsync("```diff\n- There is no existing Baby with the id or name: " + text + "```");
                return;
            }

            await buildMessageExistingBaby(existingBaby);
        }
        private async Task GetExistingItem(string text)
        {
            ExistingItem existingItem = await Idea.GetExistingItem(text);

            if (existingItem == null)
            {
                await Context.Channel.SendMessageAsync("```diff\n- There is no existing Item with the id or name: " + text + "```");
                return;
            }

            await buildMessageExistingItem(existingItem);
        }
        private async Task GetExistingItemActive(string text)
        {
            ExistingItemActive existingItemActive = await Idea.GetExistingItemActive(text);

            if (existingItemActive == null)
            {
                await Context.Channel.SendMessageAsync("```diff\n- There is no existing Active idea with the id or name: " + text + "```");
                return;
            }

            await buildMessageExistingItemActive(existingItemActive);
        }
        private async Task GetExistingEnemy(string text)
        {
            ExistingEnemy existingEnemy = await Idea.GetExistingEnemy(text);

            if (existingEnemy == null)
            {
                await Context.Channel.SendMessageAsync("```diff\n- There is no existing Enemy with the id or name: " + text + "```");
                return;
            }

            await buildMessageExistingEnemy(existingEnemy);
        }

        private async Task DeleteIdeaBaby(string text)
        {
            Baby baby = await Idea.GetBaby(text);

            if (baby == null)
            {
                await Context.Channel.SendMessageAsync("```diff\n- There is no Baby idea with the id or name: " + text + "```");
                return;
            }

            if (baby.id == Idea.deleteBaby && Context.User.Id == Idea.deleteBabyUserID)
            {
                Idea.deleteBaby = 0;
                Idea.deleteBabyUserID = 0;
                deletionTime.Stop();
                Console.WriteLine("Delete confirmed");
                await Idea.DeleteBaby(baby);
                await Context.Channel.SendMessageAsync("```diff\n Succesfully deleted Baby```");
            }
            else
            {
                Idea.deleteBaby = baby.id;
                Idea.deleteBabyUserID = Context.User.Id;
                deletionTime.Start();
                Console.WriteLine("Waiting for delete confirmation");
                await buildMessageBaby(baby);
                await Context.Channel.SendMessageAsync("```diff\n Please confirm the deletion of the Baby above, by typing the command again```");
            }
        }
        private async Task DeleteIdeaItem(string text)
        {
            Item item = await Idea.GetItem(text);

            if (item == null)
            {
                await Context.Channel.SendMessageAsync("```diff\n- There is no Item idea with the id or name: " + text + "```");
                return;
            }

            if (item.id == Idea.deleteItem && Context.User.Id == Idea.deleteItemUserID)
            {
                Idea.deleteItem = 0;
                Idea.deleteItemUserID = 0;
                deletionTime.Stop();
                Console.WriteLine("Delete confirmed");
                await Idea.DeleteItem(item);
                await Context.Channel.SendMessageAsync("```diff\n Succesfully deleted Item```");
            }
            else
            {
                Idea.deleteItem = item.id;
                Idea.deleteItemUserID = Context.User.Id;
                deletionTime.Start();
                Console.WriteLine("Waiting for delete confirmation");
                await buildMessageItem(item);
                await Context.Channel.SendMessageAsync("```diff\n Please confirm the deletion of the Item above, by typing the command again```");
            }
        }
        private async Task DeleteIdeaItemActive(string text)
        {
            ItemActive itemActive = await Idea.GetItemActive(text);

            if (itemActive == null)
            {
                await Context.Channel.SendMessageAsync("```diff\n- There is no Item idea with the id or name: " + text + "```");
                return;
            }

            if (itemActive.id == Idea.deleteItemActive && Context.User.Id == Idea.deleteItemActiveUserID)
            {
                Idea.deleteItemActive = 0;
                Idea.deleteItemActiveUserID = 0;
                deletionTime.Stop();
                Console.WriteLine("Delete confirmed");
                await Idea.DeleteItemActive(itemActive);
                await Context.Channel.SendMessageAsync("```diff\n Succesfully deleted Item```");
            }
            else
            {
                Idea.deleteItemActive = itemActive.id;
                Idea.deleteItemActiveUserID = Context.User.Id;
                deletionTime.Start();
                Console.WriteLine("Waiting for delete confirmation");
                await buildMessageItemActive(itemActive);
                await Context.Channel.SendMessageAsync("```diff\n Please confirm the deletion of the Active Item above, by typing the command again```");
            }
        }
        private async Task DeleteIdeaEnemy(string text)
        {
            Enemy enemy = await Idea.GetEnemy(text);

            if (enemy == null)
            {
                await Context.Channel.SendMessageAsync("```diff\n- There is no Item idea with the id or name: " + text + "```");
                return;
            }

            if (enemy.id == Idea.deleteEnemy && Context.User.Id == Idea.deleteEnemyUserID)
            {
                Idea.deleteEnemy = 0;
                Idea.deleteEnemyUserID = 0;
                deletionTime.Stop();
                Console.WriteLine("Delete confirmed");
                await Idea.DeleteEnemy(enemy);
                await Context.Channel.SendMessageAsync("```diff\n Succesfully deleted Item```");
            }
            else
            {
                Idea.deleteEnemy = enemy.id;
                Idea.deleteEnemyUserID = Context.User.Id;
                deletionTime.Start();
                Console.WriteLine("Waiting for delete confirmation");
                await buildMessageEnemy(enemy);
                await Context.Channel.SendMessageAsync("```diff\n Please confirm the deletion of the Enemy above, by typing the command again```");
            }
        }

        public async Task DeleteAllIdeaBaby(string text)
        {
            int delRating = 0;
            if (!int.TryParse(text, out delRating)) return;

            if (Context.User.Id == Idea.deleteEnemyUserID)
            {
                Idea.deleteEnemyUserID = 0;
                deletionTime.Stop();
                Console.WriteLine("Delete confirmed");
                await Idea.DeleteAllBaby(delRating);
                await Context.Channel.SendMessageAsync("```diff\n Succesfully deleted all Babies with a rating of " +delRating+ " or lower```");
            }
            else
            {
                Idea.deleteEnemyUserID = Context.User.Id;
                deletionTime.Start();
                Console.WriteLine("Waiting for delete confirmation");
                await Context.Channel.SendMessageAsync("```diff\n Please confirm the deletion of all Babies with a rating of " +delRating+ " or lower, by typing the command again```");
            }
            
        }
        private async Task DeleteAllIdeaItem(string text)
        {
            int delRating = 0;
            if (!int.TryParse(text, out delRating)) return;

            if (Context.User.Id == Idea.deleteItemUserID)
            {
                Idea.deleteItemUserID = 0;
                deletionTime.Stop();
                Console.WriteLine("Delete confirmed");
                await Idea.DeleteAllItem(delRating);
                await Context.Channel.SendMessageAsync("```diff\n Succesfully deleted all Items with a rating of " + delRating + " or lower```");
            }
            else
            {
                Idea.deleteItemUserID = Context.User.Id;
                deletionTime.Start();
                Console.WriteLine("Waiting for delete confirmation");
                await Context.Channel.SendMessageAsync("```diff\n Please confirm the deletion of all Items with a rating of " + delRating + " or lower, by typing the command again```");
            }
        }
        private async Task DeleteAllIdeaItemActive(string text)
        {
            int delRating = 0;
            if (!int.TryParse(text, out delRating)) return;

            if (Context.User.Id == Idea.deleteItemActiveUserID)
            {
                Idea.deleteItemActiveUserID = 0;
                deletionTime.Stop();
                Console.WriteLine("Delete confirmed");
                await Idea.DeleteAllItemActive(delRating);
                await Context.Channel.SendMessageAsync("```diff\n Succesfully deleted all Active Items with a rating of " + delRating + " or lower```");
            }
            else
            {
                Idea.deleteItemActiveUserID = Context.User.Id;
                deletionTime.Start();
                Console.WriteLine("Waiting for delete confirmation");
                await Context.Channel.SendMessageAsync("```diff\n Please confirm the deletion of all Active Items with a rating of " + delRating + " or lower, by typing the command again```");
            }
        }
        private async Task DeleteAllIdeaEnemy(string text)
        {
            int delRating = 0;
            if (!int.TryParse(text, out delRating)) return;

            if (Context.User.Id == Idea.deleteEnemyUserID)
            {
                Idea.deleteEnemyUserID = 0;
                deletionTime.Stop();
                Console.WriteLine("Delete confirmed");
                await Idea.DeleteAllEnemy(delRating);
                await Context.Channel.SendMessageAsync("```diff\n Succesfully deleted all Enemies with a rating of " + delRating + " or lower```");
            }
            else
            {
                Idea.deleteEnemyUserID = Context.User.Id;
                deletionTime.Start();
                Console.WriteLine("Waiting for delete confirmation");
                await Context.Channel.SendMessageAsync("```diff\n Please confirm the deletion of all Enemies with a rating of " + delRating + " or lower, by typing the command again```");
            }
        }
        private async Task DeleteAllIdea(string text)
        {
            int delRating = 0;
            if (!int.TryParse(text, out delRating)) return;

            if (Context.User.Id == Idea.deleteBabyUserID && Context.User.Id == Idea.deleteItemUserID && Context.User.Id == Idea.deleteItemActiveUserID && Context.User.Id == Idea.deleteEnemyUserID)
            {
                Idea.deleteBabyUserID = 0;
                Idea.deleteItemUserID = 0;
                Idea.deleteItemActiveUserID = 0;
                Idea.deleteEnemyUserID = 0;
                deletionTime.Stop();
                Console.WriteLine("Delete confirmed");
                await Idea.DeleteAllEnemy(delRating);
                await Context.Channel.SendMessageAsync("```diff\n Succesfully deleted all Ideas with a rating of " + delRating + " or lower```");
            }
            else
            {
                Idea.deleteBabyUserID = Context.User.Id;
                Idea.deleteItemUserID = Context.User.Id;
                Idea.deleteItemActiveUserID = Context.User.Id;
                Idea.deleteEnemyUserID = Context.User.Id;
                deletionTime.Start();
                Console.WriteLine("Waiting for delete confirmation");
                await Context.Channel.SendMessageAsync("```diff\n Please confirm the deletion of all Ideas with a rating of " + delRating + " or lower, by typing the command again```");
            }
        }

        private async Task<IUserMessage> buildMessageBaby(Baby baby)
        {
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

            return await Context.Channel.SendMessageAsync("", false, builder.Build());
        }
        private async Task<IUserMessage> buildMessageItem(Item item)
        {
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

            return await Context.Channel.SendMessageAsync("", false, builder.Build());
        }
        private async Task<IUserMessage> buildMessageItemActive(ItemActive itemActive)
        {
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

            return await Context.Channel.SendMessageAsync("", false, builder.Build());
        }
        private async Task<IUserMessage> buildMessageEnemy(Enemy enemy)
        {
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

            return await Context.Channel.SendMessageAsync("", false, builder.Build());
        }

        private async Task<IUserMessage> buildMessageExistingBaby(ExistingBaby existingBaby)
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle(" Baby - " + existingBaby.name);
            builder.WithFooter("ID: " + existingBaby.id.ToString());
            builder.WithColor(Color.Gold);

            builder.AddField("Name: ", existingBaby.name, true);
            builder.AddField("Cost: ", existingBaby.cost, true);
            builder.AddField("HP: ", existingBaby.hp, true);
            builder.AddField("Type: ", existingBaby.type, true);
            builder.AddField("Damage: ", existingBaby.damage, true);
            builder.AddField("Firerate: ", existingBaby.firerate, true);
            builder.AddField("Recharge: ", existingBaby.recharge, true);
            builder.AddField("Abilities: ", existingBaby.abilities, true);

            builder.AddField("Unlocking: ", existingBaby.unlocking);
            builder.AddField("Creator: ", existingBaby.creator, true);

            return await Context.Channel.SendMessageAsync("", false, builder.Build());
        }
        private async Task<IUserMessage> buildMessageExistingItem(ExistingItem existingItem)
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle(" Item - " + existingItem.name);
            builder.WithFooter("ID: " + existingItem.id.ToString());
            builder.WithColor(Color.Blue);

            builder.AddField("Name: ", existingItem.name, true);
            builder.AddField("Cost: ", existingItem.cost, true);
            builder.AddField("Tier: ", existingItem.tier, true);
            builder.AddField("Description: ", existingItem.description, true);
            builder.AddField("Effect: ", existingItem.effect, true);

            builder.AddField("Unlocking: ", existingItem.unlocking);
            builder.AddField("Creator: ", existingItem.creator, true);

            return await Context.Channel.SendMessageAsync("", false, builder.Build());
        }
        private async Task<IUserMessage> buildMessageExistingItemActive(ExistingItemActive existingItemActive)
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle(" Item - " + existingItemActive.name);
            builder.WithFooter("ID: " + existingItemActive.id.ToString());
            builder.WithColor(Color.Green);

            builder.AddField("Name: ", existingItemActive.name, true);
            builder.AddField("Description: ", existingItemActive.description, true);
            builder.AddField("Effect: ", existingItemActive.effect, true);

            builder.AddField("Unlocking: ", existingItemActive.unlocking);
            builder.AddField("Creator: ", existingItemActive.creator, true);

            return await Context.Channel.SendMessageAsync("", false, builder.Build());
        }
        private async Task<IUserMessage> buildMessageExistingEnemy(ExistingEnemy existingEnemy)
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle(" Baby - " + existingEnemy.name);
            builder.WithFooter("ID: " + existingEnemy.id.ToString());
            builder.WithColor(Color.Red);

            builder.AddField("Name: ", existingEnemy.name, true);
            builder.AddField("HP: ", existingEnemy.hp, true);
            builder.AddField("Type: ", existingEnemy.type, true);
            builder.AddField("Damage: ", existingEnemy.damage, true);
            builder.AddField("Firerate: ", existingEnemy.firerate, true);
            builder.AddField("Walkspeed: ", existingEnemy.walkspeed, true);
            builder.AddField("Abilities: ", existingEnemy.abilities, true);
            builder.AddField("Appearance: ", existingEnemy.appearance, true);

            builder.AddField("Unlocking: ", existingEnemy.unlocking);
            builder.AddField("Creator: ", existingEnemy.creator, true);

            return await Context.Channel.SendMessageAsync("", false, builder.Build());
        }
    }
}
