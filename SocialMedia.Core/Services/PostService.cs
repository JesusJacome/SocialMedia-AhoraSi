using Microsoft.Extensions.Options;
using SocialMedia.Core.CustomEntities;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Exceptions;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.QueryFilters;

namespace SocialMedia.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly PaginationOptions options;

        public PostService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            this.unitOfWork = unitOfWork;
            this.options = options.Value;
        }


        public async Task<Post> GetPost(int id)
        {
            return await unitOfWork.PostRepository.GetByIdAsync(id);
        }

        public PagedList<Post> GetPosts(PostQueryFilter filters )
        {
            filters.PageNumber = filters.PageNumber == 0 ? options.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? options.DefaultPageSize : filters.PageSize;


            var posts = unitOfWork.PostRepository.GetAll();

            if (filters.UserId != null)
            {
                posts = posts.Where(x => x.UserId == filters.UserId);
            }
            
            if(filters.Date != null)
            {
                posts = posts.Where(x => x.Date.ToShortDateString() == filters.Date?.ToShortDateString());
            }

            if (filters.Description != null)
            {
                posts = posts.Where(x => x.Description.ToLower().Contains(filters.Description.ToLower()));
            }

            var pagedPosts = PagedList<Post>.Create(posts, filters.PageNumber, filters.PageSize);

            return pagedPosts;
        }

        public async Task InsertPost(Post post)
        {
            var user = await unitOfWork.UserRepository.GetByIdAsync(post.UserId);

            if (user == null)
            {
                throw new BussinesExceptions("User doesn't exist");
            }

            var userPosts = await unitOfWork.PostRepository.GetPostsByUser(post.UserId);

            if(userPosts.Count() < 10)
            {
                var lastPost = userPosts.OrderByDescending(x => x.Date).FirstOrDefault();
                if ((DateTime.Now - lastPost.Date).TotalDays < 7)
                {
                    throw new BussinesExceptions("You are not able to publish the post");
                }
            }


            if (post.Description.Contains("sexo"))
            {
                throw new BussinesExceptions("Content not allowed");
            }
            await unitOfWork.PostRepository.AddAsync(post);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdatePost(Post post)
        {
            unitOfWork.PostRepository.Update(post);
            await unitOfWork.SaveChangesAsync();
            return true;
            
        }
        public async Task<bool> DeletePost(int id)
        {
            await unitOfWork.PostRepository.DeleteAsync(id);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
