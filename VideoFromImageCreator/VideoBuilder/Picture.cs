using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BytescoutImageToVideoLib;

namespace VideoBuilder
{
    class Picture : Resource
    {

        public Picture(String path, int duration)
            : this(path, duration, SlideEffectType.seNone, PanZoomEffectType.pzeNone)
        {
           
        }

        public Picture(String path, int duration, SlideEffectType slideEffect)
            : this(path, duration, slideEffect, PanZoomEffectType.pzeNone)
        {
        }

        public Picture(String path, int duration, PanZoomEffectType zoomEffect)
            : this(path, duration, SlideEffectType.seNone, zoomEffect)
        {
        }

        public Picture(String path, int duration, SlideEffectType slideEffect, PanZoomEffectType zoomEffect) : base(path)
        {
            this.Duration = duration;
            this.SlideEffect = slideEffect;
            this.ZoomEffect = zoomEffect;
        }

        public int Duration { get; set; }
        public SlideEffectType SlideEffect{get; set;}
        public PanZoomEffectType ZoomEffect { get; set; }
    }
}
