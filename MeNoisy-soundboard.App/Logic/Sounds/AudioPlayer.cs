using MeNoisySoundboard.App.Contexts;
using MeNoisySoundboard.App.Contexts.Sounds;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace MeNoisySoundboard.App.Logic.Sounds
{
    public static class AudioPlayerHandler
    {
        public static AudioPlayer Play(Sound sound)
        {
            var globalParams = GlobalParamsProvider.Params;

            // Stop previous sounds
            if (globalParams.PlaySoundsOnlyOnce && sound.Players.Count > 0)
            {
                foreach(var player in sound.Players)
                    player.Stop();
            }

            var audioPlayer = new AudioPlayer(sound);
            sound.Players.Add(audioPlayer);
            audioPlayer.FinishedEvent += () =>
            {
                sound.Players.Remove(audioPlayer);
            };
            audioPlayer.Play();

            return audioPlayer;
        }
    }

    public class AudioPlayer : IDisposable, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public event Action? FinishedEvent;
        public bool IsPlaying { get; set; } = false;

        public TimeSpan TotalTime { get; private set; }
        public TimeSpan CurrentTime { get; private set; }

        private Sound sound;
        private Timer? timer;
        private WaveOutEvent? waveOutDevice;
        private AudioFileReader audioFileReader;

        public AudioPlayer(Sound _sound)
        {
            sound = _sound;
            audioFileReader = new AudioFileReader(sound.FilePath);
            audioFileReader.Volume = sound.Volume;

            CurrentTime = audioFileReader.CurrentTime;
            TotalTime = audioFileReader.TotalTime;
        }

        public void Play()
        {
            if (waveOutDevice == null)
            {
                waveOutDevice = new WaveOutEvent();
                waveOutDevice.DeviceNumber = GetDeviceNumber();
                waveOutDevice.Init(audioFileReader);
                waveOutDevice.PlaybackStopped += WaveOutDevice_PlaybackStopped;

                timer = new Timer();
                timer.Tick += new EventHandler(UpdateCurrentTime);
                timer.Interval = 100;
                timer.Start();
            }

            waveOutDevice.Play();
            IsPlaying = true;
        }

        public void Pause()
        {
            waveOutDevice.Pause();
            IsPlaying = false;
        }

        public void Stop()
        {
            waveOutDevice.Stop();
            IsPlaying = false;
        }

        public void Dispose()
        {
            IsPlaying = false;
            FinishedEvent = null;
            waveOutDevice.Dispose();
            audioFileReader.Dispose();
        }


        // Bubble up
        private void WaveOutDevice_PlaybackStopped(object? sender, StoppedEventArgs e)
        {
            timer = null;
            FinishedEvent?.Invoke();
            Dispose();
        }

        private void UpdateCurrentTime(object? sender, EventArgs e)
        {
            if (IsPlaying)
            {
                CurrentTime = audioFileReader.CurrentTime;
            }
        }

        private int GetDeviceNumber()
        {
            var globalParams = GlobalParamsProvider.Params;
            if (string.IsNullOrEmpty(globalParams.DeviceName) || globalParams.DeviceName == GlobalParams.DEFAULT_DEVICE_NAME) return -1;

            for (int n = 0; n < WaveOut.DeviceCount; n++)
            {
                var capability = WaveOut.GetCapabilities(n);
                if (capability.ProductName == globalParams.DeviceName) return n;
            }

            return -1;
        }
    }
}
