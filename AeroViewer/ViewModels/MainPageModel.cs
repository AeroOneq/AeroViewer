using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using AeroViewer.Models;
using System.Reflection;

namespace AeroViewer.ViewModels
{
    public delegate Task UploadDelegate();
    public class MainPageModel
    {
        #region View Model Properties
        public string DocumentName { get; set; } = "Название документа";
        public ObservableCollection<TunnelExitModel> TunnelsData { get; set; }
        #endregion

        #region Usual properties
        public static UploadDelegate UploadDelegate { get; set; }
        #endregion

        #region Constructors
        private MainPageModel() =>
            TunnelsData = new ObservableCollection<TunnelExitModel>();
        #endregion

        public void CreateNewTunnelData(List<TunnelExit> tunnelExitsList)
        {
            TunnelsData = new ObservableCollection<TunnelExitModel>();

            foreach (TunnelExit tunnelExit in tunnelExitsList)
                TunnelsData.Add(new TunnelExitModel(tunnelExit));
        }
        public string[][] GetStringArray()
        {

            return new string[TunnelsData.Count][];
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
