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
        private const string PRODUCT_NAME = "Video from Image Creator";


        private List<Picture> previewPictures = new List<Picture>();
        //With these Pictures, the Video will be generated)
        private List<Picture> generatePictures = new List<Picture>();

        private Music music;

        //These dictionarys are the Collections which are bound to the ListBoxes! (GUI-only; they have nothing to do with the generated video)
        public IDictionary<int, string> dirValues { get; set; }
        public IDictionary<int, string> dirGenValues { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            SetComboBoxes();
            MainWindowName.Title = PRODUCT_NAME;
            dirValues = new Dictionary<int, string>();
            dirGenValues = new Dictionary<int, string>();
        }

        private void SetComboBoxes()
        {
            FiletypeCombobox.Items.Add(".avi");
            FiletypeCombobox.Items.Add(".wmv");
        }

        private void ProjectDialog_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();
            PathTextbox.Text = folderBrowserDialog.SelectedPath;
        }

           private void MusicDialog_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".mp3";
            dlg.Filter = "Music Files|*.mp3";

            // Display OpenFileDialog by calling ShowDialog method
            DialogResult result = dlg.ShowDialog();

            // Set Filename
            this.music = new Music(dlg.FileName);
            MusicTextbox.Text = dlg.FileName.Split('\\').Last();
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

        private void EditPicturePreviewBox_Click(object sender, RoutedEventArgs e)
        {
            EditPicture(ListBox, previewPictures);
        }
        private void EditPictureGenerateBox_Click(object sender, RoutedEventArgs e)
        {
            EditPicture(ListBox1, generatePictures);
        }

        private void EditPicture(System.Windows.Controls.ListBox listbox, List<Picture> pics)
        {
            KeyValuePair<int, string> keyPair = new KeyValuePair<int, string>();
            if (listbox.SelectedItem is KeyValuePair<int, string>)
            {
                keyPair = ((KeyValuePair<int, string>)listbox.SelectedItem);
            }
            string val = keyPair.Value;

            //Get Picture reference
            Picture picture = null;
            foreach (Picture p in pics)
            {
                if (p.Path == val)
                {
                    picture = p;
                    break;
                }
            }
            if (picture != null)
            {
                AddPictureWindow addPictureWindow = new AddPictureWindow();

                addPictureWindow.Init(picture);
                if (addPictureWindow.ShowDialog().Value)
                {
                    pics.Remove(picture);
                    picture = addPictureWindow.Picture;
                    pics.Insert(0, picture);
                }
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
                    
                    if (FileUtils.IsImage(file.ToLower()))
                    {
                        AddPicture(new Picture(file, DEFAULT_DURATION, TransitionEffectType.teNone, TransitionEffectType.teNone, VisualEffectType.veNone));
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void MoveLeft_Click(object sender, RoutedEventArgs e)
        {

            int selectedIndex = ListBox1.SelectedIndex;
            //Switch is allowed, if the picture is not the first one (the most left picture) and when there is one selected (selectedInde == -1, if not)
            if (selectedIndex > 0)
            {
                switchKVPairs(selectedIndex, selectedIndex - 1);
            }
        }

        private void MoveRight_Click(object sender, RoutedEventArgs e)
        {

            int selectedIndex = ListBox1.SelectedIndex;

            //Switch is only allowed, if the picture is not the last one (the most right picture) and when there is one selected
            if (selectedIndex + 1 < generatePictures.Count && selectedIndex != -1)
            {
                switchKVPairs(selectedIndex, selectedIndex + 1);
            }
        }

        /// <summary>
        /// Sets the Duration of a Picture, if one is selected or of all Pictures is generateList, if nothing is selected 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetDuration_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = ListBox1.SelectedIndex;
            int newDuration = 0;
            SetDurationView durationView = new SetDurationView();

            if (durationView.ShowDialog().Value)
            {
                newDuration = durationView.newDuration;
            }
            if (newDuration > 0)
            {
                //Set Duration of selected Picture
                if (selectedIndex != -1)
                {
                    Picture p = generatePictures[selectedIndex];
                    p.Duration = newDuration;
                }
                    //Set Duration of all Pictures
                else
                {
                    foreach (Picture p in generatePictures)
                    {
                        p.Duration = newDuration;
                    }
                }
            }
        }

        public void AddPicture(Picture picture)
        {
            this.previewPictures.Add(picture);
            dirValues.Add(dirValues.Count + 1, picture.Path);
            ListBox.ItemsSource = dirValues;
            ListBox.Items.Refresh();
        }

        private void RemovePicture_Click(object sender, RoutedEventArgs e)
        {
            RemoveItemFromList(ListBox1, generatePictures, dirGenValues);
        }

        private void RemovePreviewPicture_Click(object sender, RoutedEventArgs e)
        {
            RemoveItemFromList(ListBox, previewPictures, dirValues);
        }

        private void RemoveItemFromList(System.Windows.Controls.ListBox listbox, List<Picture> pics, IDictionary<int, string> dict)
        {
            KeyValuePair<int, string> keyPair = new KeyValuePair<int, string>();
            if (listbox.SelectedItem is KeyValuePair<int, string>)
            {
                keyPair = ((KeyValuePair<int, string>)listbox.SelectedItem);
            }

            pics.Remove(pics.Find(p => p.Path == keyPair.Value));

            dict.Remove(keyPair.Key);
            listbox.ItemsSource = dict;
            listbox.Items.Refresh();
        }

        private void GenerateVideo_Click(object sender, RoutedEventArgs e)
        {
            //When there is no new project created, the Window CreateProjectView will be opend 
            if (PathTextbox.Text == null || FiletypeCombobox.SelectedValue == null || FileNameTextBox.Text == null)
            {
                setVideoMetadata();
            } if (generatePictures.Count > 0)
            {
                VideoBuilder builder = new VideoBuilder();
                foreach (Picture picture in generatePictures)
                {
                    builder = builder.AddPicture(picture);
                }
                builder = builder.Height(800).Width(800);
                if (!(music == null || string.IsNullOrEmpty(music.Path)))
                {
                    builder.AddMusic(music);
                }
                //Sets videopath to the inserted value
                builder.Build(PathTextbox.Text + "\\" + FileNameTextBox.Text + "." + FiletypeCombobox.SelectedValue);
            }
        }

        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        
        private void setVideoMetadata()
        {
            CreateProjectView CreateProjectView = new CreateProjectView();
            var result = CreateProjectView.ShowDialog();
            if (result.Value)
            {
                FiletypeCombobox.SelectedValue = CreateProjectView.FileType;
                FileNameTextBox.Text = CreateProjectView.FileName;
                PathTextbox.Text = CreateProjectView.FilePath;
                MainWindowName.Title = string.Format(PRODUCT_NAME + "- {0}", FileNameTextBox.Text);
            }
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

        /// <summary>
        /// Switches the Entries in the dirGenValues-Directory
        /// </summary>
        /// <param name="indexA"></param>
        /// <param name="indexB"></param>
        private void switchKVPairs(int indexA, int indexB)
        {
            Picture picture = this.generatePictures[indexA];
            this.generatePictures.RemoveAt(indexA);
            this.generatePictures.Insert(indexB, picture);

            this.ListBox1.SelectedIndex = indexB;

            KeyValuePair<int, string> first = new KeyValuePair<int, string>();
            if (ListBox1.Items[indexA] is KeyValuePair<int, string>)
            {
                first = ((KeyValuePair<int, string>)ListBox1.Items[indexA]);
            }
            KeyValuePair<int, string> second = new KeyValuePair<int, string>();
            if (ListBox1.Items[indexB] is KeyValuePair<int, string>)
            {
                second = ((KeyValuePair<int, string>)ListBox1.Items[indexB]);
            }
            dirGenValues.Remove(first.Key);
            dirGenValues.Remove(second.Key);


            dirGenValues.Add(new KeyValuePair<int, string>(second.Key, first.Value));
            dirGenValues.Add(new KeyValuePair<int, string>(first.Key, second.Value));
            ListBox1.Items.Refresh();

        }
    }
}
