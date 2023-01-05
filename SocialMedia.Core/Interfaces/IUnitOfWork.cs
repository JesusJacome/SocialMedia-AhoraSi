using SocialMedia.Core.Entities;
using SocialMedia.Infrastructure.Interfaces;

namespace SocialMedia.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IPostRepository PostRepository { get; }
        IRepository<User> UserRepository { get; }
        IRepository<Comment> CommentRepository { get; }
        ISecurityRepository SecurityRepository { get; } 
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
