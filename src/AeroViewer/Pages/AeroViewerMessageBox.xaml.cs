using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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
        /// <summary>
        /// Adds the buttons which were defined by the developer to the messagebox
        /// </summary>
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
    }
}
