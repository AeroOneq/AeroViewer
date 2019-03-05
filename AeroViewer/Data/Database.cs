using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AeroViewer.Models;
using System.IO;
using System.Reflection;
using AeroViewer.Attributes;
using System.Linq;

namespace AeroViewer.Data
{
    public class Database
    {
        #region CSV File constants
        private int IDIndex { get; } = 7;
        private int LatitudeIndex { get; } = 6;
        private int LongitudeIndex { get; } = 5;
        private int DistrictIndex { get; } = 4;
        private int AdmAreaIndex { get; } = 3;
        private int TunnelIndex { get; } = 2;
        private int NameIndex { get; } = 1;
        private int RowNumIndex { get; } = 0;
        #endregion

        #region Properties
        public string FilePath { get; } = string.Empty;
        #endregion

        #region Constructors
        public Database() { }
        public Database(string filePath) =>
            FilePath = filePath;
        #endregion

        public async Task<List<TunnelExit>> ReadFileDataAsync()
        {
            return await Task.Run(() =>
            {
                List<TunnelExit> tunnelExitsList = new List<TunnelExit>();
                string[] tunnelExitsStringData = File.ReadAllLines(FilePath, Encoding.UTF8);

                for (int i = 1; i < tunnelExitsStringData.Length; i++)
                    tunnelExitsList.Add(CreateTunnelExitObject(tunnelExitsStringData[i]));

                return tunnelExitsList;
            });
        }
        private TunnelExit CreateTunnelExitObject(string tunnelExitString)
        {
            TunnelExit tunnelExit = new TunnelExit();
            try
            {
                string[] tunnelExitStringData = tunnelExitString.Split(';');
                tunnelExit = new TunnelExit
                {
                    ID = tunnelExitStringData[IDIndex],
                    RowNum = int.Parse(tunnelExitStringData[RowNumIndex]),
                    Name = tunnelExitStringData[NameIndex],
                    Tunnel = Tunnel.Parse(tunnelExitStringData[TunnelIndex]),
                    AdmArea = tunnelExitStringData[AdmAreaIndex],
                    District = tunnelExitStringData[DistrictIndex],
                    Latitude = tunnelExitStringData[LatitudeIndex],
                    Longitude = tunnelExitStringData[LongitudeIndex],
                    IsDamaged = "OK"
                };
                return tunnelExit;
            }
            catch (ArgumentNullException)
            {
                tunnelExit.IsDamaged = "Damaged";
                return tunnelExit;
            }
            catch (Exception ex)
            {
                tunnelExit.IsDamaged = "Damaged";
                return tunnelExit;
            }
        }

        public async Task WriteFileDataAsync(List<TunnelExit> tunnelExits)
        {
            await Task.Run(() =>
            {
                string[][] data = new string[tunnelExits.Count][];
                PropertyInfo[] modelProperties = typeof(TunnelExit).GetProperties(
                    BindingFlags.Public | BindingFlags.Instance);

                for (int i = 0; i < tunnelExits.Count; i++)
                {
                    List<string> dataString = new List<string>();
                    foreach (PropertyInfo propertyInfo in modelProperties)
                        if (propertyInfo.GetCustomAttribute<CustomPropertyAttribute>() == null)
                            dataString.Add(propertyInfo.GetValue(tunnelExits[i]).ToString());
                    data[i] = dataString.ToArray();
                }

                string[] csvData = data.Select(x => string.Join(";", x)).ToArray();
                File.WriteAllLines(FilePath, csvData, Encoding.UTF8);
            });
        }
    }
}
