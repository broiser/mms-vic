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
            btnAdd.IsEnabled = false;
        }
        public void Init(Picture picture)
        {
            Picture = picture;
            picturePath.Text = Picture.Path;
            duration.Text = Picture.Duration.ToString();

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
                btnAdd.IsEnabled = true;
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string path = this.picturePath.Text;
            if (IsValid(path))
            {
                int dur = 0;
                try
                {
                    dur = int.Parse(this.duration.Text);
                }
                catch (Exception)
                {
                    dur = DEFAULT_DURATION;
                }

                //TODO extract correct Effect
                TransitionEffectType inEffect = (TransitionEffectType)inTransitionEffectCB.SelectedValue;
                TransitionEffectType outEffect = TransitionEffectType.teNone;
                VisualEffectType visualEffectType = VisualEffectType.veNone;

                Picture = new Picture(path, dur, inEffect, outEffect, visualEffectType);
                DialogResult = true;
            }


        }
        private bool IsValid(string path) {
            
            if (!string.IsNullOrEmpty(path))
            {
                return true;
            }
            MessageBox.Show("Path doesn't accept empty!","Error");
            //PATH darf nicht null sein!! TODO
            return false;
        }

        private void setUpComboboxes()
        {
            setUpInTransitionCombobox();
            setUpOutTransitionCombobox();
        }

        private void setUpInTransitionCombobox()
        {
            inTransitionEffectCB.Items.Add(TransitionEffectType.teNone);
            inTransitionEffectCB.Items.Add(TransitionEffectType.teBoxIn);
            inTransitionEffectCB.Items.Add(TransitionEffectType.teBoxOut);
            inTransitionEffectCB.Items.Add(TransitionEffectType.teCoverDown);

            inTransitionEffectCB.SelectedValue = TransitionEffectType.teNone; 
        }

        private void setUpOutTransitionCombobox()
        {
            //TODO add all possible effects to the Comboboxes
        }

        private void setUpVisualEffectTypeCombobox()
        {
            //TODO add all visual effects to comboBox
        }

    }
}
