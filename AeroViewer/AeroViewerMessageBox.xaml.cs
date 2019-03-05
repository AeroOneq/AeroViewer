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
using System.Windows.Shapes;

namespace AeroViewer
{
    public partial class AeroViewerMessageBox : Window
    {
        #region Properties
        private MessageBoxResult MessageBoxResult { get; set; } = MessageBoxResult.None;
        #endregion

        public AeroViewerMessageBox()
        {
            InitializeComponent();
        }

        public static MessageBoxResult ShowMessageBox(string title, string message,
            MessageBoxButton buttons)
        {
            AeroViewerMessageBox messageBox = new AeroViewerMessageBox { Title = title };
            messageBox.messageTextBlock.Text = message;
            messageBox.AddMessageBoxButtons(buttons);
            messageBox.ShowDialog();
            return messageBox.MessageBoxResult;
        }

        #region Add buttons methods
        public void AddMessageBoxButtons(MessageBoxButton buttons)
        {
            switch (buttons)
            {
                case MessageBoxButton.OK:
                    AddMessageBoxButton("OK", MessageBoxResult.OK, isDefault: true);
                    break;

                case MessageBoxButton.OKCancel:
                    AddMessageBoxButton("Cancel", MessageBoxResult.Cancel,
                        isCancel: true);
                    AddMessageBoxButton("OK", MessageBoxResult.OK, isDefault: true);
                    break;

                case MessageBoxButton.YesNo:
                    AddMessageBoxButton("Yes", MessageBoxResult.Yes, isDefault: true);
                    AddMessageBoxButton("No", MessageBoxResult.No);
                    break;

                case MessageBoxButton.YesNoCancel:
                    AddMessageBoxButton("No", MessageBoxResult.No);
                    AddMessageBoxButton("Yes", MessageBoxResult.Yes, isDefault: true);
                    AddMessageBoxButton("Cancel", MessageBoxResult.Cancel,
                        isCancel: true);
                    break;
            }
        }
        private void AddMessageBoxButton(string text, MessageBoxResult messageBoxResult,
            bool isCancel = false, bool isDefault = false)
        {
            Button button = new Button()
            {
                Content = text,
                IsCancel = isCancel,
                IsDefault = isDefault
            };
            button.Click += (sender, e) =>
            {
                MessageBoxResult = messageBoxResult;
                DialogResult = true;
            };
            buttonsGrid.Children.Add(button);
        }
        #endregion
        #region Event handlers
        private void MessageBoxBtnMouseEnter(object sender, MouseEventArgs e)
        {
            Button button = sender as Button;
            button.Background = new SolidColorBrush(Color.FromRgb(34, 139, 34));
            button.Foreground = new SolidColorBrush(Colors.White);
        }
        private void MessageBoxBtnMouseLeave(object sender, MouseEventArgs e)
        {
            Button button = sender as Button;
            button.Background = new SolidColorBrush(Colors.White);
            button.Foreground = new SolidColorBrush(Color.FromRgb(34, 139, 34));
        }
        #endregion
    }
}
