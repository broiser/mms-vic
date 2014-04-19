using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BytescoutImageToVideoLib;

namespace VideoFromImageCreator
{
    class VideoBuilder
    {
        private ImageToVideo imageToVideo = new ImageToVideo();


        public VideoBuilder RegistraitionName(String name)
        {
            imageToVideo.RegistrationName = name;
            return this;
        }

        public VideoBuilder RegistrationKey(String key){
            imageToVideo.RegistrationKey = key;
            return this;
        }

        public VideoBuilder Width(int width){
            imageToVideo.OutputWidth = width;
            return this;
        }

        public VideoBuilder Height(int height){
            imageToVideo.OutputHeight = height;
            return this;
        }

        public VideoBuilder AddPictureFromFolder(String path){
            imageToVideo.AddAllImagesFromFolder(path, true);
            return this;
        }

        public VideoBuilder AddPicture(Picture picture){
            Slide slide = imageToVideo.AddImageFromFileName(picture.Path);
            slide.Duration = picture.Duration;
            slide.InEffect = picture.InTransitionEffect;
            slide.OutEffect = picture.OutTransitionEffect;
            return this;
        }

        public VideoBuilder AddMusic(Music music){
            imageToVideo.ExternalAudioTrackFromFileName = music.Path;
            return this;
        }

        public void Build(String path){
            imageToVideo.OutputVideoFileName = path;
            imageToVideo.RunAndWait();
        }   
    }
}
