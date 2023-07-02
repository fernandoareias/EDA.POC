using EDA.Core.Queries;

namespace EDA.Post.Query.Api.Queries
{
    public class FindPostsWithLikesQuery : BaseQuery
    {
        public int NumberOfLikes { get; set; }
    }
}
