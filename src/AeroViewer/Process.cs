using System.Windows.Controls;
using System.Windows.Threading;

namespace AeroViewer
{
    public static class Process
    {
        #region Properties
        public static Loader Loader { get; set; }
        public static TextBlock StatusTextBox { get; set; }
        public static Image SuccessIcon { get; set; }
        public static Image FailIcon { get; set; }
        public static Dispatcher Dispatcher { get; set; }
        #endregion

        /// <summary>
        /// Sets initial status and makes all icons collapsed, only loader and the status
        /// text block are visible
        /// </summary>
        public static void SetInitialStatus(string initialStatus)
        {
            Dispatcher.Invoke(() =>
            {
                StatusTextBox.Text = initialStatus;
                Loader.Visibility = System.Windows.Visibility.Visible;
                SuccessIcon.Visibility = System.Windows.Visibility.Collapsed;
                FailIcon.Visibility = System.Windows.Visibility.Collapsed;
            });
        }

        public static void UpdateStatus(string newStatus) =>
            Dispatcher.Invoke(() => StatusTextBox.Text = newStatus);

        /// <summary>
        /// Sets the final status and displays the result status depends on the isSuccess value
        /// </summary>
        public static void SetFinalStatus(string finalStatus, bool isSuccess)
        {
            Dispatcher.Invoke(() =>
            {
                StatusTextBox.Text = finalStatus;
                Loader.Visibility = System.Windows.Visibility.Collapsed;

                if (isSuccess)
                {
                    SuccessIcon.Visibility = System.Windows.Visibility.Visible;
                    FailIcon.Visibility = System.Windows.Visibility.Collapsed;
                    return;
                }
                SuccessIcon.Visibility = System.Windows.Visibility.Collapsed;
                FailIcon.Visibility = System.Windows.Visibility.Visible;
            });
        }
    }
}
