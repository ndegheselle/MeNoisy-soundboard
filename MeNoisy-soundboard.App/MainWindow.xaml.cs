using AdonisUI.Controls;
using MeNoisy_soundboard.App.Base;
using MeNoisy_soundboard.App.Pages.Sounds;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Documents;

namespace MeNoisy_soundboard.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : AdonisWindow, IWindow
    {
        public FrameworkElement CurrentRootPage { get; set; }
        public ObservableCollection<FrameworkElement> Stack { get; set; } = new ObservableCollection<FrameworkElement>();

        public MainWindow()
        {
            InitializeComponent();
            Navigate(new SoundsPage(this));
        }

        #region Navigation

        // XXX : Maybe pass the window there ?
        public void Navigate(FrameworkElement page)
        {
            Stack.Clear();
            CurrentRootPage = page;
            MainContainer.Content = page;
        }

        public void Push(FrameworkElement page)
        {
            Stack.Add(page);
            MainContainer.Content = page;
        }

        public void Pop()
        {
            Stack.RemoveAt(Stack.Count - 1);
            if (Stack.Count <= 0)
            {
                MainContainer.Content = CurrentRootPage;
            }
            else
            {
                MainContainer.Content = Stack[Stack.Count];
            }
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
