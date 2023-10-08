using MeNoisySoundboard.App.Base.Helpers;
using MeNoisySoundboard.App.Base.UI;
using MeNoisySoundboard.App.Logic.Sounds;
using MeNoisySoundboard.App.Logic.Sounds.Context;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace MeNoisySoundboard.App.Views.Sounds
{
    /// <summary>
    /// Logique d'interaction pour EditSound.xaml
    /// </summary>
    public partial class EditSoundPage : BasePage<SoundsContext>
    {
        private Sound OriginalSound;
        public Sound ActualSound { get; set; }

        public EditSoundPage()
        {
            InitializeComponent();
        }

        public override void Show(object contexte, object? parameters = null)
        {
            base.Show(contexte, parameters);
            ActualSound = parameters as Sound;

            if (ActualSound?.Id != null)
            {
                OriginalSound = new Sound();
                ActualSound.CopyProperties(OriginalSound);
            }
            this.DataContext = ActualSound;
        }

        public override async Task Hide(bool canceled)
        {
            if (!canceled || OriginalSound == null) return;
            // Revert back to old object
            OriginalSound.CopyProperties(ActualSound);
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
           
            if (ActualSound.FilePath != OriginalSound?.FilePath)
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
