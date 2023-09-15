using AdonisUI.Controls;
using MeNoisy_soundboard.App.Pages._Base;
using MeNoisy_soundboard.App.Pages.Sounds;

namespace MeNoisy_soundboard.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : AdonisWindow, IWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            MainContainer.Content = new SoundsPage(this);
        }
    }
}
