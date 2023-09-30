using System.Threading.Tasks;
using System.Windows.Controls;

namespace MeNoisySoundboard.App.Base
{
    public class BasePage<TContext> : BasePage where TContext : class, new()
    {
        public TContext Context { get; set; }

        public override void Show(IApp app, object contexte, object? parameters = null)
        {
            base.Show(app, contexte, parameters);
            Context = contexte as TContext;
            this.DataContext = Context;
        }
    }

    public abstract class BasePage : UserControl
    {
        protected IApp App { get; set; }

        public virtual void Show(IApp app, object contexte, object? parameters = null)
        {
            App = app;
        }

        // Async to handle pages change animation for example
        public virtual async Task Hide()
        { }
    }
}
