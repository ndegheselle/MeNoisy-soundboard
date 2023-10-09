using MeNoisySoundboard.App.Base.UI;
using MeNoisySoundboard.App.Logic.Params;
using MeNoisySoundboard.App.Logic.Sounds.Context;
using MeNoisySoundboard.App.Views.Sounds;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace MeNoisySoundboard.App.Views.Params
{
    /// <summary>
    /// Logique d'interaction pour ParamsPage.xaml
    /// </summary>
    public partial class ParamsPage : BasePage<ParamsContext>
    {
        public ObservableCollection<WaveOutCapabilities> Devices { get; set; } = new ObservableCollection<WaveOutCapabilities>();

        public ParamsPage()
        {
            InitializeComponent();
            RefreshDevices();
        }

        public override void Show(object contexte, object? parameters = null)
        {
            base.Show(contexte, parameters);
            Context.PropertyChanged += Context_PropertyChanged;
        }

        public override Task Hide(bool canceled)
        {
            Context.PropertyChanged -= Context_PropertyChanged;
            return base.Hide(canceled);
        }

        private void Context_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            App.SaveContext();
        }

        private void RefreshDevices()
        {
            Devices.Clear();
            for (int n = -1; n < WaveIn.DeviceCount; n++)
            {
                Devices.Add(WaveOut.GetCapabilities(n));
            }
        }

        #region UI Events
        private void RefreshDevices_Click(object sender, RoutedEventArgs e)
        {
            RefreshDevices();
        }
        #endregion
    }
}
