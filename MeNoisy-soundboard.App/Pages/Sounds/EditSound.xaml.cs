using MeNoisySoundboard.App.Base;
using MeNoisySoundboard.App.Logic.Sounds.Context;
using System;
using System.Linq;

namespace MeNoisySoundboard.App.Pages.Sounds
{
    /// <summary>
    /// Logique d'interaction pour EditSound.xaml
    /// </summary>
    public partial class EditSound : BasePage<SoundsContext>
    {
        private Sound OrginalSound;
        public Sound ActualSound { get; set; }

        public EditSound()
        {
            InitializeComponent();
        }

        public override void Show(IApp app, object contexte, object? parameters = null)
        {
            base.Show(app, contexte, parameters);
            ActualSound = parameters as Sound;

            if (ActualSound?.Id != null) OrginalSound = ActualSound;
            this.DataContext = ActualSound;
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
            if (ActualSound.Id == null)
            {
                ActualSound.Id = Guid.NewGuid();
                Context.Sounds.Add(ActualSound);
            }

            App.SaveContext();
            App.Navigation.Pop();
        }

        #endregion
    }
}
