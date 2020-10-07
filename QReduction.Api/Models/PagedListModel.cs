using System.Collections.Generic;
using QReduction.Core.Models;

namespace QReduction.Apis.Models
{
    public class PagedListModel<T>  where T : class
    {

        #region ctor
        public PagedListModel(int currentPage, int pageSize)
        {
            QueryOptions = new BaseSearchModel()
            {
                CurrentPage = currentPage,
                PageSize = pageSize
            };

            DataList = new List<T>();
        }

        public PagedListModel()
        {
            QueryOptions = new BaseSearchModel();
            DataList = new List<T>();
        }

        #endregion

        public BaseSearchModel QueryOptions { get; set; }
        public IEnumerable<T> DataList { get; set; }
    }
}
