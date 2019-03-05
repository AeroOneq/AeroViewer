using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public string ID { get; set; }
        [HeaderName("Имя")]
        public string Name { get; set; }
        [HeaderName("Туннель")]
        public Tunnel Tunnel { get; set; }
        [HeaderName("Округ")]
        public string AdmArea { get; set; }
        [HeaderName("Район")]
        public string District { get; set; }
        [HeaderName("Широта")]
        public string Longitude { get; set; }
        [HeaderName("Долгота")]
        public string Latitude { get; set; }

        [HeaderName("Статус загрузки")]
        [CustomProperty]
        public string IsDamaged { get; set; }

        public TunnelExitModel() { }
        public TunnelExitModel(TunnelExit tunnelExit)
        {
            IsSelected = false;

            RowNum = tunnelExit.RowNum;
            ID = tunnelExit.ID;
            Name = tunnelExit.Name;
            Tunnel = tunnelExit.Tunnel;
            AdmArea = tunnelExit.AdmArea;
            District = tunnelExit.District;
            Longitude = tunnelExit.Longitude;
            Latitude = tunnelExit.Latitude;
            IsDamaged = tunnelExit.IsDamaged;
        }
    }
}
