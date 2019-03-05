﻿using System;
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
using AeroViewer.ViewModels;
using AeroViewer.Attributes;
using System.Reflection;
using System.Collections.ObjectModel;
using AeroViewer.Services;

namespace AeroViewer
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        #region Properties
        public Frame ParentFrame { get; set; }
        private string CurrentFilePath { get; set; }
        #endregion
        public MainPage(Frame parentFrame)
        {
            InitializeComponent();

            ParentFrame = parentFrame;
            ParentFrame.SizeChanged += OnParentFrameSizeChanged;
            MainPageModel.UploadDelegate += UploadData;
        }

        public async Task UploadData()
        {
            await Task.Run(async () =>
            {
                await Dispatcher.BeginInvoke(new Action(() =>
                    csvDataGrid.ItemsSource = MainPageModel.GetModel().TunnelsData));
            });
            EditAutoGeneratedColums<TunnelExitModel>();
        }

        private void EditAutoGeneratedColums<T>()
        {
            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Instance
                | BindingFlags.Public);

            csvDataGrid.Columns[0].Width = 30;
            csvDataGrid.Columns[csvDataGrid.Columns.Count - 1].IsReadOnly = true;
            for (int i = 0; i < properties.Length; i++)
            {
                csvDataGrid.Columns[i].Header =
                    properties[i].GetCustomAttribute<HeaderNameAttribute>().HeaderName;
            }
        }

        #region Event handlers
        private void OnParentFrameSizeChanged(object sender, EventArgs e)
        {
            Width = ParentFrame.Width;
            Height = ParentFrame.Height;

            outterBorder.Width = Width;
            outterBorder.Height = Height;
            topDevisionRectangle.Width = Width - 2 * topDevisionRectangle.Margin.Left;
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

        #region Action buttons operations
        private void SaveChangesAsync(object sender, RoutedEventArgs e)
        {
            SaveWindow saveDataWindow = new SaveWindow(MainPageModel.GetModel().TunnelsData,
                CSVService.GetService().Database.FilePath);

            saveDataWindow.ShowDialog();
        }

        private async void DeleteRecordsAsync(object sender, EventArgs e)
        {
            await Task.Run(async () =>
            {
                List<TunnelExitModel> selectedItems = GetSelectedItems().ToList();

                foreach (TunnelExitModel tunnelExit in selectedItems)
                    await Dispatcher.BeginInvoke(new Action(() =>
                        MainPageModel.GetModel().TunnelsData.Remove(tunnelExit)));
            });
        }

        private IEnumerable<TunnelExitModel> GetSelectedItems() =>
           MainPageModel.GetModel().TunnelsData.Where((t) => t.IsSelected == true);
        #endregion
    }
}
