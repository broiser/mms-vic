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
using BytescoutImageToVideoLib;

namespace VideoFromImageCreator
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<Picture> pictureList = new List<Picture>();

        public MainWindow()
        {
            InitializeComponent();
            this.pictureGrid.ItemsSource = this.pictureList;
        }

        private void addPicture(Picture picture)
        {
            this.pictureList.Add(picture);
            this.pictureGrid.Items.Refresh();
        }

        private void AddFile_Click(object sender, RoutedEventArgs e)
        {
            //TODO
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
        }

    }
}
