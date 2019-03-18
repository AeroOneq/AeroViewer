namespace AeroViewer.Models
{
    public class Tunnel
    {
        #region Parse properties
        private static string GlobalIDPropertyName { get; } = "global_id:";
        private static string ValuePropertyName { get; } = "value:";
        #endregion

        #region Properties
        public long GlobalID { get; set; }
        public string Name { get; set; } = string.Empty;
        #endregion

        #region Constructors
        public Tunnel() { }
        public Tunnel(long globalID, string name)
        {
            GlobalID = globalID;
            Name = name;
        }
        #endregion

        /// <summary>
        /// Creates a tunnel object from a string from CSV file
        /// </summary>
        public static Tunnel Parse(string tunnelData)
        {
            int globalIDStartIndex = tunnelData.IndexOf(GlobalIDPropertyName) +
                GlobalIDPropertyName.Length;
            int valueStartIndex = tunnelData.IndexOf(ValuePropertyName) +
                ValuePropertyName.Length;

            long globalID = long.Parse(tunnelData.Substring(globalIDStartIndex,
                tunnelData.IndexOf(",") - globalIDStartIndex));
            string value = tunnelData.Substring(valueStartIndex,
                tunnelData.IndexOf("}") - valueStartIndex);

            return new Tunnel
            {
                GlobalID = globalID,
                Name = value
            };
        }

        /// <summary>
        /// Returns the data representation in a format which is defined in an initial CSV file
        /// </summary>
        public override string ToString() =>
            "\"{ \"\"global_id\"\": " + GlobalID + ", \"\"value\"\": \"\"" + Name + "\"\" }\"";
    }
}
