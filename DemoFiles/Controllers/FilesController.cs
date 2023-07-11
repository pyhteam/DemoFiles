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
			string wordFilePath = @"E:\Don Ly Hon.docx";
			string pdfFilePath = @"E:\HMZOutput\"+Guid.NewGuid().ToString()+".pdf";
			string imageFilePath = @"E:\signature.png";
			try
			{
				HMZHelper.ExportWordToPdf(wordFilePath, pdfFilePath, imageFilePath, 70, 70, "SignatureKey");

				return Ok();

			}
			catch(Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
