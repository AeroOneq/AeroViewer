﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using AeroViewer.ViewModels;
using AeroViewer.Attributes;
using System.Reflection;
using System.Collections.ObjectModel;
using AeroViewer.Services;

namespace AeroViewer
{
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
                {
                    csvDataGrid.ItemsSource = MainPageModel.GetModel().TunnelsData;
                    fileNameTextBlock.Text = MainPageModel.GetModel().DocumentName;
                }));
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
            SaveWindow saveDataWindow = new SaveWindow(csvDataGrid.ItemsSource as ObservableCollection<TunnelExitModel>,
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

        public void AddNewRecord(object sender, EventArgs e) =>
            MainPageModel.GetModel().TunnelsData.Add(new TunnelExitModel()
            {
                RowNum = MainPageModel.GetModel().TunnelsData[
                    MainPageModel.GetModel().TunnelsData.Count - 1].RowNum + 1,
                IsDamaged = "OK",
            });

        #region Filter methods
        private async void FindAllRecords(object sender, TextChangedEventArgs e)
        {
            PropertyInfo[] propertyInfos = typeof(TunnelExitModel).GetProperties(BindingFlags.Instance |
                BindingFlags.Public);

            string filterText = filterTextBox.Text;
            int selectionMode = filterComboBox.SelectedIndex;
            await Task.Run(async () =>
            {
                Process.SetInitialStatus("Поиск записей");

                ObservableCollection<TunnelExitModel> suitableObjects =
                    GetAllSuitableRecords(selectionMode, propertyInfos, filterText);

                Process.UpdateStatus("Отображение записей");
                await Dispatcher.BeginInvoke(new Action(() =>
                    csvDataGrid.ItemsSource = suitableObjects));
                Process.SetFinalStatus("Записи отображены", true);
            });
        }

        private ObservableCollection<TunnelExitModel> GetAllSuitableRecords(
            int selectionMode, PropertyInfo[] propertyInfos, string filterText)
        {
            ObservableCollection<TunnelExitModel> suitableObjects =
                new ObservableCollection<TunnelExitModel>();

            switch (selectionMode)
            {
                case 0:
                    suitableObjects = CreateTunnelExitsList(string.Empty, propertyInfos,
                        filterText);
                    break;
                case 1:
                    suitableObjects = CreateTunnelExitsList("AdmArea", propertyInfos,
                        filterText);
                    break;
                case 2:
                    suitableObjects = CreateTunnelExitsList("TunnelGlobalID", propertyInfos,
                        filterText);
                    break;
            }

            return suitableObjects;
        }

        private ObservableCollection<TunnelExitModel> CreateTunnelExitsList(
            string selectedProperty, PropertyInfo[] propertyInfos, string filterText)
        {
            ObservableCollection<TunnelExitModel> suitableObjects =
                new ObservableCollection<TunnelExitModel>();

            foreach (TunnelExitModel tunnelExitModel in MainPageModel.GetModel().TunnelsData)
            {
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    if (propertyInfo.GetValue(tunnelExitModel).ToString().IndexOf(filterText) > -1
                        && propertyInfo.GetCustomAttribute<CustomPropertyAttribute>() == null
                        && CheckProperty(selectedProperty, propertyInfo))
                    {
                        suitableObjects.Add(tunnelExitModel);
                        break;
                    }
                }
            }

            return suitableObjects;
        }

        private bool CheckProperty(string selectedProperty, PropertyInfo propertyInfo)
        {
            if (selectedProperty == string.Empty)
                return true;
            if (selectedProperty == propertyInfo.Name)
                return true;
            return false;
        }
        #endregion
        #endregion
    }
}
