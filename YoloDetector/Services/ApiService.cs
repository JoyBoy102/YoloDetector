using Emgu.CV;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Windows;

namespace YoloDetector.Services
{
    public class ApiService
    {
        private string baseAddress = "http://127.0.0.1:8000";
        private HttpClient _httpClient;
    
        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(baseAddress);
        }

        private const string _getFrameEndpoint = "/frame";

        public VideoCapture GetStream(string endPoint)
        {
            var capture = new VideoCapture($"{baseAddress}/{endPoint}");
            return capture;
        }
    }
}
