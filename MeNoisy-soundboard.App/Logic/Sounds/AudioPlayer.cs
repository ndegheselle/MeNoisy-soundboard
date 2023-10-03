using MeNoisySoundboard.App.Logic.Sounds.Context;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MeNoisySoundboard.App.Logic.Sounds
{
    public class AudioPlayer : IDisposable, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public event Action? FinishedEvent;
        public bool IsPlaying { get; set; } = false;

        private Sound sound;
        private WaveOutEvent waveOutDevice;
        private AudioFileReader audioFileReader;

        public TimeSpan TotalTime
        {
            get
            {
                return audioFileReader.TotalTime;
            }
        }

        public AudioPlayer(Sound _sound)
        {
            sound = _sound;
            audioFileReader = new AudioFileReader(sound.FilePath);
            audioFileReader.Volume = sound.Volume;
        }

        public void Play()
        {
            if (waveOutDevice == null)
            {
                waveOutDevice = new WaveOutEvent();
                waveOutDevice.Init(audioFileReader);
                waveOutDevice.PlaybackStopped += WaveOutDevice_PlaybackStopped;
            }

            waveOutDevice.Play();
            IsPlaying = true;
        }

        private void WaveOutDevice_PlaybackStopped(object? sender, StoppedEventArgs e)
        {
            FinishedEvent.Invoke();
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
            FinishedEvent = null;
            waveOutDevice.Dispose();
            audioFileReader.Dispose();
        }
    }
}
