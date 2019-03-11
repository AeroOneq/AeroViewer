using AeroViewer.Attributes;
using AeroViewer.ViewModels;

namespace AeroViewer.Models
{
    public class TunnelExit
    {
        #region Properties
        public int RowNum { get; set; }

        public string Name { get; set; } = string.Empty;
        public Tunnel Tunnel { get; set; } 
        public string AdmArea { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string Longitude { get; set; } = string.Empty;
        public string Latitude { get; set; } = string.Empty;
        public string ID { get; set; } = string.Empty;

        [CustomProperty]
        public string IsDamaged { get; set; }
        #endregion

        public TunnelExit() { }
        public TunnelExit(TunnelExitModel tunnelExitModel)
        {
            RowNum = tunnelExitModel.RowNum;
            ID = tunnelExitModel.ID;
            Name = tunnelExitModel.Name;
            AdmArea = tunnelExitModel.AdmArea;
            District = tunnelExitModel.District;
            Tunnel = new Tunnel(tunnelExitModel.TunnelGlobalID, tunnelExitModel.Name);
            Longitude = tunnelExitModel.Longitude;
            Latitude = tunnelExitModel.Latitude;
        }
    }
}
