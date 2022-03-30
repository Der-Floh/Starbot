﻿using System;
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
                int.TryParse(stats[2], out parsedInt);
                baby.damage = parsedInt;
                float.TryParse(stats[2], out parsedFloat);
                baby.firerate = parsedFloat;
                float.TryParse(stats[2], out parsedFloat);
                baby.recharge = parsedFloat;
                baby.abilities = stats[7];

                baby.creator = Context.Message.Author.Username;
                baby.verified = false;
                baby.rating = 0;
                baby.date = DateTime.Now.Date;

                //await Context.Message.ReplyAsync("Not fully implemented yet");

                IUserMessage botMessage = await SendEmbedIdea(statNames, stats, "Baby");

                baby.id = botMessage.Id;
                await jsonHandler.AddBaby(baby);

                await botMessage.AddReactionAsync(new Emoji("👍"));
                await botMessage.AddReactionAsync(new Emoji("👎"));
                await botMessage.AddReactionAsync(new Emoji("💾"));
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

                //await Context.Message.ReplyAsync("Not fully implemented yet");

                await Context.Message.DeleteAsync();
                IUserMessage botMessage = await SendEmbedIdea(statNames, stats, "Item");

                item.id = botMessage.Id;
                await jsonHandler.AddItem(item);

                await botMessage.AddReactionAsync(new Emoji("👍"));
                await botMessage.AddReactionAsync(new Emoji("👎"));
                await botMessage.AddReactionAsync(new Emoji("💾"));
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

            builder.WithColor(Color.Red);
            IUserMessage userMessage = await Context.Channel.SendMessageAsync("", false, builder.Build());
            return userMessage;
        }
    }
}
