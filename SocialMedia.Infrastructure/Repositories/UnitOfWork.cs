using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
using SocialMedia.Infrastructure.Interfaces;

namespace SocialMedia.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SocialMediaContext context;
        private readonly PostRepository postRepository;
        private readonly IRepository<User> userRepository;
        private readonly IRepository<Comment> commentRepository;
        private readonly SecurityRepository securityRepository;

        public UnitOfWork(SocialMediaContext context)
        {
            this.context = context;
        }

        public IPostRepository PostRepository => postRepository ?? new PostRepository(context);

        public IRepository<User> UserRepository => userRepository ?? new BaseRepository<User>(context);

        public IRepository<Comment> CommentRepository => commentRepository ?? new BaseRepository<Comment>(context);

        public ISecurityRepository SecurityRepository => securityRepository ?? new SecurityRepository(context);

        public void Dispose()
        {
            if (context != null)
            {
                context.Dispose();
            }
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
