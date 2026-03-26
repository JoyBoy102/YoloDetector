using CommunityToolkit.Mvvm.Input;
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
using YoloDetector.AdditionalWindows;

namespace YoloDetector.ViewModels
{
    public class MainWindowViewModel: BaseViewModel
    {
        private MainWindowModel _mainModel;
        private VideoCapture _capture;
        private CancellationTokenSource _cts;
        private CameraStructure _selectedCamera;
        public IAsyncRelayCommand AddCameraCommand { get; set; }
        public MainWindowViewModel()
        {
            _mainModel = new MainWindowModel();
            AddCameraCommand = new AsyncRelayCommand(AddCamera);
        }

        public static async Task<MainWindowViewModel> CreateAsync()
        {
            var instance = new MainWindowViewModel();
            await instance.InitializeAsync();
            return instance;
        }

        private async Task InitializeAsync()
        {
            await _mainModel.StartAllCamerasAsync();
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
                   // _ = OnSelectedCameraChangedAsync();
                }
            }
        }

        public async Task OnSelectedCameraChangedAsync()
        {
           // _cts?.Cancel();
           // _capture?.Dispose();
          //  if (_selectedCamera != null)
          //  {
          //      _cts = new CancellationTokenSource();
          //      _capture = new VideoCapture(SelectedCamera.VideoPath);
          //      await StartProcessFrames(_capture, _cts.Token);
          //  }
        }

        private async Task AddCamera()
        {
            var res = await _mainModel.AddCamera();
            if (res) OnPropertyChanged(nameof(AllCameras));
        }

    }
}
