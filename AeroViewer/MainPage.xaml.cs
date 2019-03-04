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
using AeroViewer.ViewModels;
using System.Reflection;

namespace AeroViewer
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public Frame ParentFrame { get; set; }

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
    }
}
