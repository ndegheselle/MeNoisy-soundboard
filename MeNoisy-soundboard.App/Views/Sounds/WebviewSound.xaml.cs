using System.Windows;
using System.Windows.Controls;
using MeNoisySoundboard.App.Contexts;
using MeNoisySoundboard.App.Contexts.Sounds;
using Microsoft.Web.WebView2.WinForms;
using Microsoft.Web.WebView2.Wpf;
using System;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MeNoisySoundboard.App.Views.Sounds
{
    /// <summary>
    /// Logique d'interaction pour WebviewSound.xaml
    /// </summary>
    public partial class WebviewSound : UserControl
    {
        private WebSound _actualSound { get; set; }
        public WebSound ActualSound
        {
            get
            {
                return _actualSound;
            }
            set
            {
                _actualSound = value;
                this.DataContext = _actualSound;
            }
        }

        public WebviewSound()
        {
            App app = (App)Application.Current;
            app.WebviewSound = this;

            GlobalParamsProvider.Params.PropertyChanged += Params_PropertyChanged;
            InitializeComponent();
        }

        public async void NavigateToSound(WebSound sound)
        {
            ActualSound = sound;
            this.Visibility = System.Windows.Visibility.Visible;

            await Webview.EnsureCoreWebView2Async();
            Webview.CoreWebView2.Navigate(ActualSound.Uri.ToString());

            await Webview.CoreWebView2.Profile.SetPermissionStateAsync(
                Microsoft.Web.WebView2.Core.CoreWebView2PermissionKind.Microphone,
                ActualSound.Uri.ToString(),
                Microsoft.Web.WebView2.Core.CoreWebView2PermissionState.Allow);
            await Webview.CoreWebView2.Profile.SetPermissionStateAsync(
                Microsoft.Web.WebView2.Core.CoreWebView2PermissionKind.Autoplay,
                ActualSound.Uri.ToString(),
                Microsoft.Web.WebView2.Core.CoreWebView2PermissionState.Allow);
            await InitWebViewJs();
            await SetWebViewAudio(GlobalParamsProvider.Params.OutputDevice.Value);
        }

        #region Handle webview
        /// <summary>
        /// Set the audio output from the name (with appropriate permissions)
        /// </summary>
        /// <returns></returns>
        private async Task InitWebViewJs()
        {
            await Webview.CoreWebView2.ExecuteScriptAsync(@"
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

        private async Task SetWebViewAudio(string value)
        {
            await Webview.CoreWebView2.ExecuteScriptAsync($"setSelectedAudioDeviceId('{value}')");
        }

        #endregion

        private async void Params_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(GlobalParams.OutputDevice))
            {
                if (Webview.CoreWebView2 != null)
                    await SetWebViewAudio(GlobalParamsProvider.Params.OutputDevice.Value);
            }
        }

        private void StopButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            Webview.CoreWebView2.Navigate("about:blank");
            ActualSound = null;
        }
    }
}
