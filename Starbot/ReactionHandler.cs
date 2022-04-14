using Discord;
using Discord.WebSocket;
using Starbot;
using Starbot.Types;
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
            if (reaction.User.Value.IsBot) return;
            

            IUserMessage reactionMsg = message.GetOrDownloadAsync().Result;
            int thumbsUpCount = 0;
            int thumbsDownCount = 0;
            if (reaction.Emote.Name == "👍" || reaction.Emote.Name == "👎")
            {
                var emotes = await reactionMsg.GetReactionUsersAsync(new Emoji("👍"), 1000).FlattenAsync();
                thumbsUpCount = emotes.Count();
                
                emotes = await reactionMsg.GetReactionUsersAsync(new Emoji("👎"), 1000).FlattenAsync();
                thumbsDownCount = emotes.Count();
            }
            else
            {
                return;
            }
            int rating = thumbsUpCount - thumbsDownCount;

            IEmbed[] embed = reactionMsg.Embeds.ToArray();
            string type = embed[0].ToString().Substring(0, embed[0].ToString().IndexOf(" "));

            //JsonHandler jsonHandler = new JsonHandler();
            //await jsonHandler.SetRating(reactionMsg.Id, type, rating);

            switch (type)
            {
                case "Baby": await Idea.SetRatingBaby(reactionMsg.Id, rating); break;
                case "Item": await Idea.SetRatingItem(reactionMsg.Id, rating); break;
                case "ItemActive": await Idea.SetRatingItemActive(reactionMsg.Id, rating); break;
                case "Enemy": await Idea.SetRatingEnemy(reactionMsg.Id, rating); break;
            }
        }
    }
}
