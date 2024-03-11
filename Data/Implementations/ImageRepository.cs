using API.Data.Interfaces;
using API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Implementations
{
    public class ImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext _context;

        public ImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, AppDbContext context)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public async Task<IEnumerable<BlogImage>> GetAll()
        {
            return await _context.BlogImages.ToListAsync();
        }

        public async Task<BlogImage> Upload(IFormFile file, BlogImage image)
        {
            var localPath = Path.Combine(_webHostEnvironment.ContentRootPath, "images", $"{image.Filename}{image.FileExtension}");

            using var stream = new FileStream(localPath, FileMode.Create);

            await file.CopyToAsync(stream);

            var httpRequest = _httpContextAccessor.HttpContext.Request;
            var urlPath = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/images/{image.Filename}{image.FileExtension}";

            image.ImageUrl = urlPath;

            await _context.BlogImages.AddAsync(image);
            await _context.SaveChangesAsync();

            return image;
        }
    }
}