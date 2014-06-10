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
        //private const string RESULT = "result.wmv";
        private const int DEFAULT_DURATION = 5000;


        private List<Picture> pictures = new List<Picture>();
        private Music music = new Music("");
        private SlideConfiguration configuration = new SlideConfiguration(4);
        private string productName = "Video From Image Creator";

        public string FilePath { get; set; }
        public string FileType { get; set; }
        public string FileName { get; set; }


        public MainWindow()
        {
            InitializeComponent();
            MainWindowName.Title = productName;
            InitializePictureGrid();
        }

        private void InitializePictureGrid()
        {
            pictureGrid.ItemsSource = pictures;
        }

        private void AddFile_Click(object sender, RoutedEventArgs e)
        {
            AddPictureWindow addPictureWindow = new AddPictureWindow();
            if (addPictureWindow.ShowDialog().Value)
            {
                AddPicture(addPictureWindow.Picture);
            }

        }

        private void AddFolder_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            string[] files;
            try
            {
                files = Directory.GetFiles(fbd.SelectedPath);
                for (int i = 0; i < files.Length; i++)
                {
                    if (files[i].EndsWith(".jpg") || files[i].EndsWith(".jpeg") || files[i].EndsWith(".png") || files[i].EndsWith(".gif") || files[i].EndsWith(".bmp"))
                    {
                        string path = files[i];
                        int dur = DEFAULT_DURATION;
                        //TODO extract correct Effect
                        TransitionEffectType inEffect = TransitionEffectType.teNone;
                        TransitionEffectType outEffect = TransitionEffectType.teNone;
                        VisualEffectType visualEffectType = VisualEffectType.veNone;

                        Picture p = new Picture(path, dur, inEffect, outEffect, visualEffectType);
                        AddPicture(p);

                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void AddTransitionEffect_Click(object sender, RoutedEventArgs e)
        {
            Picture p = (Picture)pictureGrid.SelectedItem;
            int position = pictureGrid.Items.IndexOf(pictureGrid.SelectedCells[0].Item);

            pictures.Remove(p);
            //p.InTransitionEffect= AUSWAHL
            pictures.Insert(position, p);
        }

        private void AddVisualEffect_Click(object sender, RoutedEventArgs e)
        {
            if (pictureGrid.Items.Count != 0)
            {
                Picture p = (Picture)pictureGrid.SelectedItem;
                int position = pictureGrid.Items.IndexOf(pictureGrid.SelectedCells[0].Item);

                pictures.Remove(p);
                //p.visualEffectType= AUSWAHL
                pictures.Insert(position, p);
            }
        }

        private void MoveUp_Click(object sender, RoutedEventArgs e)
        {
            Picture p = (Picture)pictureGrid.SelectedItem;
            int position = pictureGrid.Items.IndexOf(pictureGrid.SelectedCells[0].Item);
            if (position - 1 >= 0)
            {
                pictures.Remove(p);
                pictures.Insert(position - 1, p);
                this.pictureGrid.Items.Refresh();
            }

        }

        private void MoveDown_Click(object sender, RoutedEventArgs e)
        {
            Picture p = (Picture)pictureGrid.SelectedItem;
            int position = pictureGrid.Items.IndexOf(pictureGrid.SelectedCells[0].Item);
            if (position + 1 < pictures.Count)
            {
                pictures.Remove(p);
                pictures.Insert(position + 1, p);
                this.pictureGrid.Items.Refresh();
            }

        }

        private void SetDuration_Click(object sender, RoutedEventArgs e)
        {
            Picture p = (Picture)pictureGrid.SelectedItem;

            if (p == null)
            {
                List<Picture> helpPictures = new List<Picture>();

                foreach (Picture pics in pictures)
                {
                    pics.Duration = 10;
                    helpPictures.Add(pics);
                }
                pictures = helpPictures;
                this.pictureGrid.Items.Refresh();
            }

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
            this.music.Path = dlg.FileName;

        }

        public void AddPicture(Picture picture)
        {
            this.pictures.Add(picture);
            this.pictureGrid.Items.Refresh();
        }

        private void GenerateVideo_Click(object sender, RoutedEventArgs e)
        {

            // VideoBuilder builder = new VideoBuilder().SlideConfiguration(configuration).Music(music);
            VideoBuilder builder = new VideoBuilder();
            foreach (Picture picture in pictures)
            {
                builder = builder.AddPicture(picture);
            }
            builder = builder.Height(800).Width(800);
            if (!music.Path.Equals(""))
            {
                builder.AddMusic(music);
            }
            builder.Build(FilePath + "\\" + FileName + FileType);
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
                EnableComponets(result.Value);
                FileType = CreateProjectView.FileType;
                FileName = CreateProjectView.FileName;
                FilePath = CreateProjectView.FilePath;
                MainWindowName.Title = string.Format("Video From Image Creator - {0}", FileName);
            }
        }

        private void EnableComponets(bool result)
        {
            GenerateButton.IsEnabled = result;
        }
    }
}
