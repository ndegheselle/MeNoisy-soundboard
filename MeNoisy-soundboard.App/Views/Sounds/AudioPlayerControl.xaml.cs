using MeNoisySoundboard.App.Logic.Sounds;
using System.Windows;
using System.Windows.Controls;

namespace MeNoisySoundboard.App.Views.Sounds
{
    /// <summary>
    /// Logique d'interaction pour SoundControlBar.xaml
    /// </summary>
    public partial class AudioPlayerControl : UserControl
    {
        #region Dependency properties

        public static readonly DependencyProperty PlayerProperty = DependencyProperty.Register(
            "Player", typeof(AudioPlayer), typeof(AudioPlayerControl), new FrameworkPropertyMetadata(null));
        public AudioPlayer Player
        {
            get => (AudioPlayer)GetValue(PlayerProperty);
            set => SetValue(PlayerProperty, value);
        }

        #endregion


        public AudioPlayerControl()
        {
            InitializeComponent();
        }

        #region UI events

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            Player.Stop();
            e.Handled = true;
        }

        #endregion
    }
}
