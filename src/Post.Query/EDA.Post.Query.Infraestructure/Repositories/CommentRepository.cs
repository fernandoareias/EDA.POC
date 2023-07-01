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
    public class CommentRepository : ICommentRepository
    {
        private readonly DatabaseContextFactory _contextFactory;

        public CommentRepository(DatabaseContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }


        public async Task CreateAsync(CommentEntity comment)
        {
            using ApplicationDbContext context = _contextFactory.CreateDbContext();
            await context.Comments.AddAsync(comment);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            using ApplicationDbContext context = _contextFactory.CreateDbContext();
            var comment = await context.Comments.FirstOrDefaultAsync(c => c.PostId == id);
            if (comment == null) return;
            context.Comments.Remove(comment);
            await context.SaveChangesAsync();
        }

        public async Task<CommentEntity?> GetByIdAsync(Guid id)
        {
            using ApplicationDbContext context = _contextFactory.CreateDbContext();

            return await context.Comments.FirstOrDefaultAsync(c => c.CommentId == id);
        }

        public async Task UpdateAsync(CommentEntity comment)
        {
            using ApplicationDbContext context = _contextFactory.CreateDbContext();
            context.Comments.Update(comment);
            await context.SaveChangesAsync();
        }
    }
}
