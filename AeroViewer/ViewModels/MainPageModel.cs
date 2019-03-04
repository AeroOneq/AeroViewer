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
        public ObservableCollection<TunnelExitModel> TunnelsData { get; set; }
        public static UploadDelegate UploadDelegate { get; set; }
        #endregion

        #region Constructors
        private MainPageModel() =>
            TunnelsData = new ObservableCollection<TunnelExitModel>();
        #endregion

        public void CreateNewTunnelData(List<TunnelExit> tunnelExitsList)
        {
            TunnelsData = new ObservableCollection<TunnelExitModel>();

            for (int i = 1; i < tunnelExitsList.Count; i++)
                TunnelsData.Add(new TunnelExitModel(tunnelExitsList[i]));
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
