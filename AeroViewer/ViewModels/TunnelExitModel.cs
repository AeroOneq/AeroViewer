using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AeroViewer.Models;

namespace AeroViewer.ViewModels
{
    public class TunnelExitModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string TunnelData { get; set; }
        public string AdmArea { get; set; }
        public string District { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }

        public TunnelExitModel() { }
        public TunnelExitModel(TunnelExit tunnelExit)
        {
            ID = tunnelExit.ID;
            Name = tunnelExit.Name;
            TunnelData = $"Имя: {tunnelExit.Tunnel.Name}\n " +
                $"GobalID {tunnelExit.Tunnel.GlobalID}";
            AdmArea = tunnelExit.AdmArea;
            District = tunnelExit.District;
            Longitude = tunnelExit.Longitude;
            Latitude = tunnelExit.Latitude;
        }
    }
}
