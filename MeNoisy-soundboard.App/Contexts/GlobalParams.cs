using MeNoisySoundboard.App.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeNoisySoundboard.App.Contexts
{
    public class GlobalParamsProvider
    {
        private static readonly Lazy<GlobalParams> lazy =
            new Lazy<GlobalParams>(() => Load() ?? new GlobalParams());

        public static GlobalParams Params { get { return lazy.Value; } }


        public static GlobalParams? Load()
        {
            return JsonContextHandler.Load<GlobalParams>("params");
        }

        public static void Save()
        {
            if (Params == null) return;
            JsonContextHandler.Save(Params, "params");
        }
    }

    public class GlobalParams : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public bool PlaySoundsOnlyOnce { get; set; } = true;
        public string? DeviceName { get; set; } = null;
    }
}
