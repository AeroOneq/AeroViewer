using System.Collections.Generic;
using System.Threading.Tasks;
using AeroViewer.Data;
using AeroViewer.Interfaces;
using AeroViewer.Models;

namespace AeroViewer.Services
{
    /// <summary>
    /// Service which works with CSV files
    /// </summary>
    public class CSVService : IFileService<TunnelExit>
    {
        #region Properties
        public Database Database { get; } = new Database();

        private static CSVService csvService;
        public static CSVService CSVServiceObject
        {
            get
            {
                if (csvService == null)
                    csvService = new CSVService();
                return csvService;
            }
        }
        #endregion

        #region Constructors
        private CSVService() { }
        #endregion

        public static void UpdateFilePath(string filePath) =>
            CSVServiceObject.Database.FilePath = filePath;

        #region IFileService implementation
        public async Task<List<TunnelExit>> ReadAsync() =>
            await Database.ReadFileDataAsync();

        public async Task RewriteAsync(List<TunnelExit> tunnelExits) =>
            await Database.RewriteDataAsync(tunnelExits);

        public async Task AppendAsync(List<TunnelExit> tunnelExits, bool? addHeaderString) =>
            await Database.AppendDataAsync(tunnelExits, addHeaderString);
        #endregion
    }
}
