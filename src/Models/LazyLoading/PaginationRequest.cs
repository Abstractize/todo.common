namespace Models.Common.LazyLoading;

public class PaginationRequest<TSearch, TSortBy>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public TSearch? Search { get; set; }
    public TSortBy? SortBy { get; set; }
    public bool Descending { get; set; } = false;

    public int Skip => (Page - 1) * PageSize;
}