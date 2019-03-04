using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroViewer.Interfaces
{
    interface IFileService<T>
    {
        Task<List<T>> Read();
    }
}
