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
using Microsoft.Win32;

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
        public SaveWindow(ObservableCollection<TunnelExitModel> tunnelExitModels)
        {
            if (tunnelExitModels == null || tunnelExitModels.Count == 0)
                throw new ArgumentException("Нет данных для сохранения");

            InitializeComponent();

            filePathTextBox.Text = CSVService.CSVServiceObject.Database.FilePath;
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
                    await csvService.AppendAsync(TunnelExits, false);

                Process.SetFinalStatus("Файл перезаписан", true);
            }
            catch (UnauthorizedAccessException ex)
            {
                HandleSaveFileException(ex);
            }
            catch (IOException ex)
            {
                HandleSaveFileException(ex);
            }
            catch (Exception ex)
            {
                HandleSaveFileException(ex);
            }
        }

        private void HandleSaveFileException(Exception ex)
        {
            Process.SetFinalStatus("Ошибка записи", false);
            ExceptionHandler.Handler.HandleExceptionWithMessageBox(
                ex, "Ошибка при сохранении файла");
        }

        private void ChooseFilePath(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Title = "Выберете файл или папку",
                Filter = "CSV files(*.csv)|*csv"
            };

            if (saveFileDialog.ShowDialog() == false)
                return;

            filePathTextBox.Text = saveFileDialog.FileName;
            if (filePathTextBox.Text.Substring(filePathTextBox.Text.Length - 4) != ".csv")
                filePathTextBox.Text += ".csv";
        }

        private void CancelSavingProcess(object sender, EventArgs e) => Close();
        #endregion
    }
}
