using MeNoisySoundboard.App.Base;
using MeNoisySoundboard.App.Contexts.Sounds;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeNoisySoundboard.App.Contexts
{
    public class GlobalContextProvider
    {
        private static readonly Lazy<GlobalContext> lazy =
            new Lazy<GlobalContext>(() => Load() ?? new GlobalContext());

        public static GlobalContext Context { get { return lazy.Value; } }

        private GlobalContextProvider()
        {
        }

        public static GlobalContext? Load()
        {
            return JsonContextHandler.Load<GlobalContext>("data");
        }

        public static void Save()
        {
            if (Context == null) return;
            JsonContextHandler.Save(Context, "data");
        }
    }

    public class GlobalContext
    {
        public ObservableCollection<Sound> Sounds { get; set; } = new ObservableCollection<Sound>();
    }
}
