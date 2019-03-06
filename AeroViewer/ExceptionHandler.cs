using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace AeroViewer
{
    public class ExceptionHandler
    {
        public Dispatcher Dispatcher { get; private set; }

        #region Singleton
        private static ExceptionHandler Handler { get; set; } = new ExceptionHandler();
        public static void UpdateDispatcher(Dispatcher dispatcher) =>
            Handler.Dispatcher = dispatcher;
        public static ExceptionHandler GetHandler() =>
            Handler;
        #endregion

        #region Constructors
        private ExceptionHandler() { }
        #endregion

        public void HandleExceptionWithMessageBox(Exception ex, string title) =>
            AeroViewerMessageBox.ShowMessageBox(title, ex.Message, System.Windows.MessageBoxButton.OK);
    }
}
