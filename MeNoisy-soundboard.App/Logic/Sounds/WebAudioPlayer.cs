using System.Windows;
using System.Windows.Controls;
using MeNoisySoundboard.App.Contexts.Sounds;

namespace MeNoisySoundboard.App.Logic.Sounds
{
    internal class WebAudioPlayer
    {
        internal void Play(WebSound webSource)
        {
            App app = (App)Application.Current;
            app.WebviewSound.NavigateToSound(webSource);
        }
    }
}
