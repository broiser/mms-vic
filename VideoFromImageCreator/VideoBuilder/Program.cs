using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            VideoBuilder builder = new VideoBuilder();
            builder = builder.Configuration(new SlideConfiguration(4));
            builder = builder.AddPicture(new Picture( "C:\\Users\\Java\\Pictures\\Unbenannt2.PNG", 2));
            builder = builder.AddPicture(new Picture("C:\\Users\\Java\\Pictures\\Unbenannt.PNG", 10));
            builder = builder.Music(new Music("C:\\Users\\Java\\Dropbox\\Musik-Uploads\\Icona Pop - All Night (Official Video Edit).mp3"));
            builder = builder.Height(800).Width(800);
            builder.Build("result.wmv");
        }
    }
}
