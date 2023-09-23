using System.Windows.Controls;

namespace MeNoisySoundboard.App.Base
{
    public class BasePage<TContext> : BasePage where TContext : class, new()
    {
        public TContext Context { get; set; }

        public override void Show(IWindow window, object contexte, object? parameters = null)
        {
            base.Show(window, contexte, parameters);
            Context = contexte as TContext ?? new TContext();
            this.DataContext = Context;
        }
    }

    public abstract class BasePage : UserControl
    {
        protected IWindow Window { get; set; }

        public virtual void Show(IWindow window, object contexte, object? parameters = null)
        {
            Window = window;
        }
    }
}
