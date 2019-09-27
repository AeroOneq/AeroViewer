using System;

namespace AeroViewer.Attributes
{
    /// <summary>
    /// Attribute which marks properties in a class, which represents CSV data, 
    /// which are added by a programmer (so they are not in a file)
    /// </summary>
    public class CustomPropertyAttribute : Attribute
    {
        public CustomPropertyAttribute() { }
    }
}
