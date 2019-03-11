using System.Collections.Generic;
using System.Threading.Tasks;

namespace AeroViewer.Interfaces
{
    /// <summary>
    /// Interface which defines the methods which must be implemented by all
    /// services that work with files
    /// </summary>
    /// <typeparam name="T">File data model class</typeparam>
    interface IFileService<T>
    {
        Task<List<T>> ReadAsync();
        Task RewriteAsync(List<T> data);
        Task AppendAsync(List<T> data, bool? addHeaderString);
    }
}
