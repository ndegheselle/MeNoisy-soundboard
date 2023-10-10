using MeNoisySoundboard.App.Base;
using MeNoisySoundboard.App.Logic.Sounds;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MeNoisySoundboard.App.Contexts.Sounds
{
    public class Sound : ErrorValidationContext, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public Guid? Id { get; set; } = null;

        public string Name { get; set; }
        public float Volume { get; set; } = 1.0f;
        public string FilePath { get; set; }
        public ObservableCollection<Key> Shortcut { get; set; } = new ObservableCollection<Key>();
        public TimeSpan Duration { get; set; }

        private ObservableCollection<AudioPlayer> players { get; set; } = new ObservableCollection<AudioPlayer>();
        [JsonIgnore]
        public AudioPlayer? LastPlayer { get; set; }

        public void Play()
        {
            var audioPlayer = new AudioPlayer(this);
            LastPlayer = audioPlayer;
            players.Add(audioPlayer);
            LastPlayer.FinishedEvent += () =>
            {
                players.Remove(audioPlayer);
                if (players.Count == 0)
                    LastPlayer = null;
            };
            LastPlayer.Play();
        }
    }
}
