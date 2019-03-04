using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AeroViewer.Models;
using AeroViewer.Services;
using AeroViewer.Interfaces;
using AeroViewer.ViewModels;

namespace AeroViewer
{
    public partial class LeftMenuPage : Page
    {
        public Frame ParentFrame { get; }
        private IFileService<TunnelExit> CSVFileService { get; set; }

        public LeftMenuPage(Frame parentFrame)
        {
            InitializeComponent();

            Process.StatusTextBox = statusTextBlock;
            Process.Dispatcher = Dispatcher;
            ParentFrame = parentFrame;
            parentFrame.SizeChanged += RepositionPageElements;
        }

        private void RepositionPageElements(object sender, SizeChangedEventArgs e)
        {
            this.Width = ParentFrame.Width;
            this.Height = ParentFrame.Height;

            outterBorder.Width = Width;
            outterBorder.Height = Height;
        }

        private void FilePathTextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Style = Resources["menuTextBoxStyleActive"]
                as Style;
        }

        private void FilePathTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Style = Resources["menuTextBoxStylePassive"]
                as Style;
        }

        private void MenuButtonMouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Button).Background = new LinearGradientBrush(
                Color.FromRgb(34, 139, 34), Color.FromRgb(0, 162, 80), 38);
        }

        private void MenuButtonMouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Button).Background = new SolidColorBrush(
                Color.FromRgb(34, 139, 34));
        }

        private async void UploadCSVFile(object sender, RoutedEventArgs e)
        {
            try
            {
                loader.Visibility = Visibility.Visible;
                Process.UpdateStatus("Открытие файла");
                CSVService csvService = new CSVService(filePathTextBox.Text);
                Process.UpdateStatus("Чтение данных");
                var tunnelsList = await csvService.Read();
                Process.UpdateStatus("Отображение данных");
                await Task.Run(() => MainPageModel.GetModel().CreateNewTunnelData(tunnelsList));
                await MainPageModel.UploadDelegate();
                loader.Visibility = Visibility.Collapsed;
            }
            finally
            {

            }
        }
    }
}
