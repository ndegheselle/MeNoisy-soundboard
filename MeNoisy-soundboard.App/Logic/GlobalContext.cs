using MeNoisy_soundboard.App.Logic.Sounds.Context;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeNoisy_soundboard.App.Logic
{
    public class GlobalContext : ISoundsContext
    {
        public ObservableCollection<Sound> Sounds { get; set; } = new ObservableCollection<Sound>()
        {
            new Sound()
            {
                Name = "Testing",
                Duration = TimeSpan.FromSeconds(3),
                Shortcut = "CTRL + 8"
            },
            new Sound()
            {
                Name = "Testing",
                Duration = TimeSpan.FromSeconds(3),
                Shortcut = "CTRL + 8"
            }
        };
    }
}
