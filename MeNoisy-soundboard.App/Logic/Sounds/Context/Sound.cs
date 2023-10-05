using MeNoisySoundboard.App.Base;
using NAudio.Wave;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MeNoisySoundboard.App.Logic.Sounds.Context
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

        private AudioPlayer? _player;
        [JsonIgnore]
        public AudioPlayer? Player
        {
            get
            {
                return _player;
            }
            set
            {
                _player = value;
                if (_player == null) return;

                _player.FinishedEvent += () =>
                {
                    _player.Dispose();
                    _player = null;
                };
            }
        }
    }

    public class SoundsContext
    {
        public ObservableCollection<Sound> Sounds { get; set; } = new ObservableCollection<Sound>();
    }
}
