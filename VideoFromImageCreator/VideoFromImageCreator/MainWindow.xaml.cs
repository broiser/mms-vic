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

namespace VideoFromImageCreator
{
    public partial class MainWindow : Window
    {
        private const String RESULT = "result.wmv";
        private const int DEFAULT_DURATION = 5000;


        private List<Picture> pictures = new List<Picture>();
        private Music music = new Music("");
        private SlideConfiguration configuration = new SlideConfiguration(4);

        public MainWindow()
        {
            InitializeComponent();
            InitializePictureGrid();
        }

        private void InitializePictureGrid()
        {
            pictureGrid.ItemsSource = pictures;
        }

        private void AddFile_Click(object sender, RoutedEventArgs e)
        {
            AddPictureWindow window = new AddPictureWindow(this);
            window.Visibility = Visibility.Visible;
            this.Visibility = Visibility.Hidden;
        }

        private void AddFolder_Click(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        private void AddEffect_Click(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        private void MoveUp_Click(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        private void MoveDown_Click(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        private void SetDuration_Click(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        private void AddMusic_Click(object sender, RoutedEventArgs e)
        {
            //TODO
            //Voerst mal nur 1 Titel hinzufügen und dann je nach Möglichkeiten der Bibliothek eine Liste/DataGrid aufbauen und wie bei den Pictures machen...
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
            builder.Build(RESULT);
        }

    }
}
