using MeNoisySoundboard.App.Contexts.Sounds;
using MeNoisySoundboard.App.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeNoisySoundboard.App.Logic.Sounds
{
    public interface IAudioPlayer
    {
        void Play(Sound sound);
        void Stop();
    }

    public static class AudioPlayerHandler
    {
        public static IAudioPlayer Play(Sound sound)
        {
            var globalParams = GlobalParamsProvider.Params;

            // Stop previous sounds
            if (globalParams.PlaySoundsOnlyOnce && sound.Players.Count > 0)
            {
                foreach (var player in sound.Players)
                    player.Stop();
            }

            var audioPlayer = new AudioPlayer();
            sound.Players.Add(audioPlayer);
            audioPlayer.FinishedEvent += () =>
            {
                sound.Players.Remove(audioPlayer);
            };
            audioPlayer.Play(sound);

            return audioPlayer;
        }
    }
}
