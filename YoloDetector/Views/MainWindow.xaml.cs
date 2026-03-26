using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YoloDetector.ViewModels;

namespace YoloDetector
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeViewModelAsync();
        }

        private async Task InitializeViewModelAsync()
        {
            var viewModel = await MainWindowViewModel.CreateAsync();
            DataContext = viewModel;
        }
    }
}