using EDA.Core.Queries;

namespace EDA.Post.Query.Api.Queries
{
    public class FindPostsByAuthorQuery : BaseQuery
    {
        public string Author { get; set; }
    }
}
