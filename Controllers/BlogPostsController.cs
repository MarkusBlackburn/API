using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.Data.Interfaces;
using API.DTOs;
using API.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly ICategoryRepository _categoryRepository;

        public BlogPostsController(IBlogPostRepository blogPostRepository, ICategoryRepository categoryRepository)
        {
            _blogPostRepository = blogPostRepository;
            _categoryRepository = categoryRepository;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateBlogPost(CreateBlogPostRequestDto request)
        {
            var blogPost = new BlogPost
            {
                Author = request.Author,
                Title = request.Title,
                Content = request.Content,
                ImageUrl = request.ImageUrl,
                IsVisible = request.IsVisible,
                PublishedDate = request.PublishedDate,
                ShortDescription = request.ShortDescription,
                UrlHandle = request.BlogPostUrl,
                Categories = new List<Category>()
            };

            foreach (var categoryId in request.Categories)
            {
                var existCategory = await _categoryRepository.GetCategoryById(categoryId);

                if (existCategory is not null)
                {
                    blogPost.Categories.Add(existCategory);
                }
            }

            blogPost = await _blogPostRepository.CreateBlogPost(blogPost);

            var response = new BlogPostDto
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                ShortDescription = blogPost.ShortDescription,
                Content = blogPost.Content,
                ImageUrl = blogPost.ImageUrl,
                BlogPostUrl = blogPost.UrlHandle,
                PublishedDate = blogPost.PublishedDate,
                Author = blogPost.Author,
                IsVisible = blogPost.IsVisible,
                Categories = blogPost.Categories.Select(c => new CategoryDto{
                    Id = c.Id,
                    Name = c.Name,
                    CategoryUrl = c.UrlHandle
                }).ToList()
            };

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogPosts()
        {
            var blogPosts = await _blogPostRepository.GetAllBlogPosts();

            var response = new List<BlogPostDto>();
            foreach (var blogpost in blogPosts)
            {
                response.Add(new BlogPostDto
                {
                    Id = blogpost.Id,
                    Title = blogpost.Title,
                    ShortDescription = blogpost.ShortDescription,
                    Content = blogpost.Content,
                    ImageUrl = blogpost.ImageUrl,
                    BlogPostUrl = blogpost.UrlHandle,
                    Author = blogpost.Author,
                    PublishedDate = blogpost.PublishedDate,
                    IsVisible = blogpost.IsVisible,
                    Categories = blogpost.Categories.Select(c => new CategoryDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        CategoryUrl = c.UrlHandle
                    }).ToList()
                });
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetBlogPostById([FromRoute] Guid id)
        {
            var blogPost = await _blogPostRepository.GetBlogPostById(id);

            if (blogPost is null)
            {
                return NotFound();
            }

            var response = new BlogPostDto
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                ShortDescription = blogPost.ShortDescription,
                Content = blogPost.Content,
                ImageUrl = blogPost.ImageUrl,
                BlogPostUrl = blogPost.UrlHandle,
                PublishedDate = blogPost.PublishedDate,
                Author = blogPost.Author,
                IsVisible = blogPost.IsVisible,
                Categories = blogPost.Categories.Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    CategoryUrl = c.UrlHandle
                }).ToList()
            };

            return Ok(response);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateBlogPostById([FromRoute] Guid id, UpdateBlogPostRequesDto request)
        {
            var blogPost = new BlogPost
            {
                Id = id,
                Title = request.Title,
                UrlHandle = request.BlogPostUrl,
                ShortDescription = request.ShortDescription,
                Content = request.Content,
                ImageUrl = request.ImageUrl,
                PublishedDate = request.PublishedDate,
                Author = request.Author,
                IsVisible = request.IsVisible,
                Categories = new List<Category>()
            };

            foreach (var categoryGuid in request.Categories)
            {
                var existCategory = await _categoryRepository.GetCategoryById(categoryGuid);

                if (existCategory is not null)
                {
                    blogPost.Categories.Add(existCategory);
                }
            }

            var updatedBlogPost = await _blogPostRepository.UpdateBlogPostById(blogPost);

            if (updatedBlogPost is null)
            {
                return NotFound();
            }

            var response = new BlogPostDto
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                ShortDescription = blogPost.ShortDescription,
                Content = blogPost.Content,
                ImageUrl = blogPost.ImageUrl,
                BlogPostUrl = blogPost.UrlHandle,
                PublishedDate = blogPost.PublishedDate,
                Author = blogPost.Author,
                IsVisible = blogPost.IsVisible,
                Categories = blogPost.Categories.Select(c => new CategoryDto{
                    Id = c.Id,
                    Name = c.Name,
                    CategoryUrl = c.UrlHandle
                }).ToList()
            };

            return Ok(response);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteBlogPostById([FromRoute] Guid id)
        {
            var blogPost = await _blogPostRepository.DeleteBlogPostById(id);

            if (blogPost is null)
            {
                return NotFound();
            }

            var response = new BlogPostDto
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                ShortDescription = blogPost.ShortDescription,
                Content = blogPost.Content,
                ImageUrl = blogPost.ImageUrl,
                BlogPostUrl = blogPost.UrlHandle,
                PublishedDate = blogPost.PublishedDate,
                Author = blogPost.Author,
                IsVisible = blogPost.IsVisible,
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("{url}")]
        public async Task<IActionResult> GetBlogPostByUrl([FromRoute] string url)
        {
            var blogPost = await _blogPostRepository.GetBlogPostByUrl(url);

            if (blogPost is null)
            {
                return NotFound();
            }

            var response = new BlogPostDto
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                ShortDescription = blogPost.ShortDescription,
                Content = blogPost.Content,
                ImageUrl = blogPost.ImageUrl,
                BlogPostUrl = blogPost.UrlHandle,
                PublishedDate = blogPost.PublishedDate,
                Author = blogPost.Author,
                IsVisible = blogPost.IsVisible,
                Categories = blogPost.Categories.Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    CategoryUrl = c.UrlHandle
                }).ToList()
            };

            return Ok(response);
        }
    }
}