using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroViewer.Models
{
    public class Tunnel
    {
        #region Parse constants
        private const string GlobalIDPropertyName = "\"global_id\":" + " ";
        private const string ValuePropertyName = "\"value\":" + " ";
        #endregion

        public string GlobalID { get; set; } 
        public string Name { get; set; }

        public static Tunnel Parse(string tunnelData)
        {
            int globalIDStartIndex = tunnelData.IndexOf(GlobalIDPropertyName) +
                GlobalIDPropertyName.Length;
            int valueStartIndex = tunnelData.IndexOf(ValuePropertyName) +
                ValuePropertyName.Length;

            string globalID = tunnelData.Substring(globalIDStartIndex,
                tunnelData.IndexOf(",") - globalIDStartIndex);
            string value = tunnelData.Substring(valueStartIndex,
                tunnelData.IndexOf("}") - 1 - valueStartIndex);

            return new Tunnel
            {
                GlobalID = globalID,
                Name = value
            };
        }
    }
}
