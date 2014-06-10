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
using System.Windows.Forms;
using System.IO;

namespace VideoFromImageCreator
{
    /// <summary>
    /// Interaction logic for CreateProjectView.xaml
    /// </summary>
    public partial class CreateProjectView : Window
    {
        public string FilePath { get; set; }
        public string FileType { get; set; }
        public string FileName { get; set; }

        public CreateProjectView()
        {
            InitializeComponent();
            SetComboBoxes();
        }

        private void SetComboBoxes()
        {
            SetUpFileTypeCombobox();
        }
        private void SetUpFileTypeCombobox()
        {
            this.filetypeCombobox.Items.Add(".avi");
            this.filetypeCombobox.Items.Add(".wmv");
            
        }

        private void ButtonFileDialog_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();
            PathTextbox.Text = folderBrowserDialog.SelectedPath;

            
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            FileName = FileNameTextBox.Text;
            FilePath = PathTextbox.Text;
            FileType = (string)filetypeCombobox.SelectedValue;
            if (IsValid())
            {
                this.DialogResult = true;
                this.Close();
            }
        }
        private bool IsValid()
        {
            if (!string.IsNullOrEmpty(FileName)
                && !string.IsNullOrEmpty(FilePath)
                && !string.IsNullOrEmpty(FileType))
            {
                return true;
            }
            System.Windows.Forms.MessageBox.Show("You haven't entered all required Fields!","Error");
            return false;
        }
    }
}
