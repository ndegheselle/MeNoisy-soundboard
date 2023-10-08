using MeNoisySoundboard.App.Base.UI;
using MeNoisySoundboard.App.Logic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MeNoisySoundboard.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, IApp
    {
        public GlobalContext GlobalContext { get; set; }
        public NavigationHandler Navigation
        {
            get
            {
                return ((MainWindow)MainWindow).Navigation;
            }
        }

        public void SaveContext()
        {
            GlobalContext.Save();
        }

        #region Lifcycle
        protected override void OnStartup(StartupEventArgs e)
        {
            Exit += App_Exit;
            base.OnStartup(e);

            GlobalContext = GlobalContext.Load();
            HotkeyHandler.SetHook();
            HotkeyHandler.OnGlobalHotkey += HotkeyHandler_OnGlobalHotkey;
        }

        private void App_Exit(object sender, ExitEventArgs e)
        {
            HotkeyHandler.UnHook();
        }
        #endregion

        private void HotkeyHandler_OnGlobalHotkey(HashSet<Key> keys)
        {
            var orderedKeys = keys.OrderByDescending(x => x);
            foreach (var sound in GlobalContext.SoundsContext.Sounds)
            {
                if (sound.Shortcut.Count <= 0) continue;
                if (orderedKeys.SequenceEqual(sound.Shortcut))
                {
                    sound.Play();
                }
            }
        }
    }
}
