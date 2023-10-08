﻿using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MeNoisySoundboard.App.Base.UI
{
    public class BasePage<TContext> : BasePage where TContext : class, new()
    {
        public TContext? Context { get; set; }

        public override void Show(object contexte, object? parameters = null)
        {
            Context = (TContext)contexte;
            DataContext = Context;
        }
    }

    public abstract class BasePage : UserControl
    {
        protected IApp App { get; set; } = (IApp)Application.Current;

        public abstract void Show(object contexte, object? parameters = null);

        // Async to handle pages change animation for example
        public virtual async Task Hide(bool canceled)
        { }
    }
}
