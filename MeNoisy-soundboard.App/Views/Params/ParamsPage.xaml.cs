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
    public partial class ParamsPage : BasePage
    {
        public ObservableCollection<string> Devices { get; set; } = new ObservableCollection<string>();
        public GlobalParams Context { get; set; }

        public ParamsPage(GlobalParams context)
        {
            Context = context;
            Context.PropertyChanged += Context_PropertyChanged;
            InitializeComponent();
            RefreshDevices();
        }

        ~ParamsPage()
        {
            Context.PropertyChanged -= Context_PropertyChanged;
        }

        private void Context_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            GlobalParamsProvider.Save();
        }

        private void RefreshDevices()
        {
            Devices.Clear();
            Devices.Add(GlobalParams.DEFAULT_DEVICE_NAME);
            for (int n = 0; n < WaveOut.DeviceCount; n++)
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
