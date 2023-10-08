using MeNoisySoundboard.App.Base.Components;
using MeNoisySoundboard.App.Logic.Sounds;
using MeNoisySoundboard.App.Logic.Sounds.Context;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
