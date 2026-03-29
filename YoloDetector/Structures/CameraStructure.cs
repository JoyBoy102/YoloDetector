using Emgu.CV;
using Emgu.CV.Structure;
using MjpegProcessor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using YoloDetector.Services;
using YoloDetector.ViewModels;

namespace YoloDetector.Structures
{
    public class CameraStructure : INotifyPropertyChanged
    {
        public string CameraName { get; set; }
        public BitmapSource CurrentFrame { get; set; }

        public string CurrentStreamUri {  get; set; }
        
        public async Task StartProcessFramesAsync(ApiService apiService)
        {
            var capture = apiService.GetStream(CurrentStreamUri);

            while (true)
            {
                using (var mat = capture.QueryFrame())
                {
                    if (mat != null)
                    {
                        var bitmapSource = FrameConverter.MatToBitmapSource(mat);

                        await Application.Current.Dispatcher.InvokeAsync(() =>
                        {
                            CurrentFrame = bitmapSource;
                            OnPropertyChanged(nameof(CurrentFrame));
                        });
                    }
                }
                await Task.Delay(33);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
