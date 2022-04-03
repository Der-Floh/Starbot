// See https://aka.ms/new-console-template for more information
using System.Reflection;
using Discord;
using Discord.Interactions;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Starbot;
using Starbot.Modules;
using Starbot.Log;
using Discord_I.Rule_Suggestions_Bot;
using Starbot.Types;

//new DiscordBot().MainAsync().GetAwaiter().GetResult();

namespace SuggestionsBot
{
    class Program
    {
        private DiscordSocketClient _client;

        public static Task Main() => new Program().MainAsync();

        public async Task MainAsync()
        {
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
            if (!Directory.Exists(Directory.GetCurrentDirectory() + @"\Suggestion-Files"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\Suggestion-Files");
            }
            await Idea.InitIdea();

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json", false, true)
                .Build();

            using IHost host = Host.CreateDefaultBuilder()
                .ConfigureServices((_, services) =>
                services
                .AddSingleton(config)
                .AddSingleton(x => new DiscordSocketClient(new DiscordSocketConfig
                {
                    GatewayIntents = GatewayIntents.AllUnprivileged,
                    AlwaysDownloadUsers = true,
                }))
                .AddTransient<ConsoleLogger>()
                .AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>()))
                .AddSingleton<InteractionHandler>()
                .AddSingleton(x => new CommandService(new CommandServiceConfig
                {
                    LogLevel = LogSeverity.Debug,
                    DefaultRunMode = Discord.Commands.RunMode.Async
                }))
                .AddSingleton<PrefixHandler>()
                .AddSingleton<ReactionHandler>())
                .Build();

            await RunAsync(host);
        }

        public async Task RunAsync(IHost host)
        {
            using IServiceScope servicScope = host.Services.CreateScope();
            IServiceProvider provider = servicScope.ServiceProvider;

            var sCommands = provider.GetRequiredService<InteractionService>();
            _client = provider.GetRequiredService<DiscordSocketClient>();
            var config = provider.GetRequiredService<IConfigurationRoot>();

            await provider.GetRequiredService<InteractionHandler>().InitializeAsync();
            
            var pCommands = provider.GetRequiredService<PrefixHandler>();
            pCommands.AddModule<PrefixModule>();
            await pCommands.InitializeAsync();

            var reactions = provider.GetRequiredService<ReactionHandler>();
            reactions.InitializeAsync();

            //_client.Log += _ => provider.GetRequiredService<ConsoleLogger>().Log(_);
            _client.Log += LogAsync;
            //sCommands.Log += _ => provider.GetRequiredService<ConsoleLogger>().Log(_);
            sCommands.Log += LogAsync;

            _client.Ready += async () =>
            {
                if (IsDebug())
                {
                    await sCommands.RegisterCommandsToGuildAsync(UInt64.Parse(config["testGuild"]));
                }
                else
                {
                    await sCommands.RegisterCommandsGloballyAsync(true);
                }
            };
            await _client.LoginAsync(TokenType.Bot, config["token"]);
            await _client.StartAsync();

            /*
            if (System.Diagnostics.Debugger.IsAttached)
            {
                await _client.SetStatusAsync(UserStatus.Offline);
            }*/

            await Task.Delay(-1);
        }

        private async Task LogAsync(LogMessage msg)
        {
            switch (msg.Severity)
            {
                case LogSeverity.Critical:
                case LogSeverity.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogSeverity.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogSeverity.Info:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case LogSeverity.Verbose:
                case LogSeverity.Debug:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
            }
            Console.WriteLine(msg.ToString());
            Console.ResetColor();
        }
        private void OnProcessExit(object sender, EventArgs e)
        {
            Idea.WriteToJson();
        }

        static bool IsDebug()
        {
#if DEBUG
            return true;
#else
            return false;
#endif
        }
    }
}