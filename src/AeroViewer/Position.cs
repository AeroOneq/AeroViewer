﻿using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AeroViewer
{
    internal class Position
    {
        #region Constants
        private const double LeftGridWidth = 250;
        private const double MainGridLeftMargin = 10;
        #endregion

        #region Singleton
        private static Position mainWindowPosition;
        public static Position MainWindowPosition
        {
            get
            {
                if (mainWindowPosition == null)
                    mainWindowPosition = new Position();
                return mainWindowPosition;
            }
        }
        #endregion

        #region Properties
        private MainWindow mainWindow;
        private MainWindow MainWindow
        {
            get => mainWindow;
            set
            {
                mainWindow = value;
                SetMaximaziedParams();
            }
        }
        private void SetMaximaziedParams()
        {
            MainWindow.Width = SystemParameters.MaximizedPrimaryScreenWidth;
            MainWindow.Height = SystemParameters.MaximizedPrimaryScreenHeight;
            MainWindow.Top = 0;
            MainWindow.Left = 0;
        }
        #endregion

        #region Constructors
        private Position() { }
        #endregion

        public static void UpdateMainWindow(MainWindow mainWindow) =>
            MainWindowPosition.MainWindow = mainWindow;
        #region Size changed methods
        public void RepositionMainWindowElemets()
        {
            RepositionGrids();
            RepositionFrames();
        }

        private void RepositionGrids()
        {
            Grid mainGrid = GetMainGrid();
            Grid backgroundGrid = GetBackGroundGrid();
            Grid leftGrid = GetLeftGrid();

            backgroundGrid.Width = MainWindow.Width -
                (SystemParameters.MaximizedPrimaryScreenWidth - SystemParameters.FullPrimaryScreenWidth);
            backgroundGrid.Height = MainWindow.Height - 
                (SystemParameters.MaximizedPrimaryScreenHeight - SystemParameters.FullPrimaryScreenHeight);
            leftGrid.Height = backgroundGrid.Height;
            mainGrid.Width = backgroundGrid.Width - LeftGridWidth - MainGridLeftMargin;
            mainGrid.Height = backgroundGrid.Height;
            mainGrid.Margin = new Thickness(LeftGridWidth + MainGridLeftMargin, 0, 0, 0);
        }

        private Grid GetLeftGrid()
        {
            Grid backgroundGrid = MainWindow.Content as Grid;
            List<Grid> mainWindowGrids = backgroundGrid.Children.OfType<Grid>().ToList();
            return mainWindowGrids.Find(g => g.Name == "leftGrid");
        }

        private Grid GetBackGroundGrid() => MainWindow.Content as Grid;

        private Grid GetMainGrid()
        {
            Grid backgroundGrid = MainWindow.Content as Grid;
            List<Grid> mainWindowGrids = backgroundGrid.Children.OfType<Grid>().ToList();
            return mainWindowGrids.Find(g => g.Name == "mainGrid");
        }

        private void RepositionFrames()
        {
            Grid leftGrid, mainGrid;
            Frame menuFrame = GetInnerFrame(leftGrid = GetLeftGrid());
            Frame mainFrame = GetInnerFrame(mainGrid = GetMainGrid());

            menuFrame.Width = leftGrid.Width;
            menuFrame.Height = leftGrid.Height;
            mainFrame.Width = mainGrid.Width;
            mainFrame.Height = mainGrid.Height;
        }

        private Frame GetInnerFrame(Grid grid) => grid.Children[0] as Frame;
        #endregion
    }
}
