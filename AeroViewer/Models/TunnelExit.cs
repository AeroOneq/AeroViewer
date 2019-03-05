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

        public string Name { get; set; }
        public Tunnel Tunnel { get; set; }
        public string AdmArea { get; set; }
        public string District { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string ID { get; set; }

        [CustomProperty]
        public string IsDamaged { get; set; }
        #endregion

        public TunnelExit() { }
        public TunnelExit(TunnelExitModel tunnelExitModel)
        {
            RowNum = tunnelExitModel.RowNum;
            ID = tunnelExitModel.ID;
            Name = tunnelExitModel.Name;
            Tunnel = tunnelExitModel.Tunnel;
            AdmArea = tunnelExitModel.AdmArea;
            District = tunnelExitModel.District;
            Longitude = tunnelExitModel.Longitude;
            Latitude = tunnelExitModel.Latitude;
        }
    }
}
