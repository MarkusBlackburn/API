using API.Models.Domain;

namespace API.Data.Interfaces
{
    public interface IImageRepository
    {
        Task<BlogImage> Upload(IFormFile file, BlogImage image);
        Task<IEnumerable<BlogImage>> GetAll();
    }
}