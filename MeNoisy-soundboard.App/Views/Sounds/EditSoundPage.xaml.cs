using MeNoisySoundboard.App.Base.Helpers;
using MeNoisySoundboard.App.Base.UI;
using MeNoisySoundboard.App.Contexts;
using MeNoisySoundboard.App.Contexts.Sounds;
using MeNoisySoundboard.App.Logic.Sounds;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Markup;

namespace MeNoisySoundboard.App.Views.Sounds
{
    /// <summary>
    /// Logique d'interaction pour EditSound.xaml
    /// </summary>
    public partial class EditSoundPage : BasePage
    {
        private Sound _originalSound;
        
        public GlobalContext Context { get; private set; }

        private Sound _actualSound;
        public Sound ActualSound
        {
            get
            {
                return _actualSound;
            }
            set
            {
                _actualSound = value;
                this.DataContext = _actualSound;
            }
        }

        public EditSoundPage(GlobalContext context, Sound actualSound)
        {
            Context = context;
            ActualSound = actualSound;
            InitializeComponent();

            if (ActualSound?.Id != null)
            {
                _originalSound = new FileSound();
                ActualSound.CopyPropertiesTo(_originalSound);
            }

        }

        public override Task OnHide(bool canceled)
        {
            if (!canceled || _originalSound == null) return Task.CompletedTask;
            // Revert back to old object
            _originalSound.CopyPropertiesTo(ActualSound);
            return Task.CompletedTask;
        }

        #region UI Events

        private void DeleteButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            foreach (var sound in Context.Sounds.Where(x => x.Id == ActualSound.Id))
                Context.Sounds.Remove(sound);

            App.SaveContext();
            App.Navigation.Pop();
        }

        private void SaveButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ActualSound.ClearErrors();

            if (ActualSound.Id == null)
            {
                ActualSound.Id = Guid.NewGuid();
                Context.Sounds.Add(ActualSound);
            }
           
            App.SaveContext();
            App.Navigation.Pop();
        }

        #endregion

        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            Type selectedType = comboBox.SelectedValue as Type;

            if (selectedType == null || selectedType == ActualSound.GetType()) return;

            Sound newSound = null;
            if (selectedType == typeof(WebSound))
                newSound = new WebSound();
            else
                newSound = new FileSound();

            ActualSound.CopyPropertiesTo(newSound);
            ActualSound = newSound;
        }
    }
}
