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
        public IDictionary<int, string> dirGenValues { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            MainWindowName.Title = productName;
            dirValues = new Dictionary<int, string>();
            dirGenValues = new Dictionary<int, string>();
        }
        /// <summary>
        /// Register the drag&drop events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindowName_Loaded(object sender, RoutedEventArgs e)
        {
            ListBox1.Drop += new System.Windows.DragEventHandler(ListBox1_Drop);
            ListBox.AddHandler(ListBoxItem.MouseLeftButtonDownEvent, new MouseButtonEventHandler(ListBox_MouseDown), true);
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
            KeyValuePair<int, string> keyPair = new KeyValuePair<int,string>();
            if (ListBox.SelectedItem is KeyValuePair<int, string>)
            {
                keyPair = ((KeyValuePair<int, string>)ListBox.SelectedItem);
            }
            string val = keyPair.Value;
            Picture picture = (Picture)generatePictures.Find(k => k.Path == val);
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
                foreach (string file in files)
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
            dirValues.Add(dirValues.Count + 1, picture.Path);
            ListBox.ItemsSource = dirValues;
            ListBox.Items.Refresh();
            //this.pictureGrid.Items.Refresh();
        }
        private void EditPicture(Picture picture)
        {
            dirValues.Select(i => i.Value == picture.Path);
        }
        private void RemovePicture_Click(object sender, RoutedEventArgs e)
        {
            KeyValuePair<int, string> keyPair = new KeyValuePair<int, string>();
            if (ListBox1.SelectedItem is KeyValuePair<int, string>)
            {
                keyPair = ((KeyValuePair<int, string>)ListBox1.SelectedItem);
            }

            generatePictures.Remove(generatePictures.Find(p => p.Path == keyPair.Value));

            dirGenValues.Remove(keyPair);
            ListBox1.ItemsSource = dirGenValues;
            ListBox1.Items.Refresh();
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
            builder.Build(FilePath + "\\" + FileName + "." + FileType);
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
        /// <summary>
        /// Drop Function for the Listbox1 (Box on the bottom)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBox1_Drop(object sender, System.Windows.DragEventArgs e)
        {
            string _class = (string)e.Data.GetData(typeof(string));
            if (_class != null)
            {
                dirGenValues.Add(dirGenValues.Count, _class);
                Picture pic = previewPictures.Find(p => p.Path == _class);
                previewPictures.Remove(pic);
                generatePictures.Add(pic);

                KeyValuePair<int, string> valuePair = dirValues.FirstOrDefault(s => s.Value == _class);
                dirValues.Remove(valuePair.Key);
                ListBox.ItemsSource = dirValues;
                ListBox.Items.Refresh();

                ListBox1.ItemsSource = dirGenValues;
                ListBox1.Items.Refresh();
            }
        }
        /// <summary>
        /// Drag Event for the ListBox (Box on the top)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string _class = null;
            if (ListBox.SelectedItem is KeyValuePair<int, string>)
            {
                _class = ((KeyValuePair<int, string>)ListBox.SelectedItem).Value;
            }

            if (_class != null)
            {
                System.Windows.DataObject dataObject = new System.Windows.DataObject(typeof(string), _class);
                DragDrop.DoDragDrop(ListBox, dataObject, System.Windows.DragDropEffects.Move);
            }
        }
    }
}
