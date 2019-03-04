using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AeroViewer.Data;
using AeroViewer.Interfaces;
using AeroViewer.Models;

namespace AeroViewer.Services
{
    public class CSVService : IFileService<TunnelExit>
    {
        #region Properties
        private Database Database { get; }
        #endregion

        #region Constructors
        public CSVService(string filePath) =>
            Database = new Database(filePath);
        #endregion
    
        public async Task<List<TunnelExit>> Read() => 
            await Database.ReadFileDataAsync();
    }
}
