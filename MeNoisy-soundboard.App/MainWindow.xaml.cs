using AdonisUI.Controls;
using MeNoisySoundboard.App.Base.UI;
using MeNoisySoundboard.App.Contexts;
using MeNoisySoundboard.App.Logic;
using MeNoisySoundboard.App.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MeNoisySoundboard.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : AdonisWindow, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public NavigationHandler Navigation { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Navigation = new NavigationHandler(MainContainer);
            Navigation.Navigate(new HomePage());


            Init();
        }

        public async void Init()
        {
            await webView.EnsureCoreWebView2Async();
            webView.CoreWebView2.OpenDevToolsWindow();
            await webView.CoreWebView2.Profile.SetPermissionStateAsync(
                Microsoft.Web.WebView2.Core.CoreWebView2PermissionKind.Microphone,
                "https://www.youtube.com/watch?v=6nlsdu6N5d8", 
                Microsoft.Web.WebView2.Core.CoreWebView2PermissionState.Allow);
            await webView.CoreWebView2.Profile.SetPermissionStateAsync(
                Microsoft.Web.WebView2.Core.CoreWebView2PermissionKind.Autoplay,
                "https://www.youtube.com/watch?v=6nlsdu6N5d8",
                Microsoft.Web.WebView2.Core.CoreWebView2PermissionState.Allow);
            await InitWebViewJs();
            await SetWebViewAudio(GlobalParamsProvider.Params.OutputDevice.Value);
        }

        private async Task SetWebViewAudio(string value)
        {
            await webView.CoreWebView2.ExecuteScriptAsync($"setSelectedAudioDeviceId('{value}')");
        }

        public async Task InitWebViewJs()
        {
            await webView.CoreWebView2.ExecuteScriptAsync(@"
window.audioOutputSelect = { deviceId: null, observer: null };
async function setSelectedAudioDeviceId(deviceName) {
    let devices = await navigator.mediaDevices.enumerateDevices()
    devices = devices.filter(x => x.kind == 'audiooutput' && x.deviceId != 'communications');

    console.log(devices, deviceName);
    let selectedDevice = devices.find(x => x.label.includes(deviceName));

    if (!selectedDevice) return;

    window.audioOutputSelect.deviceId = selectedDevice.deviceId || 'default';
    applyAudioDeviceToAll();
}

function applyAudioDeviceToAll()
{
    document.querySelectorAll('video').forEach(x => x.setSinkId(window.audioOutputSelect.deviceId));
    document.querySelectorAll('audio').forEach(x => x.setSinkId(window.audioOutputSelect.deviceId));   
}

function mutationCallback(mutationsList, observer) {
  mutationsList.forEach((mutation) => {
    if (mutation.type === 'childList') {
      mutation.addedNodes.forEach((node) => {
        if (node instanceof HTMLMediaElement) {
          node.setSinkId(window.audioOutputSelect.deviceId);
        }
      });
    }
  });
}

window.addEventListener('load', () => {
    if (!window.audioOutputSelect.observer) {
        window.audioOutputSelect.observer = new MutationObserver(mutationCallback);
        window.audioOutputSelect.observer.observe(document.body, {
            childList: true,
            subtree: true,
        });
applyAudioDeviceToAll();
    }
});
                ");
        }

        #region UI Events

        private void NavigationBack_Click(object sender, RoutedEventArgs e)
        {
            Navigation.Pop(canceled:true);
        }

        #endregion
    }
}
