using System;

namespace AeroViewer.Attributes
{
    public class HeaderNameAttribute : Attribute
    {
        public string HeaderName { get; set; }

        public HeaderNameAttribute(string headerName) =>
            HeaderName = headerName;
    }
}
