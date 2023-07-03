using EDA.Post.Query.Domain.Entities;

namespace EDA.Post.Query.Api.Queries.Handlers
{
    public interface IQueryHandler
    {
        Task<List<PostEntity>> HandleAsync(FindAllPostsQuery query);
    }
}
