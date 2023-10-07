using MeNoisySoundboard.App.Base.UI;
using MeNoisySoundboard.App.Logic.Sounds;
using MeNoisySoundboard.App.Logic.Sounds.Context;
using System;
using System.Diagnostics;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;

namespace MeNoisySoundboard.App.Pages.Sounds
{
    /// <summary>
    /// Logique d'interaction pour SoundsPage.xaml
    /// </summary>
    public partial class SoundsPage : BasePage<SoundsContext>
    {
        public SoundsPage()
        {
            InitializeComponent();
        }

        #region UI Events
        private void AddSound_Click(object sender, RoutedEventArgs e)
        {
            App.Navigation.Push<EditSound>(Context, new Sound());
        }


        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            Sound sound = element.DataContext as Sound;

            if (sound == null) return;

            App.Navigation.Push<EditSound>(Context, sound);
            e.Handled = true;
        }

        private void PlaySound(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            Sound sound = element.DataContext as Sound;
            if (sound == null) return;

            sound.Play();
        }
        #endregion

    }
}
