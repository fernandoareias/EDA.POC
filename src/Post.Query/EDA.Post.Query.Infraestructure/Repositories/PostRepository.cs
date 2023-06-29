using EDA.Post.Query.Domain.Entities;
using EDA.Post.Query.Domain.Repositories;
using EDA.Post.Query.Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDA.Post.Query.Infraestructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly DatabaseContextFactory _contextFactory;
        
        public PostRepository(DatabaseContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public Task CreateAsync(PostEntity post)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(PostEntity post)
        {
            throw new NotImplementedException();
        }

        public Task<List<PostEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PostEntity> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<PostEntity>> ListByAuthorAsync(string author)
        {
            throw new NotImplementedException();
        }

        public Task<List<PostEntity>> ListWithCommentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<PostEntity>> ListWithLikesAsync(int numberOfLikes)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(PostEntity post)
        {
            throw new NotImplementedException();
        }
    }
}
