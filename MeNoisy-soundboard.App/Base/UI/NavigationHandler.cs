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

        private readonly IApp _app;
        private readonly ContentControl _navigationContainer;

        public NavigationHandler(IApp app, ContentControl navigationContainer)
        {
            _app = app;
            _navigationContainer = navigationContainer;
        }


        public void Navigate<TPage>(object context, object? parameters = null, TPage? page = null) where TPage : BasePage, new()
        {
            Stack.Clear();
            Push(context, parameters, page);
        }

        public async void Push<TPage>(object context, object? parameters = null, TPage? page = null) where TPage : BasePage, new()
        {
            await HideActual(false);

            page ??= new TPage();
            page.Show(_app, context, parameters);
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
            await actualPage.Hide(canceled);

        }
    }
}
