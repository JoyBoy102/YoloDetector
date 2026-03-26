using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace YoloDetector.AdditionalWindows
{
    /// <summary>
    /// Логика взаимодействия для AddCameraWindow.xaml
    /// </summary>
    public partial class AddCameraWindow : Window
    {
        public string CameraName { get; private set; } = "";
        public string VideoPath { get; private set; } = "";
        public AddCameraWindow()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            CameraName = CameraNameTextBox.Text;
            VideoPath = VideoPathTextBox.Text;
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
