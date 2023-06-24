using EDA.Core.Domain;
using EDA.Post.Cmd.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDA.Post.Cmd.Domain.Aggregates
{
    public class Post : AggregateRoot
    {
        public bool Active { get; set; }
        public string Author { get; set; }
        public Dictionary<Guid, Tuple<string, string>> Comments { get; set; } = new();

        public Post()
        {
                
        }

        public Post(Guid id, string author, string message)
        {
            RaiseEvent(new PostCreatedEvent
            {
                Id = id,
                Author = author,
                Message = message,
                DatePosted = DateTime.UtcNow
            });
        }

        public void Apply(PostCreatedEvent @event)
        {
            Id = @event.Id;
            Author = @event.Author;
            Active = true;

        }

        public void EditMessage(string message)
        {
            if (!Active)
                throw new InvalidOperationException("You cannot edit the message of an inactive post!");

            if(string.IsNullOrEmpty(message))
                throw new NullReferenceException("message");


            RaiseEvent(new MessageUpdatedEvent
            {
                Id = Id,
                Message = message
            });
        }

        public void Apply(MessageUpdatedEvent @event)
        {
            Id = @event.Id;
        }

        public void LikePost()
        {
            if (!Active)
                throw new InvalidOperationException("You cannot like post of an inactive post!");

            RaiseEvent(new PosteLikedEvent
            {
                Id = Id
            });
        }

        public void Apply(PosteLikedEvent @event)
        {
            Id = @event.Id;
        }

        public void AddComment(string comment, string username)
        {
            if (!Active)
                throw new InvalidOperationException("You cannot add the message of an inactive post!");

            RaiseEvent(new CommentAddedEvent
            {
                Id = Id,
                CommentId = Guid.NewGuid(),
                Comment = comment,
                Username = username,
                CommentDate = DateTime.Now
            });
        }

        public void Apply(CommentAddedEvent @event)
        {
            Id = @event.Id;
            Comments.Add(@event.CommentId, new Tuple<string, string>(@event.Comment, @event.Username));
        }

        public void EditComment(Guid commentId, string comment, string username)
        {
            if (!Active)
                throw new InvalidOperationException("You cannot add the message of an inactive post!");

            if (!Comments[commentId].Item2.Equals(username, StringComparison.CurrentCultureIgnoreCase))
                throw new InvalidOperationException("You are not allowed to edit a comment that was made by another user!");

            RaiseEvent(new CommentUpdatedEvent
            {
                Id = Id,
                CommentId = commentId,
                Comment = comment,
                Username = username,
                EditDate = DateTime.Now
            });
        }

        public void Apply(CommentUpdatedEvent @event)
        {
            Id = @event.Id;
            Comments[@event.CommentId] = new Tuple<string, string>(@event.Comment, @event.Username);
        }
        
        public void RemoveComment(Guid commentId, string username)
        {
            if (!Active)
                throw new InvalidOperationException("You cannot add the message of an inactive post!");

            if (!Comments[commentId].Item2.Equals(username, StringComparison.CurrentCultureIgnoreCase))
                throw new InvalidOperationException("You are not allowed to edit a comment that was made by another user!");

            RaiseEvent(new CommentRemovedEvent
            {
                Id = Id,
                CommentId = commentId,

            });
        }

        public void Apply(CommentRemovedEvent @event)
        {
            Id = @event.Id;
            Comments.Remove(@event.CommentId);
        }

        public void DeletePost(string username)
        {
            if (!Active)
                throw new InvalidOperationException("You cannot add the message of an inactive post!");

            if (!Author.Equals(username, StringComparison.CurrentCultureIgnoreCase))
                throw new InvalidOperationException("You are not allowed to delete a post that was made by someone else!");

            RaiseEvent(new PostRemovedEvent
            {
                Id = Id,
            });
        }

        public void Apply(PostRemovedEvent @event)
        {
            Id = @event.Id;
            Active = false;
        }

    }
}
