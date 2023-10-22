using MeNoisySoundboard.App.Base.UI;
using MeNoisySoundboard.App.Contexts;
using MeNoisySoundboard.App.Contexts.Sounds;
using MeNoisySoundboard.App.Logic.Sounds;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
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
            "Sounds", typeof(ObservableCollection<Sound>), typeof(SoundsList), new FrameworkPropertyMetadata(null));
        public ObservableCollection<Sound> Sounds
        {
            get => (ObservableCollection<Sound>)GetValue(SoundsContextProperty);
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

        private void OpenLinkButton_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement? element = sender as FrameworkElement;
            WebSound? sound = element?.DataContext as WebSound;
            if (sound == null) return;

            OpenUrl(sound.Uri.ToString());
            
            e.Handled = true;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement? element = sender as FrameworkElement;
            Sound? sound = element?.DataContext as Sound;
            if (sound == null) return;

            App.Navigation.Push(new EditSoundPage(GlobalContextProvider.Context, sound));
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

        private void OpenUrl(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }

    }
}
