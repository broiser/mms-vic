using BytescoutImageToVideoLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VideoFromImageCreator
{
    public partial class MainWindow : Window
    {
        private const int DEFAULT_DURATION = 5000;
        private string productName = "Video From Image Creator";


        private List<Picture> previewPictures = new List<Picture>();
        private List<Picture> generatePictures = new List<Picture>();
       
        private Music music;

        public string FilePath { get; set; }
        public string FileType { get; set; }
        public string FileName { get; set; }
        public Dictionary<int, string> dirValues { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            MainWindowName.Title = productName;
            dirValues = new Dictionary<int,string>();
        }
 
        private void AddFile_Click(object sender, RoutedEventArgs e)
        {
            AddPictureWindow addPictureWindow = new AddPictureWindow();
            if (addPictureWindow.ShowDialog().Value)
            {
                AddPicture(addPictureWindow.Picture);
            }

        }
        private void EditPicture_Click(object sender, RoutedEventArgs e)
        {
            Picture picture = null;
            AddPictureWindow addPictureWindow = new AddPictureWindow();
            addPictureWindow.Picture = picture;
            if (addPictureWindow.ShowDialog().Value)
            {
                EditPicture(picture);
            }
        }

        private void AddFolder_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
           
            try
            {
                string[] files = Directory.GetFiles(fbd.SelectedPath);
                foreach(string file in files)
                {
                    if (FileUtils.IsImage(file))
                    {
                        AddPicture(new Picture(file, DEFAULT_DURATION, TransitionEffectType.teNone, TransitionEffectType.teNone, VisualEffectType.veNone));
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void AddTransitionEffect_Click(object sender, RoutedEventArgs e)
        {
        //    Picture p = (Picture)pictureGrid.SelectedItem;
        //    int position = pictureGrid.Items.IndexOf(pictureGrid.SelectedCells[0].Item);
            
        //    pictures.Remove(p);
        //    //p.InTransitionEffect= AUSWAHL
        //    pictures.Insert(position, p);
        }

        private void MoveUp_Click(object sender, RoutedEventArgs e)
        {
        //    Picture p = (Picture)pictureGrid.SelectedItem;
        //    int position = pictureGrid.Items.IndexOf(pictureGrid.SelectedCells[0].Item);
        //    if(position-1 >=0)
        //    {
        //        pictures.Remove(p);
        //        pictures.Insert(position - 1, p);
        //        this.pictureGrid.Items.Refresh();
        //    }

        }

        private void MoveDown_Click(object sender, RoutedEventArgs e)
        {
        //    Picture p = (Picture)pictureGrid.SelectedItem;
        //    int position = pictureGrid.Items.IndexOf(pictureGrid.SelectedCells[0].Item);
        //    if(position+1 < pictures.Count)
        //    {
        //        pictures.Remove(p);
        //        pictures.Insert(position + 1, p);
        //        this.pictureGrid.Items.Refresh();
        //    }

        }

        private void SetDuration_Click(object sender, RoutedEventArgs e)
        {
        //    Picture p = (Picture)pictureGrid.SelectedItem;

        //    if(p == null)
        //    {
        //        List<Picture> helpPictures = new List<Picture>();
                
        //        foreach (Picture pics in pictures)
        //        {
        //            pics.Duration=10;
        //            helpPictures.Add(pics);
        //        }
        //        pictures = helpPictures;
        //        this.pictureGrid.Items.Refresh();
        //    }

        }

        private void AddMusic_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".mp3";
            dlg.Filter = "Music Files|*.mp3";

            // Display OpenFileDialog by calling ShowDialog method
            DialogResult result = dlg.ShowDialog();

            // Set Filename
            this.music = new Music(dlg.FileName);

        }

        public void AddPicture(Picture picture)
        {
            this.previewPictures.Add(picture);
            dirValues.Add(dirValues.Count+1, picture.Path);
            ListBox.ItemsSource = dirValues;
            ListBox.Items.Refresh();
            //this.pictureGrid.Items.Refresh();
        }
        private void EditPicture(Picture picture)
        {
            dirValues.Select(i => i.Value == picture.Path);
        }

        private void GenerateVideo_Click(object sender, RoutedEventArgs e)
        {
            VideoBuilder builder = new VideoBuilder();
            foreach (Picture picture in generatePictures)
            {
                builder = builder.AddPicture(picture);
            }
            builder = builder.Height(800).Width(800);
            builder.AddMusic(music);
            builder.Build(FilePath +"\\" + FileName+"." + FileType);
        }

        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void MenuItemNew_Click(object sender, RoutedEventArgs e)
        {
            CreateProjectView CreateProjectView = new CreateProjectView();
            var result = CreateProjectView.ShowDialog();
            if (result.Value)
            {
                EnableComponents(result.Value);
                FileType = CreateProjectView.FileType;
                FileName = CreateProjectView.FileName;
                FilePath = CreateProjectView.FilePath;
                MainWindowName.Title = string.Format("Video From Image Creator - {0}", FileName);
            }
        }

        private void EnableComponents(bool result)
        {
            GenerateButton.IsEnabled = result;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ListBox_DragEnter(object sender, System.Windows.DragEventArgs e)
        {
            // GET ELEMENT AND ADD IN DragDropContainer
        }

        private void ListBox1_Drop(object sender, System.Windows.DragEventArgs e)
        {
            // DROP ELEMENT
        }
    }
}
