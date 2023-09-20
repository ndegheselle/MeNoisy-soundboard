using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeNoisy_soundboard.App.Logic.Sounds.Context
{
    public class Sound
    {
        public Guid? Id { get; set; } = null;

        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
        public string Shortcut { get; set; }
    }

    public interface ISoundsContext
    {
        ObservableCollection<Sound> Sounds { get; set; }
    }
}
