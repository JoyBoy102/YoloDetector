using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace YoloDetector.Services
{
    public class FrameConverter
    {
        /*ВСЕ ЭТИ ФУНКЦИИ ПОТОМ НА СЕРВЕРЕ НАДО БУДЕТ ДЕЛАТЬ */
        public static BitmapSource MatToBitmapSource(Mat mat)
        {
            // Способ 1: Конвертация через Bitmap
            using (Bitmap bitmap = mat.ToBitmap())
            {
                return ConvertBitmapToBitmapSource(bitmap);
            }
        }

        public static BitmapSource CreateGrayBitmap(int width, int height)
        {
            // Создаем серый цвет (128,128,128)
            byte[] pixels = new byte[width * height * 4]; // RGBA

            for (int i = 0; i < pixels.Length; i += 4)
            {
                pixels[i] = 128;     // R
                pixels[i + 1] = 128; // G
                pixels[i + 2] = 128; // B
                pixels[i + 3] = 255; // A (полностью непрозрачный)
            }

            return BitmapSource.Create(
                width, height,           // Ширина, высота
                96, 96,                  // DPI
                PixelFormats.Bgra32,     // Формат пикселей
                null,                    // Палитра
                pixels,                  // Данные пикселей
                width * 4);              // Stride (ширина * байты на пиксель)
        }

        private static BitmapSource ConvertBitmapToBitmapSource(Bitmap bitmap)
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

        private static System.Windows.Media.PixelFormat GetPixelFormat(System.Drawing.Imaging.PixelFormat pixelFormat)
        {
            // Преобразование System.Drawing.PixelFormat в System.Windows.Media.PixelFormat
            switch (pixelFormat)
            {
                case System.Drawing.Imaging.PixelFormat.Format24bppRgb:
                    return System.Windows.Media.PixelFormats.Bgr24;
                case System.Drawing.Imaging.PixelFormat.Format32bppRgb:
                    return System.Windows.Media.PixelFormats.Bgr32;
                case System.Drawing.Imaging.PixelFormat.Format32bppArgb:
                    return System.Windows.Media.PixelFormats.Bgra32;
                case System.Drawing.Imaging.PixelFormat.Format8bppIndexed:
                    return System.Windows.Media.PixelFormats.Gray8;
                default:
                    return System.Windows.Media.PixelFormats.Bgr24;
            }
        }

    }
}
