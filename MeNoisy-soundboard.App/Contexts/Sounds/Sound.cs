using MeNoisySoundboard.App.Base;
using MeNoisySoundboard.App.Logic.Sounds;
using PropertyChanged;
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

        [JsonIgnore]
        public ObservableCollection<AudioPlayer> Players { get; set; } = new ObservableCollection<AudioPlayer>();
    }
}
