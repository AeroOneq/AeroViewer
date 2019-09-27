using System;

namespace AeroViewer.Attributes
{
    /// <summary>
    /// Stores the name which will displayed in a data grid (as column header)
    /// </summary>
    public class HeaderNameAttribute : Attribute
    {
        public string HeaderName { get; set; }

        public HeaderNameAttribute(string headerName) =>
            HeaderName = headerName;
    }
}
