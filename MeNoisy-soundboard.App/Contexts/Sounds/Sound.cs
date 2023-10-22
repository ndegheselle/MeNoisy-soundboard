using MeNoisySoundboard.App.Base;
using MeNoisySoundboard.App.Logic.Sounds;
using NAudio.Wave;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MeNoisySoundboard.App.Contexts.Sounds
{
    public class WebSound : Sound
    {
        public Uri Uri { get; set; }

        public override void Play()
        {
            WebAudioPlayer webAudioPlayer = new WebAudioPlayer();
            webAudioPlayer.Play(this);
        }
    }

    public class FileSound : Sound
    {
        private string _filePath;
        public string FilePath
        {
            get { return _filePath; }
            set
            {
                _filePath = value;

                this.ClearErrors(nameof(FilePath));
                try
                {
                    AudioFileReader audioFileReader = new AudioFileReader(_filePath);
                    this.Duration = audioFileReader.TotalTime;
                }
                catch (COMException ex)
                {
                    // Show validation error
                    this.AddError(nameof(FilePath), "Invalid file type.");
                    return;
                }
            }
        }

        public float Volume { get; set; } = 1.0f;
        public TimeSpan Duration { get; private set; }

        [JsonIgnore]
        public ObservableCollection<AudioPlayer> Players { get; set; } = new ObservableCollection<AudioPlayer>();

        public override void Play()
        {
            var globalParams = GlobalParamsProvider.Params;

            // Stop previous sounds
            if (globalParams.PlaySoundsOnlyOnce && this.Players.Count > 0)
            {
                foreach (var player in this.Players)
                    player.Stop();
            }

            var audioPlayer = new AudioPlayer();
            this.Players.Add(audioPlayer);
            audioPlayer.FinishedEvent += () =>
            {
                this.Players.Remove(audioPlayer);
            };
            audioPlayer.Play(this);
        }
    }

    [JsonDerivedType(typeof(FileSound), typeDiscriminator: "File")]
    [JsonDerivedType(typeof(WebSound), typeDiscriminator: "Web")]
    public abstract class Sound : ErrorValidationContext, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public Guid? Id { get; set; } = null;
        public string Name { get; set; }
        public ObservableCollection<Key> Shortcut { get; set; } = new ObservableCollection<Key>();

        public abstract void Play();
    }
}
