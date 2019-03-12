using System.Collections;
using System.ComponentModel;

namespace AeroViewer.ViewModels
{
    public class AdmAreaSort<T> : IComparer
        where T : TunnelExitModel
    {
        private ListSortDirection ListSortDirection { get; }

        public AdmAreaSort(ListSortDirection listSortDirection)
        {
            ListSortDirection = listSortDirection;
        }

        public int Compare(object x, object y)
        {
            T xT = x as T;
            T yT = y as T;

            if (MainPageModel.PageModel.DistrictCountDictionary[xT.AdmArea] >
                MainPageModel.PageModel.DistrictCountDictionary[yT.AdmArea])
                return -1 * ((ListSortDirection == ListSortDirection.Descending) ? -1 : 1);

            if (MainPageModel.PageModel.DistrictCountDictionary[xT.AdmArea] <
                MainPageModel.PageModel.DistrictCountDictionary[yT.AdmArea])
                return 1 * ((ListSortDirection == ListSortDirection.Descending) ? -1 : 1);

            return 0;
        }
    }
}
