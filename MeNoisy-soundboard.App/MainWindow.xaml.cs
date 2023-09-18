using AdonisUI.Controls;
using MeNoisy_soundboard.App.Base;
using MeNoisy_soundboard.App.Pages.Sounds;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        public bool CanGoBack { get; set; } = false;

        public MainWindow()
        {
            InitializeComponent();
            Navigate<SoundsPage>();
        }

        #region Navigation

        public void Navigate<TPage>(object? context = null, TPage ? page = null) where TPage : BasePage, new()
        {
            Stack.Clear();
            Push(context, page);
        }

        public void Push<TPage>(object? context = null, TPage? page = null) where TPage : BasePage, new()
        {
            page = page ?? new TPage();
            page.Show(this, context);
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
