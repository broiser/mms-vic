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
using System.Windows.Shapes;
using BytescoutImageToVideoLib;

namespace VideoFromImageCreator
{
    /// <summary>
    /// Interaction logic for AddPictureWindow.xaml
    /// </summary>
    public partial class AddPictureWindow : Window
    {
        private MainWindow window;
        public AddPictureWindow(MainWindow window)
        {
            InitializeComponent();
            this.window = window;
            setUpComboboxes();
        }

        private void Browse_Click(object sender, RoutedEventArgs e)
        {
             // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".txt";
            dlg.Filter = "All Files|*.*";

            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Set Filename
                this.picturePath.Text = dlg.FileName;
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string path = this.picturePath.Text;
            int dur = int.Parse(this.duration.Text);
            //TODO extract correct Effect
            TransitionEffectType inEffect = TransitionEffectType.teNone;
            TransitionEffectType outEffect = TransitionEffectType.teNone;

            Picture picture = new Picture(path, dur, inEffect, outEffect);
            window.AddPicture(picture);

            window.Visibility = Visibility.Visible;
            this.Visibility = Visibility.Collapsed;

        }

        private void setUpComboboxes()
        {
            setUpInTransitionCombobox();
            setUpOutTransitionCombobox();
        }

        private void setUpInTransitionCombobox()
        {
            //TODO add all possible effects to the Comboboxes
        }

        private void setUpOutTransitionCombobox()
        {
            //TODO add all possible effects to the Comboboxes
        }

    }
}
