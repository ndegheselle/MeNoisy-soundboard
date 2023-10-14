using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MeNoisySoundboard.App.Base.UI
{
    public class NavigationHandler : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<BasePage> Stack { get; set; } = new ObservableCollection<BasePage>();
        public bool CanGoBack { get; set; } = false;

        private readonly ContentControl _navigationContainer;

        public NavigationHandler(ContentControl navigationContainer)
        {
            _navigationContainer = navigationContainer;
        }

        public void Navigate(BasePage page)
        {
            Stack.Clear();
            Push(page);
        }

        public async void Push(BasePage page)
        {
            await HideActual(false);
            page.OnShow();
            Stack.Add(page);
            _navigationContainer.Content = page;

            CanGoBack = Stack.Count > 1;
        }

        public async void Pop(bool canceled = false)
        {
            await HideActual(canceled);

            Stack.RemoveAt(Stack.Count - 1);
            _navigationContainer.Content = Stack[Stack.Count - 1];
            CanGoBack = Stack.Count > 1;
        }

        private async Task HideActual(bool canceled)
        {
            if (Stack.Count <= 0) return;

            var actualPage = Stack[Stack.Count - 1];
            await actualPage.OnHide(canceled);

        }
    }
}
