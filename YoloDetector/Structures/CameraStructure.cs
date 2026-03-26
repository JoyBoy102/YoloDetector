using Emgu.CV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;
using YoloDetector.Services;
using YoloDetector.ViewModels;

namespace YoloDetector.Structures
{
    public class CameraStructure : INotifyPropertyChanged
    {
        public bool IsPlaying { get; set; }
        public string CameraName { get; set; }
        public BitmapSource CurrentFrame { get; set; }


        public VideoCapture Capture { get; set; }

        public CancellationTokenSource CancellationTokenSource;
        
        public async Task StartProcessFramesAsync()
        {
            var token = CancellationTokenSource.Token;
            while (!token.IsCancellationRequested)
            {
                using (Mat frame = new Mat())
                {
                    Capture.Read(frame);

                    if (!frame.IsEmpty)
                    {
                        var bitmapSource = FrameConverter.MatToBitmapSource(frame);

                        await Application.Current.Dispatcher.InvokeAsync(() =>
                        {
                            CurrentFrame = bitmapSource;
                            OnPropertyChanged(nameof(CurrentFrame));
                        });
                    }
                    else
                    {
                        await Task.Delay(10, token);
                    }
                }
                await Task.Delay(33, token);
            }
            CurrentFrame = FrameConverter.CreateGrayBitmap(96, 96);
            OnPropertyChanged(nameof(CurrentFrame));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
