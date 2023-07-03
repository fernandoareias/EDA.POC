using EDA.Post.Query.Domain.Entities;

namespace EDA.Post.Query.Api.DTOs
{
    public class PostLookupResponse 
    {
        public Guid Id { get; set; }
        public List<PostEntity> Posts { get; set; }
    }
}
