using System.Globalization;
using System.Threading;
using System.Windows;

namespace AeroViewer
{
    public partial class MainWindow : Window
    {
        public static MainWindow Window { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Window = this;

            Position.UpdateMainWindow(this);

            menuFrame.Content = new LeftMenuPage(menuFrame);
            mainFrame.Content = new MainPage(mainFrame);
        }

        #region Size changed methods
        private void RepositionElements(object sender, SizeChangedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
                Position.UpdateMainWindow(this);

            Position.MainWindowPosition.RepositionMainWindowElemets();
        }
        #endregion
    }
}
