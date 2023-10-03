using MeNoisySoundboard.App.Base.UI;
using MeNoisySoundboard.App.Logic.Sounds;
using MeNoisySoundboard.App.Logic.Sounds.Context;
using System.Numerics;
using System.Windows;

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
        }


        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            Sound sound = element.DataContext as Sound;

            if (sound == null) return;

            if (sound.Player == null)
            {
                AudioPlayer audioPlayer = new AudioPlayer(sound);
                sound.Player = audioPlayer;
                audioPlayer.FinishedEvent += () =>
                {
                    sound.Player = null;
                    audioPlayer.Dispose();
                };
            }
            sound.Player.Play();
        }


        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            Sound sound = element.DataContext as Sound;

            if (sound?.Player == null) return;

            sound.Player.Pause();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            Sound sound = element.DataContext as Sound;

            if (sound?.Player == null) return;

            sound.Player.Stop();
        }

        #endregion
    }
}
