using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoFiles.Data;
using DemoFiles.Models;
using DemoFiles.RequestModels;
using DemoFiles.Services;
using Microsoft.AspNetCore.Mvc;

namespace DemoFiles.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly HMZContext _context;
        public ProductController( IFileService fileService, HMZContext context)
        {
            _fileService = fileService;
            _context = context;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Products.ToList());
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var product = _context.Products.Find(id);
            if(product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] ProductRequest productRequest)
        {
           string filePath = null;
            if(productRequest.Image != null)
            {
                var file = Request.Form.Files[0];
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string filePath1 = $"Product\\{DateTime.Now.ToString("yyyyMMdd")}\\{fileName}";
                var fileStream = await  ConvertFormFileToStream(file);
				filePath = await _fileService.SaveFileAsync(filePath1, fileStream);
            }
            var product = new Product
            {
                Name = productRequest.Name,
                Description = productRequest.Description,
                ImagePath = filePath
            };

            _context.Products.Add(product);
            _context.SaveChanges();
            return Ok(new {
                message = "Product added successfully",
                product = product
            });
        }
		private async Task<Stream> ConvertFormFileToStream(IFormFile file)
		{
			var stream = new MemoryStream();
			await file.CopyToAsync(stream);
			stream.Position = 0; // Đặt lại vị trí của Stream về đầu để đọc từ đầu
			return stream;
		}


		// Get To Images With Path
		[HttpGet]
        public async Task<IActionResult> GetImage(string uri)
        {
            var stream = await _fileService.GetFileAsync(uri);
            return File(stream, "image/jpeg");
        }

        // Delete Image
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = _context.Products.Find(id);
            if(product == null)
            {
                return NotFound();
            }
            if(!string.IsNullOrEmpty(product.ImagePath))
            {
                await _fileService.DeleteFileAsync(product.ImagePath);
            }
            _context.Products.Remove(product);
            _context.SaveChanges();
            return Ok(new {
                message = "Product deleted successfully"
            });
        }
    }
}