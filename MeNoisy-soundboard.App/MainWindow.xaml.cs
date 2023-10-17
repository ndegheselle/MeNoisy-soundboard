using AdonisUI.Controls;
using MeNoisySoundboard.App.Base.UI;
using MeNoisySoundboard.App.Logic;
using MeNoisySoundboard.App.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
            await webView.CoreWebView2.Profile.SetPermissionStateAsync(Microsoft.Web.WebView2.Core.CoreWebView2PermissionKind.OtherSensors, "https://www.microsoft.com", Microsoft.Web.WebView2.Core.CoreWebView2PermissionState.Allow);
            webView.CoreWebView2.PermissionRequested += CoreWebView2_PermissionRequested;
        }

        private void CoreWebView2_PermissionRequested(object? sender, Microsoft.Web.WebView2.Core.CoreWebView2PermissionRequestedEventArgs e)
        {
            throw new NotImplementedException();
        }

        #region UI Events

        private void NavigationBack_Click(object sender, RoutedEventArgs e)
        {
            Navigation.Pop(canceled:true);
        }

        #endregion
    }
}
