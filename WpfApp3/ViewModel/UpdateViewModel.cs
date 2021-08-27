using System;
using System.Diagnostics;
using System.Net;
using System.Windows;
using WpfApp3.Updater;

namespace WpfApp3.ViewModel
{
    public class UpdateViewModel: BaseViewModel
    {
        private int _progressPercentage;
        public int ProgressPercentage
        {
            get => _progressPercentage;
            private set => Set(nameof(ProgressPercentage), ref _progressPercentage, value);
        }

        public UpdateViewModel(InfoVersion info)
        {
            UpdateCurrApp(info);
        }

        private void UpdateCurrApp(InfoVersion info)
        {
            try
            {
                var client = new WebClient();
                var path = System.IO.Directory.GetCurrentDirectory();
                client.DownloadFileCompleted += DownloadFileCompleted;
                client.DownloadProgressChanged += DownloadProgressChanged;
                client.DownloadFileAsync(new Uri(info.Url), path + @"\\update.rar");
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error", ex.Message);
            }
        }

        private void DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            Process.Start("Update.exe");
            Environment.Exit(0);
        }

        private void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            ProgressPercentage = e.ProgressPercentage;
        }
    }
}
