using Discord;
using Discord.Interactions;

namespace Ramune.Modules.Test
{
    // Interaction modules must be public and inherit from an IInteractionModuleBase
    public class TestModule : InteractionModuleBase<SocketInteractionContext>
    {
        // Dependencies can be accessed through Property injection, public properties with public setters will be set by the service provider
        //public InteractionService Commands { get; set; }

        private InteractionHandler _handler;

        // Constructor injection is also a valid way to access the dependencies
        public TestModule(InteractionHandler handler)
        {
            _handler = handler;
        }

        // You can use a number of parameter types in you Slash Command handlers (string, int, double, bool, IUser, IChannel, IMentionable, IRole, Enums) by default. Optionally,
        // you can implement your own TypeConverters to support a wider range of parameter types. For more information, refer to the library documentation.
        // Optional method parameters(parameters with a default value) also will be displayed as optional on Discord.

        // [Summary] lets you customize the name and the description of a parameter
        [SlashCommand("echo", "Repeat the input")]
        public async Task Echo(string echo, [Summary(description: "mention the user")] bool mention = false)
            => await RespondAsync(echo + (mention ? Context.User.Mention : string.Empty));

        [SlashCommand("ping", "Pings the bot and returns its latency.")]
        public async Task GreetUserAsync()
            => await RespondAsync(text: $":ping_pong: It took me {Context.Client.Latency}ms to respond to you!", ephemeral: true);

        [SlashCommand("bitrate", "Gets the bitrate of a specific voice channel.")]
        public async Task GetBitrateAsync([ChannelTypes(ChannelType.Voice, ChannelType.Stage)] IChannel channel)
            => await RespondAsync(text: $"The voice channel {channel.Name} has a bitrate of {(channel as IVoiceChannel).Bitrate}");

        // This command will greet target user in the channel this was executed in.
        [UserCommand("greet")]
        public async Task GreetUserAsync(IUser user)
            => await RespondAsync(text: $":wave: {Context.User} said hi to you, <@{user.Id}>!");
    }
}
