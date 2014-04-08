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
        public MainWindow()
        {
            InitializeComponent();


            // Create BytescoutImageToVideoLib.ImageToVideo object instance
            ImageToVideo converter = new ImageToVideo();

            // Activate the component
            converter.RegistrationName = "demo";
            converter.RegistrationKey = "demo";

            // Add images and set the duration for every slide
            Slide slide;
            slide = converter.AddImageFromFileName("D:\\recovery\\Artworks\\1960.jpg");
            slide.Duration = 3000; // 3000ms = 3s
            slide = converter.AddImageFromFileName("D:\\recovery\\Artworks\\header668.jpg");

            slide.Duration = 3000;

            // Set output video size
            converter.OutputWidth = 400;
            converter.OutputHeight = 300;

            // Set output video file name
            converter.OutputVideoFileName = "result.wmv";

            Console.WriteLine("Conversion started. Hit a key to abort...");

            // Run the conversion
            converter.Run();
            /*
            // Show conversion progress:

            int i = 0;
            char[] spin = new char[] { '|', '/', '-', '\\' };

            while (!Console.KeyAvailable && converter.IsRunning)
            {
                float progress = converter.ConversionProgress;
                Console.WriteLine(String.Format("Converting images {0}% {1}", progress, spin[i++]));
                Console.CursorTop--;
                i %= 4;
                Thread.Sleep(50);
            }

            if (converter.IsRunning)
            {
                converter.Stop();
                Console.WriteLine("Conversion aborted by user.");
            }
            else
            {
                Console.WriteLine("Conversion competed successfully.");
            }

            // Release resources
            System.Runtime.InteropServices.Marshal.ReleaseComObject(converter);

            // Open the result video file in default media player
            Process.Start("result.wmv");

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
             */

        }
    }
}
