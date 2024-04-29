namespace RedditClone.Application.Common.Extensions;

public class PaginationHandler()
{
    public static (IList<T>, int) ApplyPagination<T>(IList<T> list, int page, int pageSize)
    {
        var totalPages = (int)Math.Ceiling((double)list.Count / pageSize);

        list.Skip(pageSize * (page - 1)).Take(pageSize).ToList();

        return (list, totalPages);
    }
}