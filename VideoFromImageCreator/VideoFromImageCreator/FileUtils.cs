using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoFromImageCreator
{
   public class FileUtils
    {

        public static bool IsImage(string file)
        {
            return file.EndsWith(".jpg") || file.EndsWith(".jpeg") || file.EndsWith(".png") || file.EndsWith(".gif") || file.EndsWith(".bmp");
        }
    }
}
