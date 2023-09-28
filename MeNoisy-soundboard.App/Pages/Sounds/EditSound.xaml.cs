using MeNoisySoundboard.App.Base;
using MeNoisySoundboard.App.Logic.Sounds.Context;
using System;

namespace MeNoisySoundboard.App.Pages.Sounds
{
    /// <summary>
    /// Logique d'interaction pour EditSound.xaml
    /// </summary>
    public partial class EditSound : BasePage<SoundsContext>
    {
        public Sound ActualSound { get; set; }

        public EditSound()
        {
            InitializeComponent();
        }

        public override void Show(IApp app, object contexte, object? parameters = null)
        {
            base.Show(app, contexte, parameters);
            ActualSound = parameters as Sound;

            // TODO : take a clone of the object since otherwise he will be modified even if you abort edit
            this.DataContext = ActualSound;
        }

        #region UI Events

        private void DeleteButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Context.Sounds.Remove(ActualSound);
            App.SaveContext();
            App.Pop();
        }

        private void SaveButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ActualSound.Id == null)
            {
                ActualSound.Id = Guid.NewGuid();
                Context.Sounds.Add(ActualSound);
            }

            App.SaveContext();
            App.Pop();
        }

        #endregion
    }
}
