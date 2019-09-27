using System.Collections;
using System.ComponentModel;

namespace AeroViewer.ViewModels
{
    public class AdmAreaSort<T> : IComparer
        where T : TunnelExitModel
    {
        private ListSortDirection ListSortDirection { get; }

        public AdmAreaSort(ListSortDirection listSortDirection) =>
            ListSortDirection = listSortDirection;

        public int Compare(object x, object y)
        {
            T xT = x as T;
            T yT = y as T;

            if (!(MainPageModel.PageModel.DistrictCountDictionary.ContainsKey(xT.AdmArea) &&
                MainPageModel.PageModel.DistrictCountDictionary.ContainsKey(yT.AdmArea)))
                return 0;

            if (MainPageModel.PageModel.DistrictCountDictionary[xT.AdmArea].Count >
                MainPageModel.PageModel.DistrictCountDictionary[yT.AdmArea].Count)
            {
                return -1 * ((ListSortDirection == ListSortDirection.Descending) ? -1 : 1);
            }

            if (MainPageModel.PageModel.DistrictCountDictionary[xT.AdmArea].Count <
                MainPageModel.PageModel.DistrictCountDictionary[yT.AdmArea].Count)
            {
                return 1 * ((ListSortDirection == ListSortDirection.Descending) ? -1 : 1);
            }

            return 0;
        }
    }
}
