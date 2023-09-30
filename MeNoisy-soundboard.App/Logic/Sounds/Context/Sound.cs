using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MeNoisySoundboard.App.Logic.Sounds.Context
{
    public class Sound : ICloneable
    {
        public Guid? Id { get; set; } = null;

        public string Name { get; set; }
        public string FilePath { get; set; }
        public ObservableCollection<Key> Shortcut { get; set; } = new ObservableCollection<Key>();

        public TimeSpan Duration { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class SoundsContext
    {
        public ObservableCollection<Sound> Sounds { get; set; } = new ObservableCollection<Sound>();
    }
}
