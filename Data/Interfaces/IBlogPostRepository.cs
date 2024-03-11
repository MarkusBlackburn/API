using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models.Domain;

namespace API.Data.Interfaces
{
    public interface IBlogPostRepository
    {
        Task<BlogPost> CreateBlogPost(BlogPost blogPost);

        Task<IEnumerable<BlogPost>> GetAllBlogPosts();

        Task<BlogPost?> GetBlogPostById(Guid id);

        Task<BlogPost?> UpdateBlogPostById(BlogPost blogPost);

        Task<BlogPost?> DeleteBlogPostById(Guid id);

        Task<BlogPost?> GetBlogPostByUrl(string url);
    }
}