using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ramune
{
    public class Ramune
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _services;

        private readonly DiscordSocketConfig _socketConfig = new()
        {
            GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.GuildMembers | GatewayIntents.MessageContent,
            AlwaysDownloadUsers = true,
        };

        public Ramune()
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("settings.json", optional: false)
                .Build();

            _services = new ServiceCollection()
                .AddSingleton(_configuration)
                .AddSingleton(_socketConfig)
                .AddSingleton(new CommandService(new CommandServiceConfig
                {
                    LogLevel = LogSeverity.Debug
                }))
                .AddSingleton<DiscordSocketClient>()
                .AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>()))
                .AddSingleton<InteractionHandler>()
                .BuildServiceProvider();
        }

        static void Main(string[] args)
            => new Ramune().RunAsync()
                .GetAwaiter()
                .GetResult();

        public async Task RunAsync()
        {
            var client = _services.GetRequiredService<DiscordSocketClient>();

            client.Log += LogAsync;

            // Here we can initialize the service that will register and execute our commands
            await _services.GetRequiredService<InteractionHandler>()
                .InitializeAsync();

            // Bot token can be provided from the Configuration object we set up earlier
            await client.LoginAsync(TokenType.Bot, _configuration["botToken"]);
            await client.StartAsync();

            // Never quit the program until manually forced to.
            await Task.Delay(Timeout.Infinite);
        }

        private async Task LogAsync(LogMessage message)
            => Console.WriteLine(message.ToString());

        public static bool IsDebug()
        {
#if DEBUG
            return true;
#else
            return false;
#endif
        }
    }
}