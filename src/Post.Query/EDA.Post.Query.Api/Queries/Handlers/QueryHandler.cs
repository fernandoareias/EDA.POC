using EDA.Post.Query.Domain.Entities;
using EDA.Post.Query.Domain.Repositories;

namespace EDA.Post.Query.Api.Queries.Handlers
{
    public class QueryHandler : IQueryHandler
    {
        private readonly IPostRepository _postRepository;

        public QueryHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<List<PostEntity>> HandleAsync(FindAllPostsQuery query)
        {
            return await _postRepository.GetAllAsync();
        }
    }
}
