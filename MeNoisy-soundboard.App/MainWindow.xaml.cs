using AdonisUI.Controls;
using MeNoisySoundboard.App.Base.UI;
using MeNoisySoundboard.App.Logic;
using MeNoisySoundboard.App.Logic.Sounds;
using MeNoisySoundboard.App.Pages.Sounds;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace MeNoisySoundboard.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : AdonisWindow, IApp, INotifyPropertyChanged
    {
        public GlobalContext GlobalContext { get; set; }
        public NavigationHandler Navigation { get; set; }

        public MainWindow()
        {
            GlobalContext = GlobalContext.Load();
            InitializeComponent();
            Navigation = new NavigationHandler(this, MainContainer);

            Navigation.Navigate<SoundsPage>(GlobalContext.SoundsContext);
            HotkeyHandler.OnGlobalHotkey += HotkeyHandler_OnGlobalHotkey;
        }

        private void HotkeyHandler_OnGlobalHotkey(HashSet<Key> keys)
        {
            var orderedKeys = keys.OrderByDescending(x => x);
            foreach (var sound in GlobalContext.SoundsContext.Sounds)
            {
                if (sound.Shortcut.Count <= 0) continue;
                if (orderedKeys.SequenceEqual(sound.Shortcut))
                {
                    if (sound.Player != null)
                    {
                        sound.Player.Stop();
                    }
                    sound.Player = new AudioPlayer(sound);
                    sound.Player.Play();
                }
            }
        }

        #region Save context

        public void SaveContext()
        {
            GlobalContext.Save();
        }

        #endregion

        #region UI Events

        private void NavigationBack_Click(object sender, RoutedEventArgs e)
        {
            Navigation.Pop(canceled:true);
        }

        #endregion
    }
}
