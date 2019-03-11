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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AeroViewer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Position Position { get; }
        public MainWindow()
        {
            InitializeComponent();

            Position = Position.MainWindowPosition;
            Position.UpdateMainWindow(this);
            
            menuFrame.Content = new LeftMenuPage(menuFrame);
            mainFrame.Content = new MainPage(mainFrame);
        }

        #region Size changed methods
        private void RepositionELements(object sender, SizeChangedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
                Position.UpdateMainWindow(this);

            Position.RepositionMainWindowElemets();
        }
        #endregion
    }
}
