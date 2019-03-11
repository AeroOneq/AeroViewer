using AeroViewer.Attributes;
using AeroViewer.Models;

namespace AeroViewer.ViewModels
{
    public class TunnelExitModel
    {
        [HeaderName("")]
        [CustomProperty]
        public bool IsSelected { get; set; }
        [HeaderName("Номер")]
        public int RowNum { get; set; }

        [HeaderName("ID")]
        public string ID { get; set; } = string.Empty;
        [HeaderName("Имя")]
        public string Name { get; set; } = string.Empty;
        [HeaderName("GlobalID туннеля")]
        public string TunnelGlobalID { get; set; } = string.Empty;
        [HeaderName("Описание туннеля")]
        public string TunnelDescription { get; set; } = string.Empty;
        [HeaderName("Округ")]
        public string AdmArea { get; set; } = string.Empty;
        [HeaderName("Район")]
        public string District { get; set; } = string.Empty;
        [HeaderName("Широта")]
        public string Longitude { get; set; } = string.Empty;
        [HeaderName("Долгота")]
        public string Latitude { get; set; } = string.Empty;

        [HeaderName("Статус загрузки")]
        [CustomProperty]
        public string IsDamaged { get; set; } = string.Empty;

        public TunnelExitModel() { }
        public TunnelExitModel(TunnelExit tunnelExit)
        {
            IsSelected = false;

            RowNum = tunnelExit.RowNum;
            ID = tunnelExit.ID;
            Name = tunnelExit.Name;
            TunnelGlobalID = tunnelExit.Tunnel.GlobalID;
            TunnelDescription = tunnelExit.Tunnel.Name;
            AdmArea = tunnelExit.AdmArea;
            District = tunnelExit.District;
            Longitude = tunnelExit.Longitude;
            Latitude = tunnelExit.Latitude;

            IsDamaged = tunnelExit.IsDamaged;
        }
    }
}
