using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AeroViewer.Attributes;

namespace AeroViewer.Models
{
    public class TunnelExit
    {
        [HeaderName("")]
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
        public string IsDamaged { get; set; }
    }
}
