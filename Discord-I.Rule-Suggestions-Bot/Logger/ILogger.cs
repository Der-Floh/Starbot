using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_I.Rule_Suggestions_Bot.Log
{
    public interface ILogger
    {
        public Task Log(LogMessage message);
    }
}
