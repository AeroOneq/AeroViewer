using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace AeroViewer
{
    /// <summary>
    /// Class which handles all the exceptions which can occur during runtime
    /// </summary>
    public class ExceptionHandler
    {
        public Dispatcher Dispatcher { get; private set; }

        #region Singleton
        private static ExceptionHandler exceptionHandler;

        public static ExceptionHandler Handler
        {
            get
            {
                if (exceptionHandler == null)
                    exceptionHandler = new ExceptionHandler();
                return exceptionHandler;
            }
        }

        public static void UpdateDispatcher(Dispatcher dispatcher) =>
            Handler.Dispatcher = dispatcher;
        #endregion

        #region Constructors
        private ExceptionHandler() { }
        #endregion

        public void HandleExceptionWithMessageBox(Exception ex, string title) =>
            AeroViewerMessageBox.ShowMessageBox(title, ex.Message, System.Windows.MessageBoxButton.OK);
    }
}
