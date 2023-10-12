using MeNoisySoundboard.App.Base.Helpers;
using MeNoisySoundboard.App.Base.UI;
using MeNoisySoundboard.App.Contexts;
using MeNoisySoundboard.App.Contexts.Sounds;
using MeNoisySoundboard.App.Logic.Sounds;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace MeNoisySoundboard.App.Views.Sounds
{
    /// <summary>
    /// Logique d'interaction pour EditSound.xaml
    /// </summary>
    public partial class EditSoundPage : BasePage
    {
        private Sound _originalSound;
        
        public GlobalContext Context { get; private set; }
        public Sound ActualSound { get; set; }

        public EditSoundPage(GlobalContext context, Sound actualSound)
        {
            Context = context;
            ActualSound = actualSound;

            if (ActualSound?.Id != null)
            {
                _originalSound = new Sound();
                ActualSound.CopyProperties(_originalSound);
            }
            this.DataContext = ActualSound;
            InitializeComponent();
        }

        public override Task OnHide(bool canceled)
        {
            if (!canceled || _originalSound == null) return Task.CompletedTask;
            // Revert back to old object
            _originalSound.CopyProperties(ActualSound);
            return Task.CompletedTask;
        }

        #region UI Events

        private void DeleteButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Context.Sounds.Remove(ActualSound);
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
           
            if (ActualSound.FilePath != _originalSound?.FilePath)
            {
                try
                {
                    AudioPlayer audioPlayer = new AudioPlayer(ActualSound);
                    ActualSound.Duration = audioPlayer.TotalTime;
                }
                catch (COMException ex)
                {
                    // Show validation error
                    ActualSound.AddError(nameof(Sound.FilePath), "Invalid file type.");
                    return;
                }
            }

            App.SaveContext();
            App.Navigation.Pop();
        }

        #endregion
    }
}
