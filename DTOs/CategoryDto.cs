namespace API.DTOs
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CategoryUrl { get; set; } = string.Empty;
    }
}