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

        [Command("idea-baby")]
        public async Task HandleBabyIdeaCommand([Remainder]string text)
        {
            try
            {
                string[] statNames = { "Name", "Cost", "HP", "Type", "Damage", "Firerate", "Recharge", "Abilities" };
                string[] stats = await GetIdeaStats(statNames, text);

                string statMsg = "";
                for (int i = 0; i < stats.Length; i++)
                {
                    statMsg += statNames[i] + ": " + stats[i] + "\n";
                }

                Baby baby = new Baby
                {
                    name = stats[0],
                    cost = Int32.Parse(stats[1]),
                    hp = Int32.Parse(stats[2]),
                    type = stats[3],
                    damage = Int32.Parse(stats[4]),
                    firerate = float.Parse(stats[5]),
                    recharge = float.Parse(stats[6]),
                    abilities = stats[7],

                    creator = Context.Message.Author.Username,
                    verified = false,
                    rating = 0,
                    date = DateTime.Today
                };

                JsonHandler jsonHandler = new JsonHandler();
                await jsonHandler.AddBaby(baby);

                await Context.Message.AddReactionAsync(new Emoji("👍"));
                await Context.Message.AddReactionAsync(new Emoji("👎"));
                await Context.Message.AddReactionAsync(new Emoji("💾"));
                //Emote emote = Emote.Parse("<:rb:943432569465221130>");
                //Context.Message.AddReactionAsync(emote);

                await Context.Message.ReplyAsync("Not fully implemented yet");
                await Context.Message.ReplyAsync(statMsg);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex);
                Console.ResetColor();
                SendError(ex);
            }
        }

        [Command("idea-item")]
        public async Task HandleItemIdeaCommand([Remainder] string text)
        {
            try
            {
                string[] statNames = { "Name", "Cost", "Tier", "Description", "Effect" };
                string[] stats = await GetIdeaStats(statNames, text);

                string statMsg = "";
                for (int i = 0; i < stats.Length; i++)
                {
                    statMsg += statNames[i] + ": " + stats[i] + "\n";
                }
                /*
                Item item = new Item
                {
                    name = stats[0],
                    cost = Int32.Parse(stats[1]),
                    tier = Int32.Parse(stats[2]),
                    description = stats[3],
                    effect = stats[4],

                    creator = Context.Message.Author.Username,
                    verified = false,
                    rating = 0,
                    date = DateTime.Today
                };*/
                Item item = new Item();
                item.name = stats[0];
                item.cost = Int32.Parse(stats[1]);
                item.tier = Int32.Parse(stats[2]);
                item.description = stats[3];
                item.effect = stats[4];

                JsonHandler jsonHandler = new JsonHandler();
                await jsonHandler.AddItem(item);

                await Context.Message.AddReactionAsync(new Emoji("👍"));
                await Context.Message.AddReactionAsync(new Emoji("👎"));
                await Context.Message.AddReactionAsync(new Emoji("💾"));
                //Emote emote = Emote.Parse("<:rb:943432569465221130>");
                //Context.Message.AddReactionAsync(emote);

                await Context.Message.ReplyAsync("Not fully implemented yet");
                await Context.Message.ReplyAsync(statMsg);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex);
                Console.ResetColor();
                SendError(ex);
            }
        }

        private async Task SendError(Exception ex)
        {
            await Context.Message.ReplyAsync("```diff\n- The command resolved in an error!\n\n" + ex + "```");
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
                        //last = text.Length;
                        if (text.Contains(statNames[j] + "="))
                        {
                            last = text.LastIndexOf(statNames[j] + "=");
                            break;
                        }
                    }
                    stats[i] = text.Substring(first, last - first);
                    stats[i] = stats[i].Replace("\n", "");
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
    }
}
