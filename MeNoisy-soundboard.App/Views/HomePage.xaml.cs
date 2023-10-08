using MeNoisySoundboard.App.Base.UI;
using MeNoisySoundboard.App.Logic;
using MeNoisySoundboard.App.Logic.Sounds.Context;
using MeNoisySoundboard.App.Views.Params;
using MeNoisySoundboard.App.Views.Sounds;
using System;
using System.Collections.Generic;
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

namespace MeNoisySoundboard.App.Views
{
    /// <summary>
    /// Logique d'interaction pour HomePage.xaml
    /// </summary>
    public partial class HomePage : BasePage<GlobalContext>
    {
        public HomePage()
        {
            InitializeComponent();
        }

        #region UI Events
        private void AddSound_Click(object sender, RoutedEventArgs e)
        {
            App.Navigation.Push<EditSoundPage>(Context.SoundsContext, new Sound());
        }

        private void OpenParams_Click(object sender, RoutedEventArgs e)
        {
            App.Navigation.Push<ParamsPage>(Context.ParamsContext);
        }
        #endregion
    }
}
