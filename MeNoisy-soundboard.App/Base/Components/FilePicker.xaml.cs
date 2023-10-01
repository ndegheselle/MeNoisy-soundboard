using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MeNoisySoundboard.App.Base.Components
{
    /* XXX : Will have to check for proper handling of INotifyDataErrorInfo
     * Currently the default style is applied to the whole UserControl, ideally it should go on the TextBox in order to exploit the existing adonis ErrorTemplate
     * Using Validation.ValidationAdornerSiteFor on the TextBox work but is not compatible with an ErrorTemplate (which is all the point)
     * Possible solution : https://stackoverflow.com/questions/60711962/how-to-pass-validation-error-to-child-control-in-reusable-usercontrol
     * 
     * Ajoute juste des styles + un border, a check comment je peux virer le style cela dit
     */

    /// <summary>
    /// Logique d'interaction pour FilePicker.
    /// </summary>
    public partial class FilePicker : UserControl
    {

        #region Dependency properties

        public static readonly DependencyProperty SelectedFilePathProperty = DependencyProperty.Register(
            "SelectedFilePath", typeof(string), typeof(FilePicker), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string SelectedFilePath
        {
            get => (string)GetValue(SelectedFilePathProperty);
            set => SetValue(SelectedFilePathProperty, value);
        }

        #endregion

        public FilePicker()
        {
            InitializeComponent();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            SelectedFilePath = "";
        }

        private void OpenPicker_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                SelectedFilePath = openFileDialog.FileName;
        }
    }
}
