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
        private char Devider { get; } = ';';

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
                List<string> tunnelExitsStringData = ReadFileData(FilePath);

                ClearInputData(tunnelExitsStringData);

                FirstLine = tunnelExitsStringData[0].Split(Devider);
                InitialColCount = FirstLine.Length;

                for (int i = 1; i < tunnelExitsStringData.Count; i++)
                    tunnelExitsList.Add(CreateTunnelExitObject(tunnelExitsStringData[i]));

                return tunnelExitsList;
            });
        }

        private List<string> ReadFileData(string filePath)
        {
            List<string> stringData = new List<string>();

            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                    stringData.Add(line);
            }

            return stringData;
        }

        private void ClearInputData(List<string> tunnelExitsStringData)
        {
            for (int i = 0; i < tunnelExitsStringData.Count; i++)
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
                string[] tunnelExitStringData = tunnelExitString.Split(Devider);
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
            catch (Exception)
            {
                tunnelExit.IsDamaged = "Damaged";
                return tunnelExit;
            }
        }

        private void CheckAndCorrectInputStringData(string[] tunnelExitStringData)
        {
            if (tunnelExitStringData.Length != InitialColCount)
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

                WriteData(csvData);
            });
        }

        private void WriteData(string[] data)
        {
            using (FileStream fs = new FileStream(FilePath, FileMode.Create))
            using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
            {
                foreach (string dataString in data)
                    sw.WriteLine(dataString);
            }
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
                    {
                        string propertyStringValue = DeleteDeviders(propertyInfo.GetValue(
                            tunnelExits[i - startIndex]).ToString());

                        dataString.Add(propertyStringValue);
                    }

                data[i] = dataString.ToArray();
            }
            return data;
        }

        private string DeleteDeviders(string propertyStringValue)
        {
            while (propertyStringValue.IndexOf(Devider) > -1)
                propertyStringValue = propertyStringValue.Remove(propertyStringValue.IndexOf(Devider), 1);

            return propertyStringValue;
        }

        public async Task AppendDataAsync(List<TunnelExit> tunnelExits, bool? addHeaderString)
        {
            await Task.Run(() =>
            {
                string[][] data = GetStringData(tunnelExits, addHeaderString);
                string[] csvData = data.Select(x => string.Join(";", x)).ToArray();

                AppendData(csvData);
            });
        }

        private void AppendData(string[] data)
        {
            using (FileStream fs = new FileStream(FilePath, FileMode.Append))
            using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
            {
                foreach (string dataString in data)
                    sw.WriteLine(dataString);
            }
        }
    }
}
