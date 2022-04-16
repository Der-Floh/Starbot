using Discord;
using Discord.Commands;
using Starbot.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starbot
{
    public class TranslationHandler
    {
        private SocketCommandContext Context;
        public TranslationHandler(SocketCommandContext _context)
        {
            Context = _context;
        }

        public async Task GetTranslation(string text)
        {
            try
            {
                if (text.Length == 2)
                {
                    string fileName = @"Recources/irule/locales/" + text + ".json";
                    if (File.Exists(fileName))
                    {
                        await Context.Channel.SendFileAsync(fileName);
                    }
                    else
                    {
                        Console.WriteLine("Couldn't find existing translation location");
                        await Context.Channel.SendMessageAsync("```diff\n- Couldn't find translation. Make sure the shortform is spelled correctly```");
                    }
                }
                else
                {
                    await Context.Channel.SendMessageAsync("```diff\n- The language shortform has to be 2 letters long.```");
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

        public async Task GetTranslators()
        {
            var channel = Context.Guild.Channels.SingleOrDefault(x => x.Id == 959764938329587712) as IMessageChannel; //Translation channel id = 946083102118281296
            if (channel == null)
            {
                await Context.Channel.SendMessageAsync("```diff\n- Couldn't find Translation channel.```");
                return;
            }

            var translatorMsg = await channel.GetMessageAsync(964520970599661648); //Translators message id = 946083126214545408
            if (translatorMsg == null)
            {
                await Context.Channel.SendMessageAsync("```diff\n- Couldn't find Translators.```");
                return;
            }
            
            Console.WriteLine(translatorMsg.Content);
        }
    }
}
