using AdonisUI.Controls;
using MeNoisySoundboard.App.Base;
using MeNoisySoundboard.App.Logic;
using MeNoisySoundboard.App.Pages.Sounds;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace MeNoisySoundboard.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : AdonisWindow, IApp, INotifyPropertyChanged
    {
        public GlobalContext GlobalContext { get; set; }
        public NavigationHandler Navigation { get; set; }

        public MainWindow()
        {
            GlobalContext = GlobalContext.Load();
            InitializeComponent();
            Navigation = new NavigationHandler(this, MainContainer);

            Navigation.Navigate<SoundsPage>(GlobalContext.SoundsContext);
        }

        #region Save context

        public void SaveContext()
        {
            GlobalContext.Save();
        }

        #endregion

        #region UI Events
        private void NavigationBack_Click(object sender, RoutedEventArgs e)
        {
            Navigation.Pop();
        }
        #endregion
    }
}
