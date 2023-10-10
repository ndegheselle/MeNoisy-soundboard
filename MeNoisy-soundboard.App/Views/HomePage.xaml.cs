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
            InitializeComponent();
        }

        public override void Show(object contexte, object? parameters = null)
        {}

        #region UI Events
        private void AddSound_Click(object sender, RoutedEventArgs e)
        {
            App.Navigation.Push<EditSoundPage>(GlobalContextProvider.Context.Sounds, new Sound());
        }

        private void OpenParams_Click(object sender, RoutedEventArgs e)
        {
            App.Navigation.Push<ParamsPage>(GlobalParamsProvider.Params);
        }
        #endregion
    }
}
