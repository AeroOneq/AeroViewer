using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroViewer.Models
{
    public class TunnelExit
    {
        public string ID { get; set; }
        public string Name { get; set; } 
        public Tunnel Tunnel { get; set; } 
        public string AdmArea { get; set; } 
        public string District { get; set; } 
        public string Longitude { get; set; }
        public string Latitude { get; set; }

        public bool IsDamaged { get; set; }
    }
}
