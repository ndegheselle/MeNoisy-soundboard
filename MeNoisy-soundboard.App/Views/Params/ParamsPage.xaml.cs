using MeNoisySoundboard.App.Base.UI;
using MeNoisySoundboard.App.Contexts;
using NAudio.Wave;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace MeNoisySoundboard.App.Views.Params
{
    /// <summary>
    /// Logique d'interaction pour ParamsPage.xaml
    /// </summary>
    public partial class ParamsPage : BasePage<GlobalParams>
    {
        public ObservableCollection<string> Devices { get; set; } = new ObservableCollection<string>();

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
            GlobalParamsProvider.Save();
        }

        private void RefreshDevices()
        {
            Devices.Clear();
            Devices.Add("Default");
            for (int n = -1; n < WaveIn.DeviceCount; n++)
            {
                var capability = WaveOut.GetCapabilities(n);
                Devices.Add(capability.ProductName);
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
