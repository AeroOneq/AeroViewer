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

        public LeftMenuPage() { InitializeComponent(); }
        public LeftMenuPage(Frame parentFrame)
        {
            InitializeComponent();

            SetProcessParams();

            ParentFrame = parentFrame;
            parentFrame.SizeChanged += RepositionPageElements;
        }

        /// <summary>
        /// Initializes the Process class properties,
        /// which then used to show the stage of each process
        /// </summary>
        private void SetProcessParams()
        {
            Process.StatusTextBox = statusTextBlock;
            Process.Dispatcher = Dispatcher;
            Process.Loader = loader;
            Process.SuccessIcon = processSuccessImage;
            Process.FailIcon = processFailIcon;
        }

        /// <summary>
        /// Changes the size of some elements when the size of the window is changed
        /// </summary>
        private void RepositionPageElements(object sender, SizeChangedEventArgs e)
        {
            Width = ParentFrame.Width;
            Height = ParentFrame.Height;

            outterBorder.Width = Width;
            outterBorder.Height = Height;
        }

        #region Event handlers
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
        #endregion

        private async void UploadCSVFile(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.SetInitialStatus("Открытие файла");
                CSVService.UpdateFilePath(filePathTextBox.Text);
                IFileService<TunnelExit> csvService = CSVService.CSVServiceObject;

                Process.UpdateStatus("Чтение данных");
                var tunnelsList = await csvService.ReadAsync();

                Process.UpdateStatus("Отображение данных");
                await Task.Run(() => MainPageModel.PageModel.CreateNewTunnelData(tunnelsList));

                await MainPageModel.UploadDelegate();

                int damagedRecNum = MainPageModel.PageModel.NumberOfDamagedRecords;
                Process.SetFinalStatus($"Файл загружен\n" + ((damagedRecNum == 0) ? string.Empty
                    : $"Кол-во повреждений: {damagedRecNum}"), true);
            }
            catch (Exception ex)
            {
                Process.SetFinalStatus("Файл не загружен", false);
                ExceptionHandler.Handler.HandleExceptionWithMessageBox(ex,
                    "Ошибка при загрузке файла");
            }
        }
    }
}
