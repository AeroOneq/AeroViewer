using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AeroViewer.Data;
using AeroViewer.Interfaces;
using AeroViewer.Models;
using System.Collections.ObjectModel;
using System.Reflection;
using AeroViewer.Attributes;

namespace AeroViewer.Services
{
    public class CSVService : IFileService<TunnelExit>
    {
        #region Properties
        public Database Database { get; } = new Database();
        private static CSVService CSVServiceObject { get; set; } = new CSVService();
        #endregion

        #region 
        public static CSVService GetService() =>
            CSVServiceObject;
        public static void UpdateFilePath(string filePath) =>
            CSVServiceObject = new CSVService(filePath);
        #endregion

        #region Constructors
        private CSVService() { }
        private CSVService(string filePath) =>
            Database = new Database(filePath);
        #endregion

        public async Task<List<TunnelExit>> Read() =>
            await Database.ReadFileDataAsync();

        public async Task Write(List<TunnelExit> tunnelExits) =>
            await Database.WriteFileDataAsync(tunnelExits);
    }
}
