using MeNoisySoundboard.App.Base;
using MeNoisySoundboard.App.Logic.Sounds.Context;
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
            Window.Push<EditSound>(new Sound());
        }
        #endregion
    }
}
