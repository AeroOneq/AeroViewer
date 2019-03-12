namespace AeroViewer.Models
{
    public class Tunnel
    {
        #region Parse constants
        private static string GlobalIDPropertyName { get; } = "\"\"global_id\"\":";
        private static string ValuePropertyName { get; } = "\"\"value\"\":";
        #endregion

        #region Properties
        public string GlobalID { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        #endregion

        #region Constructors
        public Tunnel() { }
        public Tunnel(string globalID, string name)
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

            string globalID = tunnelData.Substring(globalIDStartIndex,
                tunnelData.IndexOf(",") - globalIDStartIndex);
            string value = tunnelData.Substring(valueStartIndex,
                tunnelData.IndexOf("}") - valueStartIndex);
            value = value.Substring(3, value.Length - 6);

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
