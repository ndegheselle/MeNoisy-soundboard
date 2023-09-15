using MeNoisy_soundboard.App.Pages._Base;
using MeNoisy_soundboard.App.Pages.Sounds.Context;

namespace MeNoisy_soundboard.App.Pages.Sounds
{
    /// <summary>
    /// Logique d'interaction pour SoundsPage.xaml
    /// </summary>
    public partial class SoundsPage : BasePage<SoundsContext>
    {
        public SoundsPage(IWindow window) : base(window)
        {
            InitializeComponent();
        }
    }
}
