using MeNoisySoundboard.App.Base.UI;
using MeNoisySoundboard.App.Logic.Sounds;
using MeNoisySoundboard.App.Logic.Sounds.Context;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;

namespace MeNoisySoundboard.App.Views.Sounds
{
    /// <summary>
    /// Logique d'interaction pour SoundsPage.xaml
    /// </summary>
    public partial class SoundsList : UserControl
    {
        #region Dependency properties

        public static readonly DependencyProperty SoundsContextProperty = DependencyProperty.Register(
            "SoundsContext", typeof(SoundsContext), typeof(SoundsList), new FrameworkPropertyMetadata(null));
        public SoundsContext SoundsContext
        {
            get => (SoundsContext)GetValue(SoundsContextProperty);
            set => SetValue(SoundsContextProperty, value);
        }

        #endregion

        private readonly IApp App;

        public SoundsList()
        {
            App = Application.Current as IApp;
            InitializeComponent();
        }

        #region UI Events
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement? element = sender as FrameworkElement;
            Sound? sound = element?.DataContext as Sound;
            if (sound == null) return;

            App.Navigation.Push<EditSoundPage>(SoundsContext, sound);
            e.Handled = true;
        }

        private void PlaySound(object sender, RoutedEventArgs e)
        {
            FrameworkElement? element = sender as FrameworkElement;
            Sound? sound = element?.DataContext as Sound;
            if (sound == null) return;

            sound.Play();
        }
        #endregion

    }
}
