using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MeNoisySoundboard.App.Base.UI
{
    public abstract class BasePage : UserControl
    {
        // Should be a direct cast but thats break the designer
        protected IApp App { get; set; } = Application.Current as IApp;

        public virtual void OnShow() { }
        // return a Task so that the page can do things before hiding (like animations)
        public virtual Task OnHide(bool canceled) {
            return Task.CompletedTask;
        }
    }
}
