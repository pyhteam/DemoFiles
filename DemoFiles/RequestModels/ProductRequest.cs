
namespace DemoFiles.RequestModels
{
    public class ProductRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }
    }
}