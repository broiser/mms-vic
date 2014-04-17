using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoFromImageCreator
{
    class Picture
    {

        public String Path { get; set; }
        public int Duration { get; set; }

        public Picture(String path, int duration)
        {
            this.Path = path;
            this.Duration = duration;
        }
    }
}
