using EDA.Post.Query.Domain.Entities;
using EDA.Post.Query.Domain.Repositories;
using EDA.Post.Query.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
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

        public async Task CreateAsync(PostEntity post)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                context.Posts.Add(post);
                await context.SaveChangesAsync();
            } 
        }

        public async Task DeleteAsync(PostEntity post)
        {
            using ApplicationDbContext context = _contextFactory.CreateDbContext();
            context.Posts.Remove(post);
            await context.SaveChangesAsync();
        }

        public async Task<List<PostEntity>> GetAllAsync()
        {
            using ApplicationDbContext context = _contextFactory.CreateDbContext();
            return await context.Posts.ToListAsync();
        }

        public async Task<PostEntity?> GetByIdAsync(Guid id)
        {
            using ApplicationDbContext context = _contextFactory.CreateDbContext();
            return await context.Posts.FirstOrDefaultAsync(c => c.PostId == id);
        }

        public async Task<List<PostEntity>> ListByAuthorAsync(string author)
        {
            using ApplicationDbContext context = _contextFactory.CreateDbContext();
            return await context.Posts.Where(c => c.Author == author).ToListAsync();
        }

        public async Task<List<PostEntity>> ListWithCommentsAsync()
        {
            using ApplicationDbContext context = _contextFactory.CreateDbContext();
            return await context.Posts.Include(c => c.Comments).ToListAsync();
        }

        public async Task<List<PostEntity>> ListWithLikesAsync(int numberOfLikes)
        {
            using ApplicationDbContext context = _contextFactory.CreateDbContext();
            return await context.Posts.Where(c => c.Likes == numberOfLikes).ToListAsync();
        }

        public async Task UpdateAsync(PostEntity post)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                context.Posts.Update(post);
                await context.SaveChangesAsync();
            }
        }
    }
}
