using System.Windows.Controls;

namespace MeNoisy_soundboard.App.Pages._Base
{
    public class BasePage<TContext> : UserControl where TContext : new()
    {
        protected IWindow Window { get; set; }
        protected TContext Context { get; set; }

        public BasePage(IWindow window, TContext contexte)
        {
            Window = window;
            Context = contexte;
            this.DataContext = Context;
        }
        public BasePage(IWindow window) : this(window, new TContext())
        {}
    }
}
