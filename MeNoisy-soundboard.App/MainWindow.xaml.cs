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
        }


        #region UI Events

        private void NavigationBack_Click(object sender, RoutedEventArgs e)
        {
            Navigation.Pop(canceled:true);
        }

        #endregion
    }
}
