namespace QReduction.Core.Models
{
    public class BaseSearchModel
    {
        public BaseSearchModel()
        {
            SortField = "Id";
            SearchOrder = SearchOrders.Asc;
            CurrentPage = 1;
            PageSize = 20;
        }

        public string SortField { get; set; }
        public SearchOrders SearchOrder { get; set; }
        public int CurrentPage { get; set; }
        public int TotalRowsCount { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public string SortBy { get { return SortField + " " + SearchOrder.ToString(); } }
    }

    public enum SearchOrders : byte
    {
        Asc = 1,
        Desc = 2
    }
}
