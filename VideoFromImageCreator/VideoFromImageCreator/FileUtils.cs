using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoFromImageCreator
{
   public class FileUtils
    {
       private  static string[] IMAGE_TYPES =  { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };

        public static bool IsImage(string file)
        {
            foreach (string type in IMAGE_TYPES)
            {

                if (file.ToLower().EndsWith(type))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
