using AdonisUI.Controls;
using MeNoisy_soundboard.App.Base;
using MeNoisy_soundboard.App.Logic;
using MeNoisy_soundboard.App.Pages.Sounds;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace MeNoisy_soundboard.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : AdonisWindow, IWindow, INotifyPropertyChanged
    {
        public ObservableCollection<BasePage> Stack { get; set; } = new ObservableCollection<BasePage>();
        public GlobalContext GlobalContext { get; set; } = new GlobalContext();
        public bool CanGoBack { get; set; } = false;

        public MainWindow()
        {
            InitializeComponent();
            Navigate<SoundsPage>(GlobalContext.Sounds);
        }

        #region Navigation

        public void Navigate<TPage>(object context, object? parameters = null, TPage? page = null) where TPage : BasePage, new()
        {
            Stack.Clear();
            Push(context, parameters, page);
        }

        public void Push<TPage>(object context, object? parameters = null, TPage? page = null) where TPage : BasePage, new()
        {
            page ??= new TPage();
            page.Show(this, context, parameters);
            Stack.Add(page);
            MainContainer.Content = page;

            CanGoBack = Stack.Count > 1;
        }

        public void Pop()
        {
            Stack.RemoveAt(Stack.Count - 1);
            MainContainer.Content = Stack[Stack.Count - 1];
            CanGoBack = Stack.Count > 1;
        }

        #endregion

        #region UI Events
        private void NavigationBack_Click(object sender, RoutedEventArgs e)
        {
            Pop();
        }

        #endregion
    }
}
