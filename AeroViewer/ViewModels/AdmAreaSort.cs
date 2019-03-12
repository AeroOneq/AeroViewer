using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace AeroViewer.ViewModels
{
    public class AdmAreaSort<T> : IComparer
        where T : TunnelExitModel
    {
        private readonly ListSortDirection listSortDirection;

        public AdmAreaSort(ListSortDirection listSortDirection)
        {
            this.listSortDirection = listSortDirection;
        }

        public int Compare(object x, object y)
        {
            T xT = x as T;
            T yT = y as T;

            if (MainPageModel.PageModel.DistrictCountDictionary[xT.AdmArea] >
                MainPageModel.PageModel.DistrictCountDictionary[yT.AdmArea])
                return -1 * ((listSortDirection == ListSortDirection.Descending) ? -1 : 1);

            if (MainPageModel.PageModel.DistrictCountDictionary[xT.AdmArea] <
                MainPageModel.PageModel.DistrictCountDictionary[yT.AdmArea])
                return 1 * ((listSortDirection == ListSortDirection.Descending) ? -1 : 1);

            return 0;
        }
    }
}
