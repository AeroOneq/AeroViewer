using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Collections.ObjectModel;
using AeroViewer.Models;
using AeroViewer.ViewModels;
using System.IO;
using AeroViewer.Services;
using AeroViewer.Interfaces;

namespace AeroViewer
{
    /// <summary>
    /// Логика взаимодействия для SaveWindow.xaml
    /// </summary>
    public partial class SaveWindow : Window
    {
        #region Properties
        private List<TunnelExit> TunnelExits { get; set; }
        #endregion

        #region Constrcutors
        public SaveWindow() { InitializeComponent(); }
        public SaveWindow(ObservableCollection<TunnelExitModel> tunnelExitModels,
            string filePath)
        {
            InitializeComponent();

            filePathTextBox.Text = filePath;
            TunnelExits = CreateTunnelExitsList(tunnelExitModels);
        }
        #endregion

        private List<TunnelExit> CreateTunnelExitsList(
            ObservableCollection<TunnelExitModel> tunnelExitModels)
        {
            List<TunnelExit> tunnelExits = new List<TunnelExit>();

            foreach (TunnelExitModel tunnelExitModel in tunnelExitModels)
                tunnelExits.Add(new TunnelExit(tunnelExitModel));

            return tunnelExits;
        }

        #region Event handlers
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

        private void DeleteButtonMouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Button).Background = new LinearGradientBrush(
                Color.FromRgb(139, 0, 0), Color.FromRgb(178, 34, 24), 38);
        }

        private void DeleteButtonMouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Button).Background = new SolidColorBrush(
                Color.FromRgb(139, 0, 0));
        }

        private void RewriteFileRadioBtnChecked(object sender, RoutedEventArgs e)
        {
            appendFileRadioBtn.IsChecked = false;
            addHeadStringRadioBtn.IsChecked = false;
            addHeadStringRadioBtn.Visibility = Visibility.Collapsed;
        }

        private void AppendFileRadioBtnChecked(object sender, RoutedEventArgs e)
        {
            rewriteFileRadioBtn.IsChecked = false;
            addHeadStringRadioBtn.Visibility = Visibility.Visible;
        }
        #endregion

        #region Save file methods
        /// <summary>
        /// Saves the changes in CSV file in a mode which was selected by user
        /// (Rewrite, append w/o header string, append w header string)
        /// </summary>
        private async void SaveFile(object sender, EventArgs e)
        {
            string filePath = filePathTextBox.Text;
            try
            {
                Close();

                Process.SetInitialStatus("Открываем файл");
                CSVService.UpdateFilePath(filePathTextBox.Text);
                IFileService<TunnelExit> csvService = CSVService.CSVServiceObject;

                Process.UpdateStatus("Записываем в файл");
                if (rewriteFileRadioBtn.IsChecked == true)
                    await csvService.RewriteAsync(TunnelExits);
                else
                    await csvService.AppendAsync(TunnelExits, addHeadStringRadioBtn.IsChecked);

                Process.SetFinalStatus("Файл перезаписан", true);
            }
            catch (UnauthorizedAccessException ex)
            {
                HandleException(ex);
            }
            catch (IOException ex)
            {
                HandleException(ex);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void HandleException(Exception ex)
        {
            Process.SetFinalStatus("Ошибка записи", false);
            ExceptionHandler.Handler.HandleExceptionWithMessageBox(
                ex, "Ошибка при сохранении файла");
        }
        #endregion
    }
}
