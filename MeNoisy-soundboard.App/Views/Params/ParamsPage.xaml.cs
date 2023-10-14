using MeNoisySoundboard.App.Base.UI;
using MeNoisySoundboard.App.Contexts;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MeNoisySoundboard.App.Views.Params
{
    /// <summary>
    /// Logique d'interaction pour ParamsPage.xaml
    /// </summary>
    public partial class ParamsPage : BasePage
    {
        public ObservableCollection<KeyValuePair<string?, string>> Devices { get; set; } = new ObservableCollection<KeyValuePair<string?, string>>();
        public GlobalParams Context { get; set; }

        public ParamsPage(GlobalParams context)
        {
            InitializeComponent();
            RefreshDevices();
            Context = context;
            Context.PropertyChanged += Context_PropertyChanged;
            this.DataContext = Context;
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
            Devices.Add(new KeyValuePair<string?, string>(null, GlobalParams.DEFAULT_DEVICE_NAME));

            MMDeviceEnumerator MMDE = new MMDeviceEnumerator();
            MMDeviceCollection allDevices = MMDE.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);
            foreach (MMDevice device in allDevices)
            {
                Devices.Add(new KeyValuePair<string?, string>(device.ID, device.FriendlyName));
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
