using Discord;
using Discord.Audio;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MusicBotPlayer
{
    public class DiscordBot
    {
        private static QueueViewModel queueViewModel { get; set; }

        /// <summary>
        /// Specifies if the bot is connected to a voice channel.
        /// </summary>
        public static bool IsConnectedToVoiceChannel { get; set; } = false;

        /// <summary>
        /// The currently connected voice channel.
        /// </summary>
        public static IVoiceChannel ConnectedChannel { get; set; } = null;

        /// <summary>
        /// The Audio Channel.
        /// </summary>
        public static IAudioClient AudioClient { get; set; } = null;

        /// <summary>
        /// Fires when track has finished playing.
        /// </summary>
        public static event EventHandler TrackFinishedPlaying;


        public static bool IsTransitioningToNextTrack = false;

        private static CommandService commands;
        private static IServiceProvider services;
        private static DiscordSocketClient client;

        private static CancellationTokenSource cts = new CancellationTokenSource();

        /// <summary>
        /// Initialise the bot and login.
        /// </summary>
        public static void InitialiseBot(QueueViewModel queueViewModel)
        {
            DiscordBot.queueViewModel = queueViewModel;
            Initialise().GetAwaiter().GetResult();
        }

        /// <summary>
        /// Run initialisation process.
        /// </summary>
        /// <returns></returns>
        private static async Task Initialise()
        {
            client = new DiscordSocketClient();
            commands = new CommandService();

            services = new ServiceCollection()
                .BuildServiceProvider();

            await InstallCommands();

            string token = ApiKeys.GetDiscordBotTokenFromAppData();
            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();

            client.MessageReceived += MessageReceived;

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        /// <summary>
        /// Install the command module.
        /// </summary>
        /// <returns></returns>
        public static async Task InstallCommands()
        {
            // Hook the MessageReceived Event into our Command Handler
            client.MessageReceived += HandleCommand;
            // Discover all of the commands in this assembly and load them.
            await commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        /// <summary>
        /// Handles commands.
        /// </summary>
        /// <param name="messageParam"></param>
        /// <returns></returns>
        public static async Task HandleCommand(SocketMessage messageParam)
        {
            // Don't process the command if it was a System Message
            var message = messageParam as SocketUserMessage;
            if (message == null) return;
            // Create a number to track where the prefix ends and the command begins
            int argPos = 0;
            // Determine if the message is a command, based on if it starts with '!' or a mention prefix
            if (!(message.HasCharPrefix('!', ref argPos) || message.HasMentionPrefix(client.CurrentUser, ref argPos))) return;
            // Create a Command Context
            var context = new CommandContext(client, message);
            // Execute the command. (result does not indicate a return value, 
            // rather an object stating if the command executed successfully)
            var result = await commands.ExecuteAsync(context, argPos, services);
            if (!result.IsSuccess)
                await context.Channel.SendMessageAsync(result.ErrorReason);
        }

        /// <summary>
        /// Fires when a message is received in Discord.
        /// </summary>
        /// <param name="message">The message received.</param>
        /// <returns></returns>
        private static async Task MessageReceived(SocketMessage message)
        {
        }

        /// <summary>
        /// Initialise the FFMPEG stream.
        /// </summary>
        /// <param name="songPath">The path to the song.</param>
        /// <returns></returns>
        private static Process InitialiseAudioStream(string songPath)
        {
            var process = Process.Start(new ProcessStartInfo
            {
                // FFmpeg requires us to spawn a process and hook into its stdout, so we will create a Process
                FileName = @"C:\Users\Leon\Source\Repos\MusicBotPlayer\MusicBotPlayer\ffmpeg.exe",
                Arguments = $"-i {songPath} " + // Here we provide a list of arguments to feed into FFmpeg. -i means the location of the file/URL it will read from
                           "-f s16le -stats -ar 48000 -ac 2 pipe:1", // Next, we tell it to output 16-bit 48000Hz PCM, over 2 channels, to stdout.
                UseShellExecute = false,
                RedirectStandardOutput = true, // Capture the stdout of the process
                RedirectStandardError = true,
                CreateNoWindow = true,
            });

            process.ErrorDataReceived += Process_ErrorDataReceived;

            process.BeginErrorReadLine();

            return process;
        }

        /// <summary>
        /// Fires when error data is received by ffmpeg. Stores the current point in time of the currently playing track.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null && e.Data.Contains("Duration"))
            {
                string[] data = e.Data.Split(' ');

                // Get the duration of the track and trim the comma off the end.
                string duration = data[3].TrimEnd(',');

                TimeSpan timeSpan;
                TimeSpan.TryParse(duration, out timeSpan);

                App.Current.Dispatcher.Invoke(() =>
                {
                    queueViewModel.TrackDuration = timeSpan;
                });
            }

            if (e.Data != null && e.Data.Contains("time="))
            {
                string[] data = e.Data.Split(' ');

                string time = data.Where(x => x.Contains("time=")).First().Substring(5);

                TimeSpan timeSpan;
                TimeSpan.TryParse(time, out timeSpan);

                App.Current.Dispatcher.Invoke(() =>
                {
                    queueViewModel.CurrentPointInTrack = timeSpan;
                });
            }
        }

        /// <summary>
        /// Stop the discord bot playing the current song.
        /// </summary>
        public static Task StopPlaying()
        {
            cts.Cancel();
            cts = new CancellationTokenSource();

            return Task.CompletedTask;
        }

        /// <summary>
        /// Sends audio to the discord voice channel.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static async Task SendAudioToVoice(string pathOrUrl)
        {
            int blockSize = 3840; // The size of bytes to read per frame; 1920 for mono
            byte[] buffer = new byte[blockSize];
            int byteCount;
            AudioOutStream discord;

            try
            {
                discord = AudioClient.CreatePCMStream(AudioApplication.Mixed);
            }
            catch
            {
                IsTransitioningToNextTrack = false;
                throw new DiscordBotNotConnectedException("Unable to create PCM stream as discord bot is not connected to a voice channel. Connect the bot first.");
            }

            var ffmpeg = InitialiseAudioStream(pathOrUrl);

            IsTransitioningToNextTrack = false;

            try
            {
                while ((byteCount = await ffmpeg.StandardOutput.BaseStream.ReadAsync(buffer, 0, buffer.Length, cts.Token)) > 0)
                {
                    await ffmpeg.StandardOutput.BaseStream.CopyToAsync(discord, buffer.Length, cts.Token);
                    await discord.FlushAsync();
                }
            }
            catch (OperationCanceledException opCanceledException)
            {
                await discord.FlushAsync();
            }

            ffmpeg.Close();
            ffmpeg.CloseMainWindow();
            ffmpeg.ErrorDataReceived -= Process_ErrorDataReceived;

            OnTrackFinishedPlaying(EventArgs.Empty);
        }

        /// <summary>
        /// Play the track.
        /// </summary>
        /// <param name="track">The track to play.</param>
        public static async Task Play(QueueTrack track)
        {
            IsTransitioningToNextTrack = true;

            string artists = StringHelper.ArrayToString(track.Artists);

            string url = YoutubeApi.YoutubeSearch(track.Name + artists);

            string videoUrl = LibVideo.GetLink(url);

            await SendAudioToVoice(videoUrl);
        }

        /// <summary>
        /// Fires the TrackFinishedPlaying event.
        /// </summary>
        /// <param name="e"></param>
        private static void OnTrackFinishedPlaying(EventArgs e)
        {
            TrackFinishedPlaying?.Invoke(null, e);
        }
    }
}
