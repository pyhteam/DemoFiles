using DemoFiles.Services;
using Microsoft.AspNetCore.Mvc;

namespace DemoFiles.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FilesController : ControllerBase
	{
		private readonly IFileService _fileService;
		public FilesController(IFileService _fileService)
		{
			this._fileService = _fileService;
		}

		[HttpPost("signature")]
		public  IActionResult Post()
		{
			//string filePath = @"E:\Don_Ly_Hon.pdf";
			string filePath = @"E:\Don_Ly_Hon.pdf";
			string pdfOutput = @"E:\HMZOutput\"+Guid.NewGuid().ToString()+".pdf";
			string imageFilePath = @"E:\signature.png";
			try
			{
				
				//HMZHelperPDF.ReplaceImage(filePath, imageFilePath, pdfOutput);
				HMZHelperPDF.ReplaceImagesInPdf(filePath, imageFilePath, pdfOutput);

				return Ok(pdfOutput);

			}
			catch(Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
