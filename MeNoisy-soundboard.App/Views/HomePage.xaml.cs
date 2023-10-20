using MeNoisySoundboard.App.Base.UI;
using MeNoisySoundboard.App.Contexts;
using MeNoisySoundboard.App.Contexts.Sounds;
using MeNoisySoundboard.App.Views.Params;
using MeNoisySoundboard.App.Views.Sounds;
using System.Windows;

namespace MeNoisySoundboard.App.Views
{
    /// <summary>
    /// Logique d'interaction pour HomePage.xaml
    /// </summary>
    public partial class HomePage : BasePage
    {
        public HomePage()
        {
            this.DataContext = GlobalContextProvider.Context;
            InitializeComponent();
        }

        #region UI Events
        private void AddSound_Click(object sender, RoutedEventArgs e)
        {
            App.Navigation.Push(new EditSoundPage(GlobalContextProvider.Context, new FileSound()));
        }

        private void OpenParams_Click(object sender, RoutedEventArgs e)
        {
            App.Navigation.Push(new ParamsPage(GlobalParamsProvider.Params));
        }
        #endregion
    }
}
