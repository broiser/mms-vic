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
            builder = builder.AddPicture(new Picture( "C:\\Users\\Java\\Pictures\\Unbenannt2.PNG", 5000));
            builder =  builder.AddPicture(new Picture("C:\\Users\\Java\\Pictures\\Unbenannt.PNG", 2000));
            builder = builder.Height(800);
            builder = builder.Width(800);
            builder.Build("result.wmv");
        }
    }
}
