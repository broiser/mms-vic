using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BytescoutImageToVideoLib;

namespace VideoFromImageCreator
{
    public class Picture : Resource
    {

        public int Duration { get; set; }
        public TransitionEffectType InTransitionEffect {get; set; }
        public TransitionEffectType OutTransitionEffect {get; set; }

        public Picture(String path, int duration, TransitionEffectType inTransitionEffect, TransitionEffectType outTransitionEffect) : base(path)
        {

            this.Duration = duration;
            this.InTransitionEffect = inTransitionEffect;
            this.OutTransitionEffect = outTransitionEffect;
        }
    }
}
