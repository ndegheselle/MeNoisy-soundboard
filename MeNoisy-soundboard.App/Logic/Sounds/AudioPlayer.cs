using MeNoisySoundboard.App.Logic.Sounds.Context;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace MeNoisySoundboard.App.Logic.Sounds
{
    public class AudioPlayer : IDisposable, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public event Action? FinishedEvent;
        public bool IsPlaying { get; set; } = false;

        private Sound sound;

        private Timer timer;
        private WaveOutEvent waveOutDevice;
        private AudioFileReader audioFileReader;

        public TimeSpan TotalTime { get; private set; }
        public TimeSpan CurrentTime { get; private set; }

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

        // Bubble up
        private void WaveOutDevice_PlaybackStopped(object? sender, StoppedEventArgs e)
        {
            timer = null;
            FinishedEvent?.Invoke();
        }

        private void UpdateCurrentTime(object? sender, EventArgs e)
        {
            if (IsPlaying)
            {
                CurrentTime = audioFileReader.CurrentTime;
            }
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
    }
}
