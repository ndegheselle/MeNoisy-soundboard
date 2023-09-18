using System.Windows.Controls;

namespace MeNoisy_soundboard.App.Base
{
    public class BasePage<TContext> : BasePage where TContext : class, new()
    {
        public TContext Context { get; set; }

        public override void Show(IWindow window, object? contexte = null)
        {
            base.Show(window, contexte);
            Context = contexte as TContext ?? new TContext();
            this.DataContext = Context;
        }
    }

    public abstract class BasePage : UserControl
    {
        protected IWindow Window { get; set; }

        public virtual void Show(IWindow window, object? contexte = null)
        {
            Window = window;
        }
    }
}
