using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AeroViewer.Attributes;
using AeroViewer.ViewModels;

namespace AeroViewer.Models
{
    public class TunnelExit
    {
        #region Properties
        public int RowNum { get; set; }

        public string Name { get; set; } = string.Empty;
        public Tunnel Tunnel { get; set; } = new Tunnel();
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
            Tunnel = new Tunnel
            {
                GlobalID = tunnelExitModel.TunnelGlobalID,
                Name = tunnelExitModel.TunnelDescription
            };
            AdmArea = tunnelExitModel.AdmArea;
            District = tunnelExitModel.District;
            Longitude = tunnelExitModel.Longitude;
            Latitude = tunnelExitModel.Latitude;
        }
    }
}
