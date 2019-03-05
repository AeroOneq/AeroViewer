using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AeroViewer.Models;
using System.IO;

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
        private string FilePath { get; set; }
        #endregion

        #region Constructors
        public Database(string filePath) =>
            FilePath = filePath;
        #endregion

        public async Task<List<TunnelExit>> ReadFileDataAsync()
        {
            return await Task.Run(() =>
            {
                try
                {
                    List<TunnelExit> tunnelExitsList = new List<TunnelExit>();
                    string[] tunnelExitsStringData = File.ReadAllLines(FilePath, Encoding.UTF8);

                    for (int i = 1; i < tunnelExitsStringData.Length; i++)
                        tunnelExitsList.Add(CreateTunnelExitObject(tunnelExitsStringData[i]));

                    return tunnelExitsList;
                }
                catch (OutOfMemoryException ex)
                {
#warning Handle exception
                    return new List<TunnelExit>();
                }
                catch (StackOverflowException ex)
                {
#warning Handle exception
                    return new List<TunnelExit>();
                }
                catch (Exception ex)
                {
#warning Handle exception
                    return new List<TunnelExit>();
                }
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
                    IsDamaged = "OK",
                    IsSelected = false
                };
                return tunnelExit;
            }
            catch (ArgumentNullException)
            {
                tunnelExit.IsDamaged = "Damaged";
                return tunnelExit;
            }
            catch (Exception)
            {
                tunnelExit.IsDamaged = "Damaged";
                return tunnelExit;
            }
        }
    }
}
