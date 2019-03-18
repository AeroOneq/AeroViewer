using AeroViewer.Attributes;
using AeroViewer.ViewModels;
using System;

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
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public long ID { get; set; }

        [CustomProperty]
        public string IsDamaged { get; set; }
        #endregion

        public TunnelExit() { }
        public TunnelExit(TunnelExitModel tunnelExitModel)
        {
            try
            {
                RowNum = tunnelExitModel.RowNum;
                ID = tunnelExitModel.ID;
                Name = tunnelExitModel.Name;
                AdmArea = tunnelExitModel.AdmArea;
                District = tunnelExitModel.District;
                Tunnel = new Tunnel(tunnelExitModel.TunnelGlobalID, tunnelExitModel.Name);
                Longitude = tunnelExitModel.Latitude;
                Latitude = tunnelExitModel.Longitude;
            }
            catch (Exception)
            {
                throw new ArgumentException("Неправильно введены данные");
            }
        }
    }
}
