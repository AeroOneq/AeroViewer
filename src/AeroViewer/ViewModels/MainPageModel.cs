﻿    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Collections.ObjectModel;
    using AeroViewer.Models;
    using AeroViewer.Services;

    namespace AeroViewer.ViewModels
    {
        public delegate Task UploadDelegate();
        public class MainPageModel
        {
            #region View Model Properties
            public string ShortDocumentName { get; set; } = "Название документа";
            public string FullDocumentName { get; set; } = "Название документа";
            public ObservableCollection<TunnelExitModel> TunnelsData { get; set; }
            #endregion

            #region Usual properties
            /// <summary>
            /// Delegate which runs after creating a list of TunnelExitModels,
            /// basicly the function of this delegate is to upload the data to the UI
            /// </summary>
            public static UploadDelegate UploadDelegate { get; set; }
            public int NumberOfDamagedRecords { get; set; } = 0;
            public Dictionary<string, List<string>> DistrictCountDictionary { get; set; }
            #endregion

            #region Constructors
            private MainPageModel() =>
                TunnelsData = new ObservableCollection<TunnelExitModel>();
            #endregion

            /// <summary>
            /// Creates a list of TunnelExitModel objets based on the TunnelExit list
            /// </summary>
            /// <returns>The number of damaged records</returns>
            public void CreateNewTunnelData(List<TunnelExit> tunnelExitsList)
            {
                InitializePropeties();

                foreach (TunnelExit tunnelExit in tunnelExitsList)
                {
                    if (tunnelExit.IsDamaged == "Damaged")
                        NumberOfDamagedRecords++;
                    else
                        TunnelsData.Add(new TunnelExitModel(tunnelExit));


                    if (DistrictCountDictionary.ContainsKey(tunnelExit.AdmArea))
                    {
                        if (DistrictCountDictionary[tunnelExit.AdmArea].FindIndex(d => d == tunnelExit.District) == -1)
                            DistrictCountDictionary[tunnelExit.AdmArea].Add(tunnelExit.District);
                    }
                    else
                    {
                        DistrictCountDictionary[tunnelExit.AdmArea] = new List<string>();
                        DistrictCountDictionary[tunnelExit.AdmArea].Add(tunnelExit.District);
                    }
                }
            }

            private void InitializePropeties()
            {
                PageModel.TunnelsData = new ObservableCollection<TunnelExitModel>();
                NumberOfDamagedRecords = 0;
                DistrictCountDictionary = new Dictionary<string, List<string>>();
                PageModel.FullDocumentName = CSVService.CSVServiceObject.Database.FilePath;
                PageModel.ShortDocumentName = PageModel.FullDocumentName.Substring(
                    PageModel.FullDocumentName.LastIndexOf("\\") + 1);
            }

            #region Singleton
            private static MainPageModel mainPageModel;
            public static MainPageModel PageModel
            {
                get
                {
                    if (mainPageModel == null)
                        mainPageModel = new MainPageModel();
                    return mainPageModel;
                }
            }
            #endregion
        }
    }
