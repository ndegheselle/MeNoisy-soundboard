using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeNoisy_soundboard.App.Pages.Sounds.Context
{
    public class Sound
    {
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
        public string Shortcut { get; set; }
    }
}
