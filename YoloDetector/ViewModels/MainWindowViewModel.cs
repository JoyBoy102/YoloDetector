using Emgu.CV;
using Emgu.CV.CvEnum;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media.Imaging;
using YoloDetector.Models;
using YoloDetector.Structures;

namespace YoloDetector.ViewModels
{
    public class MainWindowViewModel: BaseViewModel
    {
        private MainWindowModel _mainModel;
        private VideoCapture _capture;
        private CancellationTokenSource _cts;
        private CameraStructure _selectedCamera;
        private BitmapSource _currentFrame;
        public MainWindowViewModel()
        {
            _mainModel = new MainWindowModel();
        }

        public ObservableCollection<CameraStructure> AllCameras
        {
            get => _mainModel.Cameras;
            set => SetProperty(ref _mainModel.Cameras, value);
        }

        public CameraStructure SelectedCamera
        {
            get => _selectedCamera;
            set
            {
                if (SetProperty(ref _selectedCamera, value))
                {
                    _ = OnSelectedCameraChangedAsync();
                }
            }
        }

        public BitmapSource CurrentFrame
        {
            get => _currentFrame;
            set => SetProperty(ref _currentFrame, value);
        }

        public async Task OnSelectedCameraChangedAsync()
        {
            _cts?.Cancel();
            _capture?.Dispose();
            if (_selectedCamera != null)
            {
                _cts = new CancellationTokenSource();
                _capture = new VideoCapture(SelectedCamera.VideoPath);
                await StartProcessFrames(_capture, _cts.Token);
            }
        }

        public async Task StartProcessFrames(VideoCapture capture, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                using (Mat frame = new Mat())
                {
                    capture.Read(frame);

                    if (!frame.IsEmpty)
                    {
                        var bitmapSource = _mainModel.MatToBitmapSource(frame);

                        await Application.Current.Dispatcher.InvokeAsync(() =>
                        {
                            CurrentFrame = bitmapSource;
                        });
                    }
                    else
                    {
                        await Task.Delay(10, token);
                    }
                }
                await Task.Delay(33, token);
            }

        }


    }
}
