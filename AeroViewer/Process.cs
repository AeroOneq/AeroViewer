using System.Windows.Controls;
using System.Windows.Threading;

namespace AeroViewer
{
    public static class Process
    {
        public static TextBlock StatusTextBox { get; set; } 
        public static Dispatcher Dispatcher { get; set; }

        public static void UpdateStatus(string newStatus) =>
            Dispatcher.Invoke(() => StatusTextBox.Text = newStatus);
    }
}
