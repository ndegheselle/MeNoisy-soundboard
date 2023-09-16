using MeNoisy_soundboard.App.Base;
using MeNoisy_soundboard.App.Pages.Sounds.Context;
using System.Windows;

namespace MeNoisy_soundboard.App.Pages.Sounds
{
    /// <summary>
    /// Logique d'interaction pour SoundsPage.xaml
    /// </summary>
    public partial class SoundsPage : BasePage<SoundsContext>
    {
        private IWindow _Window;

        public SoundsPage(IWindow window) : base(window)
        {
            InitializeComponent();
            _Window = window;
        }


        #region UI Events
        private void AddSound_Click(object sender, RoutedEventArgs e)
        {
            _Window.Push(new EditSound(_Window));
        }
        #endregion
    }
}
