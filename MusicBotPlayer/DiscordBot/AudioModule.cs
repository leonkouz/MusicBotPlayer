using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicBotPlayer
{
    public class AudioModule : ModuleBase
    {
        [Command("join", RunMode = RunMode.Async)]
        public async Task JoinAudio(IVoiceChannel channel = null)
        {
            // Get the audio channel
            channel = channel ?? (Context.Message.Author as IGuildUser)?.VoiceChannel;

            if (channel == null)
            {
                await Context.Channel.SendMessageAsync("You must be in a voice channel.");
                return;
            }

            var userList = channel.GetUserAsync(307798617522044929);

            //if bot is not in channel, then join the channel
            if (userList.Result == null)
            {
                // For the next step with transmitting audio, you would want to pass this Audio Client in to a service.
                DiscordBot.AudioClient = await channel.ConnectAsync();
            }
            else
            {
                await Context.Channel.SendMessageAsync("Bot is already in a channel");
            }
        }

        [Command("play", RunMode = RunMode.Async)]
        public async Task PlayAudio(IVoiceChannel channel = null)
        {
            //await DiscordBot.SendAudioToVoice("test");
        }


        [Command("disconnect")]
        public async Task Disconnect(IVoiceChannel channel = null)
        {
            DiscordBot.AudioClient.Dispose();
        }
    }
}
