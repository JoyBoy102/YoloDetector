using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media.Imaging;
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
                    CameraID = 0,
                    IsPlaying = true,
                    VideoPath = "C:\\Users\\Ainur\\source\\repos\\YoloDetector\\YoloDetector\\bin\\Debug\\net10.0-windows\\video.mp4"
                },
                new CameraStructure
                {
                    CameraID = 1,
                    IsPlaying = false,
                    VideoPath = "C:\\Users\\Ainur\\source\\repos\\YoloDetector\\YoloDetector\\bin\\Debug\\net10.0-windows\\video.mp4"
                }
            };
        }

        public BitmapSource MatToBitmapSource(Mat mat)
        {
            // Способ 1: Конвертация через Bitmap
            using (Bitmap bitmap = mat.ToBitmap())
            {
                return ConvertBitmapToBitmapSource(bitmap);
            }
        }

        private BitmapSource ConvertBitmapToBitmapSource(Bitmap bitmap)
        {
            // Получаем данные Bitmap
            var bitmapData = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly,
                bitmap.PixelFormat);

            // Создаем BitmapSource
            var bitmapSource = BitmapSource.Create(
                bitmapData.Width,
                bitmapData.Height,
                96, 96, // DPI
                GetPixelFormat(bitmap.PixelFormat),
                null,
                bitmapData.Scan0,
                bitmapData.Stride * bitmapData.Height,
                bitmapData.Stride);

            // Освобождаем ресурсы
            bitmap.UnlockBits(bitmapData);

            // Замораживаем для улучшения производительности
            bitmapSource.Freeze();

            return bitmapSource;
        }

        private System.Windows.Media.PixelFormat GetPixelFormat(PixelFormat pixelFormat)
        {
            // Преобразование System.Drawing.PixelFormat в System.Windows.Media.PixelFormat
            switch (pixelFormat)
            {
                case PixelFormat.Format24bppRgb:
                    return System.Windows.Media.PixelFormats.Bgr24;
                case PixelFormat.Format32bppRgb:
                    return System.Windows.Media.PixelFormats.Bgr32;
                case PixelFormat.Format32bppArgb:
                    return System.Windows.Media.PixelFormats.Bgra32;
                case PixelFormat.Format8bppIndexed:
                    return System.Windows.Media.PixelFormats.Gray8;
                default:
                    return System.Windows.Media.PixelFormats.Bgr24;
            }
        }

    }
}
