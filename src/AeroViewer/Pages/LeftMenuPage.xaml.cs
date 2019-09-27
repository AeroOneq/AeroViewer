using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using AeroViewer.Models;
using AeroViewer.Services;
using AeroViewer.Interfaces;
using AeroViewer.ViewModels;
using Microsoft.Win32;

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
            parentFrame.SizeChanged += ResizePageElements;
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
        private void ResizePageElements(object sender, SizeChangedEventArgs e)
        {
            Width = ParentFrame.Width;
            Height = ParentFrame.Height;

            outterBorder.Width = Width;
            outterBorder.Height = Height;
        }

        private async void UploadCSVFile(object sender, RoutedEventArgs e)
        {
            Button uploadFileBtn = sender as Button;
            uploadFileBtn.IsEnabled = false;
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
            finally
            {
                uploadFileBtn.IsEnabled = true;
            }
        }

        private void SelectFile(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog()
                {
                    Filter = "CSV files(*csv)|*csv",
                    Title = "Выберете CSV файл для открытия"
                };

                if (openFileDialog.ShowDialog() != true)
                    return;

                filePathTextBox.Text = openFileDialog.FileName;
            }
            catch (NullReferenceException ex)
            {
                ExceptionHandler.Handler.HandleExceptionWithMessageBox(ex,
                    "Ошибка при выборе файла");
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handler.HandleExceptionWithMessageBox(ex,
                    "Ошибка при выборе файла");
            }
        }
    }
}
