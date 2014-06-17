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
        private const int DEFAULT_DURATION = 5000;
        public Picture Picture { get; set; }

        public AddPictureWindow()
        {
            InitializeComponent();
            setUpComboboxes();
        }
        public void Init(Picture picture)
        {
            Picture = picture;
            picturePath.Text = Picture.Path;
            duration.Text = Picture.Duration.ToString();
            inTransitionEffectCB.SelectedValue = Picture.InTransitionEffect;
            outTransitionEffectCB.SelectedValue = Picture.OutTransitionEffect;
            visualEffectCB.SelectedValue = Picture.visualEffectType;

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
            if (string.IsNullOrEmpty(path))
            {
                MessageBox.Show("Path doesn't accept empty!", "Error");
            }
            else if (!FileUtils.IsImage(path.ToLower()))
            {
                MessageBox.Show("Please select an image!", "Error");
            }
            else{
                int dur =  int.Parse(this.duration.Text);
                TransitionEffectType inEffect = (TransitionEffectType)inTransitionEffectCB.SelectedValue;
                TransitionEffectType outEffect = (TransitionEffectType)outTransitionEffectCB.SelectedValue;
                VisualEffectType visualEffectType = (VisualEffectType)visualEffectCB.SelectedValue;

                Picture = new Picture(path, dur, inEffect, outEffect, visualEffectType);
                DialogResult = true;
            }
        }

        private void setUpComboboxes()
        {
            setUpInTransitionCombobox();
            setUpOutTransitionCombobox();
            setUpVisualEffectTypeCombobox();
        }

        private void setUpInTransitionCombobox()
        {
            inTransitionEffectCB.Items.Add(TransitionEffectType.teNone);
            inTransitionEffectCB.Items.Add(TransitionEffectType.teRotate);
            inTransitionEffectCB.Items.Add(TransitionEffectType.teBoxIn);
            inTransitionEffectCB.Items.Add(TransitionEffectType.teCoverDown);
            inTransitionEffectCB.SelectedValue = TransitionEffectType.teNone;
        }

        private void setUpOutTransitionCombobox()
        {
            outTransitionEffectCB.Items.Add(TransitionEffectType.teNone);
            outTransitionEffectCB.Items.Add(TransitionEffectType.teRotate);
            outTransitionEffectCB.Items.Add(TransitionEffectType.teBoxIn);
            outTransitionEffectCB.Items.Add(TransitionEffectType.teCoverDown);
            outTransitionEffectCB.SelectedValue = TransitionEffectType.teNone;
        }

        private void setUpVisualEffectTypeCombobox()
        {
            visualEffectCB.Items.Add(VisualEffectType.veNone);
            visualEffectCB.Items.Add(VisualEffectType.veGrayscale);
            visualEffectCB.Items.Add(VisualEffectType.veSepia);
            visualEffectCB.SelectedValue = VisualEffectType.veNone;
        }

        private void duration_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

    }
}
