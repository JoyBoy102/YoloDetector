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
        public MainWindowModel()
        {
            Cameras = new ObservableCollection<CameraStructure>()
            {
                new CameraStructure
                {
                    IsPlaying = true,
                    CurrentFrame = null,
                    Capture = new VideoCapture("C:\\Users\\Ainur\\source\\repos\\YoloDetector\\YoloDetector\\bin\\Debug\\net10.0-windows\\video.mp4"),
                    CancellationTokenSource = new CancellationTokenSource(),
                    CameraName = "Стройка"
                },
                new CameraStructure
                {
                    IsPlaying = false,
                    CurrentFrame = null,
                    Capture = new VideoCapture("C:\\Users\\Ainur\\source\\repos\\YoloDetector\\YoloDetector\\bin\\Debug\\net10.0-windows\\video2.mp4"),
                    CancellationTokenSource = new CancellationTokenSource(),
                    CameraName = "Подъезд"
                }
            };
            
        }

        public async Task StartAllCamerasAsync()
        {
            foreach (var camera in Cameras)
            {
                _ = camera.StartProcessFramesAsync();
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
                    CancellationTokenSource = new CancellationTokenSource(),
                    Capture = new VideoCapture(videoPath),
                    CurrentFrame = null,
                    IsPlaying = true
                };
                _ = cameraStructure.StartProcessFramesAsync();
                Cameras.Add(cameraStructure);
                return true;
            }
            return false;

        }

    }
}
