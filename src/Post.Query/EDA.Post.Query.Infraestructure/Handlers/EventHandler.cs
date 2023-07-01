using EDA.Post.Query.Domain.Entities;
using EDA.Post.Query.Domain.Events;
using EDA.Post.Query.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDA.Post.Query.Infraestructure.Handlers
{
    public class EventHandler : IEventHandler
    {
        public readonly IPostRepository _postRepository;
        public readonly ICommentRepository _commentRepository;

        public EventHandler(IPostRepository postRepository, ICommentRepository commentRepository)
        {
            _postRepository = postRepository;
            _commentRepository = commentRepository;
        }

        public async Task On(PostCreatedEvent @event)
        {
            var post = new PostEntity
            {
                PostId = @event.Id,
                Author = @event.Author,
                DatePosted = @event.DatePosted,
                Message = @event.Message
            };

            await _postRepository.CreateAsync(post);
        }

        public async Task On(MessageUpdatedEvent @event)
        {
            var post = await _postRepository.GetByIdAsync(@event.Id);

            if (post == null) return;

            post.Message = @event.Message;
            await _postRepository.UpdateAsync(post);

        }

        public async Task On(PosteLikedEvent @event)
        {
            var post = await _postRepository.GetByIdAsync(@event.Id);

            if (post == null) return;

            post.Likes++;
            await _postRepository.UpdateAsync(post);
        }

        public async Task On(CommentAddedEvent @event)
        {
            var comment = new CommentEntity
            {
                PostId = @event.Id,
                CommentId = @event.CommentId,
                CommentDate = @event.CommentDate,
                Comment = @event.Comment,
                Username = @event.Username,
                Edited = true
            };

            await _commentRepository.CreateAsync(comment);
        }

        public async Task On(CommentUpdatedEvent @event)
        {
            var comment = await _commentRepository.GetByIdAsync(@event.Id);

            if (comment == null) return;

            comment.Comment = @event.Comment;
            comment.Edited = true;
            comment.CommentDate = @event.CommentDate;
            
            await _commentRepository.UpdateAsync(comment);
        }

        public async Task On(CommentRemovedEvent @event)
        {
            await _commentRepository.DeleteAsync(@event.Id);
        }

        public async Task On(PostRemovedEvent @event)
        {
            var post = await _postRepository.GetByIdAsync(@event.Id);

            if (post == null) return;

            await _postRepository.DeleteAsync(post);
        }
    }
}
