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
            App.Navigation.Push<EditSound>(Context, new Sound());
        }


        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            Sound sound = element.DataContext as Sound;

            if (sound == null) return;

            App.Navigation.Push<EditSound>(Context, sound);
        }

        #endregion
    }
}
