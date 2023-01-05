using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SocialMedia.Api.Reponses;
using SocialMedia.Core.CustomEntities;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.QueryFilters;
using SocialMedia.Infrastructure.Interfaces;
using System.Net;

namespace SocialMedia.Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class PostController : ControllerBase
    {
        private readonly IPostService postService;
        private readonly IMapper mapper;
        private readonly IUriService uriService;

        public PostController(IPostService postService, IMapper mapper, IUriService uriService)
        {
            this.postService = postService;
            this.mapper = mapper;
            this.uriService = uriService;
        }

        /// <summary>
        /// Retrieve all posts
        /// </summary>
        /// <param name="filters">Filters to Apply</param>
        /// <returns></returns>

        [HttpGet(Name = nameof(GetPosts))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<PostDTO>>))]
        public IActionResult GetPosts([FromQuery]PostQueryFilter filters)
        {
            var posts = postService.GetPosts(filters);
            var postsDTO = mapper.Map<IEnumerable<PostDTO>>(posts);


            var metadata = new Metadata
            {
                TotalCount = posts.TotalCount,
                PageSize = posts.PageSize,
                CurrentPage = posts.CurrentPage,
                TotalPages = posts.TotalPages,
                HasNextPage = posts.HasNextPage,
                HasPreviousPage = posts.HasPreviousPage,
                NextPageUrl = uriService.GetPostPaginationUri(filters, Url.RouteUrl(nameof(GetPosts))).ToString()
            };

            var response = new ApiResponse<IEnumerable<PostDTO>>(postsDTO)
            {
                Meta = metadata
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(response);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetPost(int id)
        {
            var post = await postService.GetPost(id);
            var postDTO = mapper.Map<PostDTO>(post);
            var response = new ApiResponse<PostDTO>(postDTO);
            return Ok(response);
        }

        [HttpPost]

        public async Task<IActionResult> Post(PostDTO postDTO)
        {
            var post = mapper.Map<Post>(postDTO);

            await postService.InsertPost(post);

            postDTO = mapper.Map<PostDTO>(post);
            var response = new ApiResponse<PostDTO>(postDTO);

            return Ok(response);
        }

        [HttpPut]

        public async Task<IActionResult> Put(int id, PostDTO postDTO)
        {
            var post = mapper.Map<Post>(postDTO);
            post.Id = id;

            var result = await postService.UpdatePost(post);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }


        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            var result = await postService.DeletePost(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
