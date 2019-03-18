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
    /// <summary>
    /// Class where all methods which directly connect to the CSV file are placed
    /// </summary>
    public class Database
    {
        #region CSV File properties
        private int InitialColCount { get; set; }

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
        public string FilePath { get; set; } = string.Empty;
        /// <summary>
        /// First (Head) line of a file, where column names are
        /// </summary>
        public string[] FirstLine { get; private set; } = new string[1];
        #endregion

        public async Task<List<TunnelExit>> ReadFileDataAsync()
        {
            return await Task.Run(() =>
            {
                List<TunnelExit> tunnelExitsList = new List<TunnelExit>();
                string[] tunnelExitsStringData = File.ReadAllLines(FilePath, Encoding.UTF8);

                ClearInputData(tunnelExitsStringData);

                FirstLine = tunnelExitsStringData[0].Split(';');
                InitialColCount = FirstLine.Length;


                for (int i = 1; i < tunnelExitsStringData.Length; i++)
                    tunnelExitsList.Add(CreateTunnelExitObject(tunnelExitsStringData[i]));

                return tunnelExitsList;
            });
        }

        private void ClearInputData(string[] tunnelExitsStringData)
        {
            for (int i = 0; i < tunnelExitsStringData.Length; i++)
            {
                if (tunnelExitsStringData[i][tunnelExitsStringData[i].Length - 1] == ';')
                {
                    tunnelExitsStringData[i] = tunnelExitsStringData[i].Remove(tunnelExitsStringData[i].Length - 1, 1);
                }
            }
        }

        /// <summary>
        /// Creates a TunnelExit object out of a CSV file string
        /// </summary>
        private TunnelExit CreateTunnelExitObject(string tunnelExitString)
        {
            TunnelExit tunnelExit = new TunnelExit();
            try
            {
                string[] tunnelExitStringData = tunnelExitString.Split(';');
                CheckAndCorrectInputStringData(tunnelExitStringData);

                tunnelExit = new TunnelExit
                {
                    ID = long.Parse(tunnelExitStringData[IDIndex]),
                    RowNum = int.Parse(tunnelExitStringData[RowNumIndex]),
                    Name = tunnelExitStringData[NameIndex],
                    Tunnel = Tunnel.Parse(tunnelExitStringData[TunnelIndex]),
                    AdmArea = tunnelExitStringData[AdmAreaIndex],
                    District = tunnelExitStringData[DistrictIndex],
                    Latitude = double.Parse(tunnelExitStringData[LatitudeIndex].Replace('.', ',')),
                    Longitude = double.Parse(tunnelExitStringData[LongitudeIndex].Replace('.', ',')),
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

        private void CheckAndCorrectInputStringData(string[] tunnelExitStringData)
        {
            if (tunnelExitStringData.Length != InitialColCount)
                throw new ArgumentException();

            for (int i = 0; i < InitialColCount - 1; i++)
                if (string.IsNullOrEmpty(tunnelExitStringData[i]) ||
                    string.IsNullOrWhiteSpace(tunnelExitStringData[i]))
                    throw new ArgumentException();

            for (int i = 0; i < InitialColCount; i++)
            {
                while (tunnelExitStringData[i].IndexOf("\"") > -1)
                    tunnelExitStringData[i] = tunnelExitStringData[i].Remove(
                        tunnelExitStringData[i].IndexOf("\""), 1);
            }
        }



        public async Task RewriteDataAsync(List<TunnelExit> tunnelExits)
        {
            await Task.Run(() =>
            {
                string[][] data = GetStringData(tunnelExits, true);

                string[] csvData = data.Select(x => string.Join(";", x)).ToArray();
                File.WriteAllLines(FilePath, csvData, Encoding.UTF8);
            });
        }

        /// <summary>
        /// Creates a jagged array out of a Tunnel exit list, where each array is data for CSV data string
        /// </summary>
        private string[][] GetStringData(List<TunnelExit> tunnelExits, bool? addHeaderString)
        {
            string[][] data;
            int startIndex = 0;

            if (addHeaderString == true)
            {
                data = new string[tunnelExits.Count + 1][];
                data[0] = FirstLine;
                startIndex = 1;
            }
            else
            {
                data = new string[tunnelExits.Count][];
                startIndex = 0;
            }

            PropertyInfo[] modelProperties = typeof(TunnelExit).GetProperties(
                BindingFlags.Public | BindingFlags.Instance);

            for (int i = startIndex; i < data.GetLength(0); i++)
            {
                List<string> dataString = new List<string>();
                foreach (PropertyInfo propertyInfo in modelProperties)
                    if (propertyInfo.GetCustomAttribute<CustomPropertyAttribute>() == null)
                        dataString.Add(propertyInfo.GetValue(tunnelExits[i - startIndex]).ToString());

                data[i] = dataString.ToArray();
            }
            return data;
        }

        public async Task AppendDataAsync(List<TunnelExit> tunnelExits, bool? addHeaderString)
        {
            await Task.Run(() =>
            {
                string[][] data = GetStringData(tunnelExits, addHeaderString);

                string[] csvData = data.Select(x => string.Join(";", x)).ToArray();
                File.AppendAllLines(FilePath, csvData, Encoding.UTF8);
            });
        }
    }
}
