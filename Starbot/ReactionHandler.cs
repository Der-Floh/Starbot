using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_I.Rule_Suggestions_Bot
{
    public class ReactionHandler
    {
        private readonly DiscordSocketClient _client;

        public ReactionHandler(DiscordSocketClient client)
        {
            _client = client;
        }
        
        public async Task InitializeAsync()
        {
            _client.ReactionAdded += HandleReactionAsync;
            _client.ReactionRemoved += HandleReactionAsync;
        }

        private async Task HandleReactionAsync(Cacheable<IUserMessage, ulong> message, Cacheable<IMessageChannel, ulong> channel, SocketReaction reaction)
        {
            if (message.GetOrDownloadAsync().Result.Author.IsBot) return;

            IUserMessage reactionMsg = message.GetOrDownloadAsync().Result;
            var emotes = await reactionMsg.GetReactionUsersAsync(new Emoji("👍"), 1000).FlattenAsync();
            int thumbsUpCount = 0;
            int thumbsDownCount = 0;
            switch ((reaction.Emote.Name))
            {
                case "👍":
                    emotes = await reactionMsg.GetReactionUsersAsync(new Emoji("👍"), 1000).FlattenAsync();
                    thumbsUpCount = emotes.Count();
                    Console.WriteLine("Thumbs up: " + thumbsUpCount);
                    break;
                case "👎":
                    emotes = await reactionMsg.GetReactionUsersAsync(new Emoji("👎"), 1000).FlattenAsync();
                    thumbsDownCount = emotes.Count();
                    Console.WriteLine("Thumbs down: " + thumbsDownCount);
                    break;
            }
            int rating = thumbsUpCount - thumbsDownCount;
            //var emotes = await message.GetOrDownloadAsync().Result.GetReactionUsersAsync(new Emoji("👍"), 1000).FlattenAsync();
        }
    }
}
