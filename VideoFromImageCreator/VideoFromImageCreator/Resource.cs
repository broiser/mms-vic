using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoFromImageCreator
{
    public abstract class Resource
    {

        public String Path { get; set; }

        public Resource(String path)
        {
            this.Path = path;
        }

    }
}
