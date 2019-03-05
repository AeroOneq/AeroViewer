using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using AeroViewer.Models;

namespace AeroViewer.ViewModels
{
    public delegate Task UploadDelegate();
    public class MainPageModel
    {
        #region Properties
        public ObservableCollection<TunnelExit> TunnelsData { get; set; }
        public static UploadDelegate UploadDelegate { get; set; }
        #endregion

        #region Constructors
        private MainPageModel() =>
            TunnelsData = new ObservableCollection<TunnelExit>();
        #endregion

        public void CreateNewTunnelData(List<TunnelExit> tunnelExitsList)
        {
            TunnelsData = new ObservableCollection<TunnelExit>(tunnelExitsList);
        }

        #region Singleton
        private static MainPageModel PageModel { get; set; }
        public static MainPageModel GetModel()
        {
            if (PageModel == null)
                PageModel = new MainPageModel();
            return PageModel;
        }
        #endregion
    }
}
