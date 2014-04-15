using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoBuilder
{
    class SlideConfiguration
    {
        public SlideConfiguration(int duration)
        {
            this.Duration = duration;
        }

        public int Duration { get; set; }
    }
}
