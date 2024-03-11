using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.Interfaces;
using API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Implementations
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly AppDbContext _context;
        public BlogPostRepository(AppDbContext context)
        {
            _context = context;

        }

        public async Task<BlogPost> CreateBlogPost(BlogPost blogPost)
        {
            await _context.BlogPosts.AddAsync(blogPost);
            await _context.SaveChangesAsync();

            return blogPost;
        }

        public async Task<BlogPost?> DeleteBlogPostById(Guid id)
        {
            var existBlogPost = await _context.BlogPosts.FirstOrDefaultAsync(c => c.Id == id);

            if (existBlogPost is null)
            {
                return null;
            }

            _context.BlogPosts.Remove(existBlogPost);
            await _context.SaveChangesAsync();
            
            return existBlogPost; 
        }

        public async Task<IEnumerable<BlogPost>> GetAllBlogPosts()
        {
            return await _context.BlogPosts.Include(c => c.Categories).ToListAsync();
        }

        public async Task<BlogPost?> GetBlogPostById(Guid id)
        {
            return await _context.BlogPosts.Include(c => c.Categories).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<BlogPost?> GetBlogPostByUrl(string url)
        {
            return await _context.BlogPosts.Include(c => c.Categories).FirstOrDefaultAsync(c => c.UrlHandle == url);
        }

        public async Task<BlogPost?> UpdateBlogPostById(BlogPost blogPost)
        {
            var existingBlogPost = await _context.BlogPosts.Include(c => c.Categories).FirstOrDefaultAsync(c => c.Id == blogPost.Id);

            if (existingBlogPost is null)
            {
                return null;
            }

            _context.Entry(existingBlogPost).CurrentValues.SetValues(blogPost);
            existingBlogPost.Categories = blogPost.Categories;

            await _context.SaveChangesAsync();

            return blogPost;
        }
    }
}