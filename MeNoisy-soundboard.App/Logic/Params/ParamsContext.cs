using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeNoisySoundboard.App.Logic.Params
{
    public class ParamsContext : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public string? DeviceName { get; set; } = null;
        public bool PlaySoundsOnlyOnce { get; set; } = true;
    }
}
