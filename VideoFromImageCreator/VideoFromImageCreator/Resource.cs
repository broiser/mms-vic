using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoFromImageCreator
{
    abstract class Resource
    {
        public Resource(String path)
        {
            this.Path = path;
        }

        public String Path { get; set; }
    }
}
