using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Printing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media.Imaging;
using YoloDetector.AdditionalWindows;
using YoloDetector.Services;
using YoloDetector.Structures;

namespace YoloDetector.Models
{
    public class MainWindowModel
    {
        public ObservableCollection<CameraStructure> Cameras;
        private ApiService _apiService;
        public MainWindowModel()
        {
            Cameras = new ObservableCollection<CameraStructure>()
            {
                new CameraStructure
                {
                    CurrentFrame = null,
                    CurrentStreamUri = "stream",
                    CameraName = "Стройка",
                },
                new CameraStructure
                {
                    CurrentFrame = null,
                    CurrentStreamUri = "stream",
                    CameraName = "Подъезд"
                }
            };
            _apiService = new ApiService(new System.Net.Http.HttpClient());
            
        }

        public async Task StartAllCamerasAsync()
        {
            foreach (var camera in Cameras)
            {
                _ = camera.StartProcessFramesAsync(_apiService);
            }
        }

        public async Task<bool> AddCamera()
        {
            AddCameraWindow addCameraWindow = new AddCameraWindow();
            addCameraWindow.Owner = Application.Current.MainWindow;
            bool? result = addCameraWindow.ShowDialog();

            if (result == true) // Пользователь нажал OK
            {
                string cameraName = addCameraWindow.CameraName;
                string videoPath = addCameraWindow.VideoPath;
                CameraStructure cameraStructure = new CameraStructure
                {
                    CameraName = cameraName,
                    CurrentStreamUri = "stream",
                    CurrentFrame = null,
                };
                _ = cameraStructure.StartProcessFramesAsync(_apiService);
                Cameras.Add(cameraStructure);
                return true;
            }
            return false;

        }

    }
}
