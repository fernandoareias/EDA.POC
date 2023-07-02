using EDA.Core.Queries;

namespace EDA.Post.Query.Api.Queries
{
    public class FindPostByIdQuery : BaseQuery
    {
        public Guid PostId { get; set; }
    }
}
