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
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using AeroViewer.Models;
using System.Reflection;
using AeroViewer.Attributes;
using AeroViewer.ViewModels;
using System.IO;
using AeroViewer.Services;

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
        public SaveWindow() { }
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
        #endregion

        #region Save file methods
        private async void SaveFile(object sender, EventArgs e)
        {
            string filePath = filePathTextBox.Text;
            try
            {
                CSVService.UpdateFilePath(filePathTextBox.Text);
                CSVService csvService = CSVService.GetService();

                await csvService.Write(TunnelExits);
            }
            catch (UnauthorizedAccessException ex)
            {
                ExceptionHandler.GetHandler().HandleExceptionWithMessageBox(
                    ex, "Ошибка при сохранении файла");
            }
            catch (IOException ex)
            {
                ExceptionHandler.GetHandler().HandleExceptionWithMessageBox(
                    ex, "Ошибка при сохранении файла");
            }
            catch (Exception ex)
            {
                ExceptionHandler.GetHandler().HandleExceptionWithMessageBox(
                    ex, "Ошибка при сохранении файла");
            }
        }
        #endregion
    }
}
